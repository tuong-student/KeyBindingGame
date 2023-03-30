using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Player;
using Game.Interface;

namespace Game.Item
{
    public class Bomb : MonoBehaviour, Iitem
    {
        [SerializeField] private Animator animator;
        public void Interact(Player.Player player)
        {
            
        }

        public void Pickup(Player.Player player)
        {
            player.SetCurrentItem(this);
            player.SetCurrentItemAnimator(this.animator);
            player.Carry(this);
        }
    }
}
