using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Game.System;
using Game.Interface;

namespace Game.Player
{
    public class Player : MonoBehaviour
    {
        private int _PLAYER_LAYER;
        public int PLAYER_LAYER => _PLAYER_LAYER;
        //----------------------------------------------------------------//
        #region Digit
        [SerializeField] private float speed = 5f;
        [SerializeField] private float dashCoolDown = 1f;
        [SerializeField] private float throwPower = 7f;
        private Vector3 moveDirection;
        private float dashTimer;
        private Vector2 faceDirection;
        #endregion
        //----------------------------------------------------------------//
        #region Bool
        private bool isAttacking;
        #endregion
        //----------------------------------------------------------------//
        #region Reference
        [SerializeField] private PlayerVisual playerVisual;
        [SerializeField] private Game.Item.Sword sword;
        private Game.Item.BaseItem groundItem;
        private Game.Item.BaseItem currentItem;
        #endregion
        //----------------------------------------------------------------//
        #region Event
        public EventHandler OnPlayerPickUp;
        #endregion
        //----------------------------------------------------------------//
        #region Instance
        GameInput gameInputInstance;
        #endregion

        //----------------------------------------------------------------//
        //----------------------------------------------------------------//

        //--- Unity Functions ---//

        void Awake()
        {
            _PLAYER_LAYER = playerVisual.GetSortingLayer();            
        }

        void Start()
        {
            gameInputInstance = SingletonContainer.Resolve<GameInput>();
            SingletonContainer.Resolve<GameManager>();
            gameInputInstance.OnPlayerMove += Move;
            gameInputInstance.gameInputSystem.Player.Dash.performed += (InputAction) => Dash();

            gameInputInstance.gameInputSystem.Player.PickupOrThrow.performed += (InputAction.CallbackContext callbackContext) =>
            {
                if(IsHasCurrentItem() == false)
                {
                   PickUp();
                }
                else
                {
                    Throw();
                }
            };

            gameInputInstance.gameInputSystem.Player.SwordAttack.performed += (InputAction.CallbackContext callbackContext) => Attack();
        }

        void Update()
        {
            if(dashTimer > 0f) 
            {
                dashTimer -= Time.deltaTime;
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            Game.Item.BaseItem temp = other.gameObject.GetComponent<Game.Item.BaseItem>();
            if(temp != null)
            {
                SetGroundItem(temp);
            }
        }

        //--- Custom Functions ---//
        private void Move(object sender, Vector2 movement)
        {
            Vector3 playerMovement = new Vector3(movement.x, movement.y, 0);
            this.transform.position += playerMovement * speed * Time.deltaTime;
            moveDirection = movement;
            if(movement != Vector2.zero)
            {
                this.faceDirection = movement;
            }
        }

        private void Dash()
        {
            if(dashTimer > 0f) return;
            if(moveDirection == Vector3.zero)
            {
                moveDirection = Vector3.right;
            }

            this.transform.position += moveDirection * speed;
            dashTimer = dashCoolDown;
        }

        private void PickUp()
        {
            if(groundItem != null && IsHasCurrentItem() == false)
            {
                SetIsHoldingTrue();
                groundItem.PickUp(this);
                SetCurrentItem(groundItem);
                OnPlayerPickUp?.Invoke(this, EventArgs.Empty);
            }
        }

        private void Attack()
        {
            playerVisual.SetCoverActivation(true);
            // When attack player will stop   
            SetIsAttacking(true);
            playerVisual.ActiveCoverSwordAttack();
            sword.Attack();
        }

        public float GetSwordAttackTime()
        {
            return sword.GetAttackTime();
        }

        public Vector2 GetFaceDirection()
        {
            return this.faceDirection;
        }

        public void SetIsAttacking(bool isAttacking)
        {
            this.isAttacking = isAttacking;
        }

        private void SetIsHoldingTrue()
        {
            playerVisual.SetIsHolding(true);
        }

        private void Throw()
        {
            if(IsHasCurrentItem())
            {
                playerVisual.Throw();
                currentItem.Throw(this.throwPower);
                playerVisual.SetIsHolding(false);
                SetCurrentItem(null);
            }
        }

        private void SetGroundItem(Game.Item.BaseItem item)
        {
            groundItem = item;
        }

        private void SetCurrentItem(Game.Item.BaseItem item)
        {
            this.currentItem = item;
        }

        public Iitem GetCurrentItem()
        {
            return this.currentItem;
        }

        public bool IsHasCurrentItem()
        {
            return this.currentItem != null;   
        }
    }
}
