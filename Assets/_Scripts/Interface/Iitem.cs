using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Interface
{
    public interface Iitem  
    {
        void Pickup(Game.Player.Player player);
        void Interact(Game.Player.Player player);
    }
}
