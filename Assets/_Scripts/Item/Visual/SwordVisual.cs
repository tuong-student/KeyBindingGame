using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Item
{
    public class SwordVisual : MonoBehaviour
    {
        [SerializeField] private Sword sword;
        [SerializeField] private Animator anim;
        [SerializeField] private SpriteRenderer sr;
        [SerializeField] private Player.Player player;
        private Vector2 faceDirection;

        void Start()
        {
            SingletonContainer.Resolve<Game.System.GameInput>().OnPlayerMove += SetFaceDirection;
        }

        public void ActiveAttackAnimation()
        {
            sr.sortingOrder = player.PLAYER_LAYER + 1;
            if(faceDirection.y > 0)
            {
                anim.SetInteger("UpSideDown", 1);
                sr.sortingOrder = player.PLAYER_LAYER - 1;
            }
            else if(faceDirection.y < 0)
            {
                anim.SetInteger("UpSideDown", -1);
            }

            if(faceDirection.x > 0)
            {
                anim.SetInteger("UpSideDown", 0);                
                //this.transform.localScale = new Vector3(-1, 1, 1);
            }
            else if(faceDirection.x < 0)
            {
                anim.SetInteger("UpSideDown", 0);
                //this.transform.localScale = new Vector3(1, 1, 1);
            }

            anim.SetTrigger("Attack");
            NOOD.NoodyCustomCode.StartDelayFunction(() => { sr.sortingOrder = player.PLAYER_LAYER - 1; }, GetAttackTime());
        }

        public float GetAttackTime()
        {
            return 0.5833334f;
        }

        private void SetFaceDirection(object sender, Vector2 direction) { if(direction != Vector2.zero)
            {
                if(direction.x == 0)
                {
                    this.faceDirection = direction;
                    if(direction.y > 0)
                        sr.sortingOrder = player.PLAYER_LAYER + 1;
                    else
                        sr.sortingOrder = player.PLAYER_LAYER - 1;
                }
                else
                {
                    sr.sortingOrder = player.PLAYER_LAYER - 1;
                    this.faceDirection = new Vector2(direction.x, 0);
                }
            }
        }
    }

}
