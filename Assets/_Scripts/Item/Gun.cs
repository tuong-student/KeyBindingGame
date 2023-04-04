using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Interface;
using DG.Tweening;

namespace Game.Item
{
    public class Gun : MonoBehaviour, Iitem 
    {
        [SerializeField] private GunVisual gunVisual;

        public Animator GetAnimator()
        {
            throw new global::System.NotImplementedException();
        }

        public void Interact(Game.Player.Player player)
        {
            Fire();
        }

        public void PickUp(Player.Player player)
        {
            this.transform.parent = player.transform;
            this.transform.localPosition = Vector3.zero;
            this.transform.localScale = Vector3.one;


            // SingletonContainer.Resolve<GameInput>().OnPlayerMove += (object sender, Vector2 move) =>
            // {
            //     if(move != Vector2.zero)
            //         bombVisual.SetIsCarry(true);
            //     else
            //         bombVisual.SetIsCarry(false);
            // };
        }

        public void Throw()
        {
            ThrowMovement(this.transform.position + new Vector3(7, 0, 0));
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

        private void Fire()
        {
            // Play fire animation

            // Fire a bullet
        }
    }
}
