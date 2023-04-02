using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Interface
{
    public interface Iitem 
    {
        void Interact(Game.Player.Player player);
        void PickUp(Game.Player.Player player);
    }
}
