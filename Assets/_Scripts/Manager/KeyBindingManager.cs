using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.UI;

namespace Game.System
{
    public enum Binding
    {
        Up,
        Down,
        Left,
        Right,
        Dash,
        SwordAttack,
        PickUpOrThrow
    }

    public class KeyBindingManager : MonoBehaviour, ISingleton
    {
        private static GameInputSystem gameInput;
        private KeyBindingPanel keyBindingPanel;
        bool isBinding = false;

        void Awake()
        {
            RegisterToContainer();
        }

        void Start()
        {
            gameInput = SingletonContainer.Resolve<GameInput>().gameInputSystem;
            gameInput.Player.BindingKey.performed += OnPlayerBindingKey;
        }
            
        void Update()
        {
            StartBinding();
        }

        private void OnPlayerBindingKey(InputAction.CallbackContext callbackContext)
        {
            if(isBinding == false)
            {
                isBinding = true;
                SingletonContainer.Resolve<GameCanvas>().TurnOnKeyBindingPanel();
                keyBindingPanel = SingletonContainer.Resolve<KeyBindingPanel>();
            }
            else
            {
                isBinding = false;
                SingletonContainer.Resolve<GameCanvas>().TurnOffkeyBindingPanel();
            }
        }

        private void StartBinding()
        {
            if(isBinding)
            {
                if(gameInput.Player.Move.WasPressedThisFrame())
                {
                    InputControl activeControl = gameInput.Player.Move.activeControl;
                    Debug.LogWarning("Press: " + activeControl.displayName);
                    keyBindingPanel.SetOldKeyText(activeControl.displayName);
                    if(activeControl == gameInput.Player.Move.controls[0])
                    {
                        KeyBinding(Binding.Up);
                    }
                    if(activeControl == gameInput.Player.Move.controls[1])
                    {
                        KeyBinding(Binding.Down);
                    }
                    if(activeControl == gameInput.Player.Move.controls[2])
                    {
                        KeyBinding(Binding.Left);
                    }
                    if(activeControl == gameInput.Player.Move.controls[3])
                    {
                        KeyBinding(Binding.Right);
                    }
                }
                if(gameInput.Player.PickupOrThrow.WasPressedThisFrame())
                {
                    keyBindingPanel.SetOldKeyText(gameInput.Player.PickupOrThrow.activeControl.displayName);
                    KeyBinding(Binding.PickUpOrThrow);
                }
                if(gameInput.Player.SwordAttack.WasPressedThisFrame())
                {
                    keyBindingPanel.SetOldKeyText(gameInput.Player.SwordAttack.activeControl.displayName);
                    KeyBinding(Binding.SwordAttack);
                }
                if(gameInput.Player.Dash.WasPerformedThisFrame())
                {
                    keyBindingPanel.SetOldKeyText(gameInput.Player.Dash.activeControl.displayName);
                    KeyBinding(Binding.Dash);
                }
            }


            }
        public void KeyBinding(Binding binding)
        {
            switch(binding)
            {
                case Binding.Up:
                    gameInput.Player.Disable();

                    gameInput.Player.Move.PerformInteractiveRebinding(1).OnComplete(callback =>
                    {
                        // Debug.Log("OldKey:" + callback.action.bindings[1].path);
                        // Debug.Log("NewKey:" + callback.action.bindings[1].overridePath);
                        keyBindingPanel.SetNewKeyText(callback.action.bindings[1].ToDisplayString());
                        callback.Dispose();
                        gameInput.Player.Enable();
                    }).Start();
                    break;
                case Binding.Down:
                    gameInput.Player.Disable();

                    gameInput.Player.Move.PerformInteractiveRebinding(2).OnComplete(callback =>
                    {
                        callback.Dispose();
                        gameInput.Player.Enable();
                        keyBindingPanel.SetNewKeyText(callback.action.bindings[2].ToDisplayString());
                    }).Start();
                    break;
                case Binding.Left:
                    gameInput.Player.Disable();

                    gameInput.Player.Move.PerformInteractiveRebinding(3).OnComplete(callback =>
                    {
                        callback.Dispose();
                        gameInput.Player.Enable();
                        keyBindingPanel.SetNewKeyText(callback.action.bindings[3].ToDisplayString());
                    }).Start();
                    break;
                case Binding.Right:
                    gameInput.Player.Disable();

                    gameInput.Player.Move.PerformInteractiveRebinding(4).OnComplete(callback =>
                    {
                        callback.Dispose();
                        gameInput.Player.Enable();
                        keyBindingPanel.SetNewKeyText(callback.action.bindings[4].ToDisplayString());
                    }).Start();               
                    break;
                case Binding.Dash:
                    gameInput.Player.Disable();

                    gameInput.Player.Dash.PerformInteractiveRebinding(0).OnComplete(callback =>
                    {
                        callback.Dispose();
                        gameInput.Player.Enable();
                        keyBindingPanel.SetNewKeyText(callback.action.bindings[0].ToDisplayString());
                    }).Start();               
                    break;
                case Binding.SwordAttack:
                    gameInput.Player.Disable();

                    gameInput.Player.SwordAttack.PerformInteractiveRebinding(0).OnComplete(callback =>
                    {
                        callback.Dispose();
                        gameInput.Player.Enable();
                        keyBindingPanel.SetNewKeyText(callback.action.bindings[0].ToDisplayString());
                    }).Start();               
                    break;
                case Binding.PickUpOrThrow:
                    gameInput.Player.Disable();

                    gameInput.Player.PickupOrThrow.PerformInteractiveRebinding(0).OnComplete(callback =>
                    {
                        callback.Dispose();
                        gameInput.Player.Enable();
                        keyBindingPanel.SetNewKeyText(callback.action.bindings[0].ToDisplayString());
                    }).Start();            
                    break;
            }
        }

        public void RegisterToContainer()
        {
            SingletonContainer.Register(this);
        }

        public void UnRegisterFromContainer()
        {
            SingletonContainer.UnRegister(this);
        }
    }

}
