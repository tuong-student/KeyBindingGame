using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.System
{
    public class GameInput : MonoBehaviour, ISingleton
    {
        public EventHandler<Vector2> OnPlayerMove;
        public GameInputSystem gameInputSystem;

        private void Awake() 
        {
            RegisterToContainer();
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

        public void RegisterToContainer()
        {
            SingletonContainer.Register(this);
        }

        public void UnRegisterFromContainer()
        {
            throw new NotImplementedException();
        }
    }
}
