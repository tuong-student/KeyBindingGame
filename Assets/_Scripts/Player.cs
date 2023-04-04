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
        [SerializeField] private PlayerVisual playerVisual;

        [SerializeField] private float speed = 5f;
        private Vector3 moveDirection;

        [SerializeField] private float dashCoolDown = 1f;
        private float dashTimer;

        private Game.Interface.Iitem groundItem;
        [SerializeField] private Animator currentItemAnimator;
        private Game.Interface.Iitem currentItem;

        public EventHandler OnPlayerPickUp;

        #region Instance
        GameInput gameInputInstance;
        #endregion

        // Start is called before the first frame update
        void Start()
        {
            gameInputInstance = SingletonContainer.Resolve<GameInput>();
            SingletonContainer.Resolve<GameManager>();
            gameInputInstance.OnPlayerMove += Move;
            gameInputInstance.gameInputSystem.Player.Dash.performed += (InputAction) => Dash();

            gameInputInstance.gameInputSystem.Player.Pickup.performed += (InputAction.CallbackContext callbackContext) =>
            {
                PickUp();
            };

            gameInputInstance.gameInputSystem.Player.Throw.performed += (InputAction.CallbackContext callbackContext) =>
            {
                Throw();
            };

            _PLAYER_LAYER = playerVisual.GetSortingLayer();
        }

        // Update is called once per frame
        void Update()
        {
            if(dashTimer > 0f) 
            {
                dashTimer -= Time.deltaTime;
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            Iitem temp = other.gameObject.GetComponent<Iitem>();
            if(temp != null)
            {
                SetGroundItem(temp);
            }
        }

        private void Move(object sender, Vector2 movement)
        {
            Vector3 playerMovement = new Vector3(movement.x, movement.y, 0);
            this.transform.position += playerMovement * speed * Time.deltaTime;
            moveDirection = playerMovement;
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
            if(groundItem != null)
            {
                groundItem.PickUp(this);
                SetCurrentItem(groundItem);
                Carrying();
                OnPlayerPickUp?.Invoke(this, EventArgs.Empty);
            }
        }

        private void Attack()
        {
            playerVisual.SetActiveCover(true);
            // When attack player will stop    
        }

        private void Carrying()
        {
            playerVisual.SetIsHolding(true);
        }

        private void Throw()
        {
            currentItem.Throw();
            playerVisual.SetIsHolding(false);
        }

        private void SetGroundItem(Game.Interface.Iitem item)
        {
            groundItem = item;
        }

        private void SetCurrentItem(Game.Interface.Iitem item)
        {
            this.currentItem = item;
            
        }
    }
}
