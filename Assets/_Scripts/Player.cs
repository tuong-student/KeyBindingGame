using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Game.System;

namespace Game.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerVisual playerVisual;

        [SerializeField] private float speed = 5f;
        private Vector3 moveDirection;

        [SerializeField] private float dashCoolDown = 1f;
        private float dashTimer;

        private Game.Interface.Iitem groundItem;
        [SerializeField] private Animator currentItemAnimator;
        private Game.Interface.Iitem currentItem;

        // Start is called before the first frame update
        void Start()
        {
            GameInput.GetInstance.OnPlayerMove += Move;
            GameInput.GetInstance.gameInputSystem.Player.Dash.performed += (InputAction) => Dash();
            
        }

        // Update is called once per frame
        void Update()
        {
            if(dashTimer > 0f) 
            {
                dashTimer -= Time.deltaTime;
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
                groundItem.Pickup(this);
            }
        }

        private void Attack()
        {
            playerVisual.SetActiveCover(true);
            // When attack player will stop    
        }

        public void Carry(Game.Item.Bomb bomb)
        {
            playerVisual.SetIsHolding(true);
            playerVisual.SetActiveCover(true);
        }

        public void Throw()
        {
            playerVisual.SetIsHolding(false);
        }

        public void SetCurrentItem(Game.Interface.Iitem item)
        {
            if(item == null)
            {
                SetCurrentItemAnimator(null);
            }
            this.currentItem = item;
        }

        public void SetCurrentItemAnimator(Animator animator)
        {
            this.currentItemAnimator = animator;
        }
    }
}
