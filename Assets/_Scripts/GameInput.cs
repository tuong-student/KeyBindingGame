using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.System
{
    public class GameInput : NOOD.MonoBehaviorInstance<GameInput>
    {
        public EventHandler<Vector2> OnPlayerMove;
        public GameInputSystem gameInputSystem;


        private void Awake() 
        {
            gameInputSystem = new GameInputSystem();
            gameInputSystem.Player.Enable();

        }
        // Start is called before the first frame update
        void Start()
        {
        }

        private void Update() 
        {
            Vector2 direction = gameInputSystem.Player.Move.ReadValue<Vector2>();
            OnPlayerMove?.Invoke(this, direction);
        }   
    }
}
