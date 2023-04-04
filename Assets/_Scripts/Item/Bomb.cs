using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Interface;
using Game.System;
using DG.Tweening;

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
            Player.Player temp = other.GetComponent<Player.Player>();
            if(temp != null)
            {
                bombVisual.SetPlayer(temp);
            }
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

            SingletonContainer.Resolve<GameInput>().OnPlayerMove += (object sender, Vector2 move) =>
            {
                if(move != Vector2.zero)
                    bombVisual.SetIsCarry(true);
                else
                    bombVisual.SetIsCarry(false);
            };
        }

        public void Throw()
        {
            ThrowMovement(this.transform.position + new Vector3(7, 0, 0));
            bombVisual.SetIsCarry(false);
            bombVisual.SetIsHold(false);
            this.transform.parent = null;
        }

        public void ThrowMovement(Vector2 destination)
        {
            float xDistance = this.transform.position.x - destination.x;
            xDistance = Mathf.Abs(xDistance);
            float xTime = xDistance * 0.1f;
            this.transform.DOMoveX(destination.x, xTime);
            this.transform.DOMoveY(this.transform.position.y + 0.5f, xTime * 2f/5f).SetEase(Ease.OutFlash).OnComplete(() => this.transform.DOMoveY(this.transform.position.y - 0.5f,xTime * 1f/5f).SetEase(Ease.InFlash));
        }
    }
}
