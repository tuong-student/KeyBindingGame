using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Item
{
    public class Sword : MonoBehaviour
    {
        [SerializeField] private SwordVisual swordVisual;

        public void Attack()
        {
            swordVisual.ActiveAttackAnimation();
        }

        public float GetAttackTime()
        {
            return swordVisual.GetAttackTime();
        }
    }
}
