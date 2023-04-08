using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.System;

namespace Game.Item
{
    public class BaseItemVisual : MonoBehaviour
    {
        [SerializeField] protected BaseItem baseItemScript;
        [SerializeField] protected Animator anim;
        [SerializeField] protected SpriteRenderer sr;
        private Player.Player player;
        private bool isHolding = false;

        protected virtual void Start()
        {
            SingletonContainer.Resolve<GameInput>().OnPlayerMove += Animate;
            sr.sortingOrder = 0;
        }

        public virtual void SetIsCarry(bool isCarry)
        {
            anim.SetBool("IsCarry", isCarry);
        }

        public virtual void SetIsHold(bool isHold)
        {
            isHolding = isHold;
            anim.SetBool("IsHold", isHold);
            if(isHold == false)
            {
                sr.sortingOrder = player.PLAYER_LAYER - 1;
            }
            else
            {
                sr.sortingOrder = player.PLAYER_LAYER + 1;
            }
        }

        public void SetPlayer(Player.Player player)
        {
            this.player = player;
        }

        protected virtual void Animate(object sender, Vector2 movement)
        {
            if (movement != Vector2.zero)
            {
                anim.SetInteger("UpSideDown", (int)movement.y);

                if (isHolding)
                {
                    SetIsCarry(true);
                    if (movement.y > 0 && movement.x == 0)
                    {
                        sr.sortingOrder = player.PLAYER_LAYER - 1;
                    }
                    else
                    {
                        sr.sortingOrder = player.PLAYER_LAYER + 1;
                    }
                }
            }
            else
            {
                SetIsCarry(false);
            }
        }

        public Animator GetAnimator()
        {
            return anim;
        }
    }
}
