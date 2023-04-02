using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Interface;
using Game.System;

namespace Game.Item
{
    public class Bomb : MonoBehaviour, Iitem
    {
        [SerializeField] private BombVisual bombVisual;

        void Start()
        {
            
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            Player.Player player = other.GetComponent<Player.Player>();
            if(player != null)
            {
                player.SetGroundItem(this);
                bombVisual.SetPlayer(player);
            }
            Debug.Log("OnTriggerEnter2D" + player);
        }

        public void Interact(Player.Player player)
        {
            
        }

        public void PickUp(Player.Player player)
        {
            this.transform.parent = player.transform;
            this.transform.localPosition = Vector3.zero;
            this.transform.localScale = Vector3.one;

            bombVisual.SetIsHold(true);

            GameInput.GetInstance.OnPlayerMove += (object sender, Vector2 move) =>
            {
                if(move != Vector2.zero)
                    bombVisual.SetIsCarry(true);
                else
                    bombVisual.SetIsCarry(false);
            };
        }

        public void Throw()
        {
            bombVisual.SetIsCarry(false);
            bombVisual.SetIsHold(false);
        }
    }
}
