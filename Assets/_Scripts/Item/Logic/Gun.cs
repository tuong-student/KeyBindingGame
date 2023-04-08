using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Interface;
using DG.Tweening;

namespace Game.Item
{
    public class Gun : BaseItem 
    {
        private GunVisual gunVisual;

        protected override void Start()
        {
            base.Start();
            gunVisual = (GunVisual)itemVisual;
        }

        private void Fire()
        {
            // Play fire animation
            gunVisual.PlayAnimationShoot();
            // Fire a bullet
        }
    }
}
