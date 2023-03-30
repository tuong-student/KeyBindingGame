using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.System;

namespace Game.Player
{
    public class PlayerVisual : MonoBehaviour
    {
        [SerializeField] private Animator anim;
        [SerializeField] private Animator coverAnim;

        [SerializeField] private SpriteRenderer sr;
        [SerializeField] private SpriteRenderer coverSr;

        private bool isHolding = false;

        void Awake()
        {
        }

        void Start()
        {
            GameInput.GetInstance.OnPlayerMove += Animate;
        }

        public void SetActiveCover(bool isActive)
        {
            coverAnim.gameObject.SetActive(isActive);
        }

        public void SetIsHolding(bool isHolding)
        {
            this.isHolding = isHolding;
        }

        private void Animate(object sender, Vector2 playerInput)
        {
            // Normal movement
            if(playerInput == Vector2.zero)
            {
                anim.SetBool("IsWalking", false);
                if(isHolding)
                {
                    coverAnim.SetBool("IsWalking", false);
                }
            }
            else
            {
                anim.SetBool("IsWalking", true);
                if(isHolding)
                {
                    coverAnim.SetBool("IsWalking", true);
                }
            }

            // Side the player sprite left right
            if(playerInput.x < 0)
            {
                sr.flipX = false;
                coverSr.flipX = false;
            }
            else if(playerInput.x > 0)
            {
                sr.flipX = true;
                coverSr.flipX = true;
            }

            // Side player sprite up down
            if(playerInput.y != 0)
            {
                anim.SetInteger("UpSideDown", (int) playerInput.y);
                if(coverAnim.enabled == true)
                    coverAnim.SetInteger("UpSideDown", (int)playerInput.y);
            }
            else if(playerInput.x != 0)
            {
                anim.SetInteger("UpSideDown", (int) playerInput.y);
                if(coverAnim.enabled == true)
                    coverAnim.SetInteger("UpSideDown", (int)playerInput.y);
            }
        }
    }
}
