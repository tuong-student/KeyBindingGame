using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Game.System;

namespace Game.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;
        private Vector3 moveDirection;

        [SerializeField] private float dashCoolDown = 1f;
        private float dashTimer;

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
    }
}
