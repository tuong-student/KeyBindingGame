using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Item
{
    public class GunVisual : BaseItemVisual
    {
        [SerializeField] private Sprite[] rifleImageUpSideDown = new Sprite[3];
        [SerializeField] private Vector3[] muzzleFlashPosition = new Vector3[3];

        [SerializeField] private Transform muzzleFlashTransform;

        protected override void Start()
        {
            base.Start();
        }

        protected override void Animate(object sender, Vector2 movement)
        {
            if(movement != Vector2.zero)
            {
                if(movement.y < 0)
                {
                    sr.sprite = rifleImageUpSideDown[2];
                    muzzleFlashTransform.localPosition = muzzleFlashPosition[2];
                }
                else if(movement.y > 0)
                {
                    sr.sprite = rifleImageUpSideDown[0];
                    muzzleFlashTransform.localPosition = muzzleFlashPosition[0];
                }

                if(movement.x < 0)
                {
                    sr.sprite = rifleImageUpSideDown[1];
                    muzzleFlashTransform.localPosition = muzzleFlashPosition[1];
                    this.transform.localScale = new Vector3(1, 1, 1);
                }
                else if(movement.x > 0)
                {
                    sr.sprite = rifleImageUpSideDown[1];
                    muzzleFlashTransform.localPosition = muzzleFlashPosition[1];
                    this.transform.localScale = new Vector3(-1, 1, 1);
                }
            }
        }

        public override void SetIsCarry(bool isCarry)
        {
            
        }

        public override void SetIsHold(bool isHold)
        {
            
        }

        public void PlayAnimationShoot()
        {
            GetAnimator().SetTrigger("Shoot");
        }
    }

}
