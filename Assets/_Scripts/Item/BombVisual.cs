using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Interface;
using Game.System;

namespace Game.Item
{
    public class BombVisual : MonoBehaviour
    {
        [SerializeField] private Animator anim;
        [SerializeField] private Bomb bombScript;
        [SerializeField] private SpriteRenderer sr;
        private Player.Player player;
        private bool isHolding = false;

        void Start()
        {
            GameInput.GetInstance.OnPlayerMove += Animate;
            sr.sortingOrder = 0;
        }

        public void SetIsCarry(bool isCarry)
        {
            anim.SetBool("IsCarry", isCarry);
        }

        public void SetIsHold(bool isHold)
        {
            anim.SetBool("IsHold", isHold);
            isHolding = isHold;
        }

        public void SetPlayer(Player.Player player)
        {
            this.player = player;
        }

        private void Animate(object sender, Vector2 movement)
        {
            if (movement != Vector2.zero)
            {
                anim.SetInteger("UpSideDown", (int)movement.y);

                if (isHolding)
                {
                    if (movement.y > 0)
                    {
                        sr.sortingOrder = player.PLAYER_LAYER - 1;
                    }
                    else
                    {
                        sr.sortingOrder = player.PLAYER_LAYER + 1;
                    }
                }
            }
        }

        public Animator GetAnimator()
        {
            return anim;
        }
    }

}
