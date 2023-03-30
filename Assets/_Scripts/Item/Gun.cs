using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Interface;

namespace Game.Item
{
    public class Gun : MonoBehaviour, Iitem
    {
        [SerializeField] private Animator animator;
        

        public void Interact(Game.Player.Player player)
        {
            Fire();
        }

        public void Pickup(Player.Player player)
        {
            player.SetCurrentItem(this);
            player.SetCurrentItemAnimator(this.animator);
        }

        private void Fire()
        {
            // Play fire animation

            // Fire a bullet
        }
    }
}
