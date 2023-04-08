using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Game.System;

namespace Game.Item
{
    public class BaseItem : MonoBehaviour, Game.Interface.Iitem
    {
        [SerializeField] protected BaseItemVisual itemVisual;
        private Vector3 faceDirection;

        protected virtual void Start()
        {
            SingletonContainer.Resolve<GameInput>().OnPlayerMove += SetDirection;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            Player.Player temp = other.GetComponent<Player.Player>();
            if(temp != null)
            {
                itemVisual.SetPlayer(temp);
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

            itemVisual.SetIsHold(true);
        }

        public void Throw(float throwPower)
        {
            this.transform.parent = null;
            itemVisual.SetIsCarry(false);
            itemVisual.SetIsHold(false);
            ThrowMovement(this.transform.position + faceDirection * throwPower);
        }

        private void ThrowMovement(Vector2 destination)
        {
            this.transform.localScale = Vector3.one;
            float distance = Vector3.Distance(this.transform.position, destination);
            float time = distance * 0.1f;
            this.transform.DOMove(destination, time);
            this.transform.DOScale(1.5f, time/2f).SetEase(Ease.InOutFlash).OnComplete(() => 
            this.transform.DOScale(1f, time/2f ).SetEase(Ease.InFlash));
        }

        private void SetDirection(object sender, Vector2 direction)
        {
            if(direction == Vector2.zero) return;
                this.faceDirection = direction.normalized;
        }
    }
}
