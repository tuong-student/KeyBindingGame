using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.System;

namespace Game.Player
{
    public class PlayerVisual : MonoBehaviour
    {
        [SerializeField] private Animator anim;
        [SerializeField] private SpriteRenderer sr;

        void Awake()
        {
        }

        void Start()
        {
            GameInput.GetInstance.OnPlayerMove += Animate;
        }

        private void Animate(object sender, Vector2 playerInput)
        {
            // Normal movement
            if(playerInput == Vector2.zero)
            {
                anim.SetBool("IsWalking", false);
            }
            else
            {
                anim.SetBool("IsWalking", true);
            }

            // Side the player sprite
            if(playerInput.x > 0) sr.flipX = true;
            else if(playerInput.x < 0) sr.flipX = false;
            if(playerInput.y != 0f)
                anim.SetInteger("UpSideDown", (int) playerInput.y);
            else if(playerInput.x != 0)
                anim.SetInteger("UpSideDown", 0);
        }
    }
}
