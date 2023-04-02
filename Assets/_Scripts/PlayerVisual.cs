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

        public int GetSortingLayer()
        {
            return sr.sortingOrder;
        }

        public void SetActiveCover(bool isActive)
        {
            coverAnim.gameObject.SetActive(isActive);
        }

        public void SetIsHolding(bool isHolding)
        {
            SetActiveCover(isHolding);
            this.isHolding = isHolding;
            anim.SetBool("IsHolding", isHolding);
            coverAnim.SetBool("IsHolding", isHolding);
        }

        public void SetIsWalking(bool isWalking)
        {
            anim.SetBool("IsWalking", isWalking);
            if(isHolding)
            {
                coverAnim.SetBool("IsWalking", isWalking);
            }
        }

        private void FlipX(bool isLeft)
        {
            Vector3 scale = this.transform.parent.transform.localScale;
            if(isLeft)
            {
                scale.x = 1;
            }
            else
            {
                scale.x = -1;
            }
            this.transform.parent.transform.localScale = scale;
        }

        private void Animate(object sender, Vector2 playerInput)
        {
            // Normal movement
            if(playerInput == Vector2.zero)
            {
                SetIsWalking(false);
            }
            else
            {
                SetIsWalking(true);
            }

            // Side the player sprite left right
            if(playerInput.x < 0)
            {
                FlipX(true);
                if(isHolding)
                {
                    SetIsHolding(true);
                }
            }
            else if(playerInput.x > 0)
            {
                FlipX(false);
                if(isHolding)
                {
                    SetIsHolding(true);
                }
            }

            // Side player sprite up down
            if(playerInput != Vector2.zero)
            {
                anim.SetInteger("UpSideDown", (int) playerInput.y);
                if(coverAnim.enabled == true)
                    coverAnim.SetInteger("UpSideDown", (int)playerInput.y);

                if(playerInput.y != 0 && playerInput.x == 0)
                    SetActiveCover(false);
            }
        }
    }
}
