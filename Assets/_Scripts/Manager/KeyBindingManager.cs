using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public enum BindingStage
    {
        Idle,
        Prepare,
        Start,
        InProgress,
        Complete,
        Cancel
    }

    public class KeyBindingManager : MonoBehaviour, ISingleton
    {
        private static GameInputSystem gameInput;
        private KeyBindingPanel keyBindingPanel;
        private bool isBinding = false;
        private List<InputControl> allBindings = new List<InputControl>();
        private BindingStage bindingStage;
        private Binding keyBindingType;

        void Awake()
        {
            RegisterToContainer();
        }

        void Start()
        {
            gameInput = SingletonContainer.Resolve<GameInput>().gameInputSystem;
            gameInput.Player.BindingKey.performed += OnPlayerBindingKey;
            GetAllBinding();
            bindingStage = BindingStage.Idle;
        }
            
        void Update()
        {
            switch(bindingStage)
            {
                case BindingStage.Prepare:
                    BindingKey();
                    break;
                case BindingStage.Start:
                    gameInput.Player.Disable();
                    gameInput.Player.BindingKey.Enable();
                    this.KeyBinding(keyBindingType);
                    bindingStage = BindingStage.InProgress;
                    break;
                case BindingStage.InProgress:
                case BindingStage.Cancel:
                    break;
                case BindingStage.Complete:
                    bindingStage = BindingStage.Idle;
                    GetAllBinding();
                    break;
            }
        }

        private List<InputControl> GetAllBinding()
        {
            allBindings = new List<InputControl>();
            foreach(var action in gameInput.Player.Get().actions)
            {
                foreach(var control in action.controls)
                {
                    allBindings.Add(control);
                }
            }
            return allBindings; 
        }

        private void OnPlayerBindingKey(InputAction.CallbackContext callbackContext)
        {
            if(isBinding == false)
            {
                isBinding = true;
                SingletonContainer.Resolve<GameCanvas>().TurnOnKeyBindingPanel();
                keyBindingPanel = SingletonContainer.Resolve<KeyBindingPanel>();
                bindingStage = BindingStage.Prepare;
            }
            else
            {
                isBinding = false;
                gameInput.Player.Enable();
                SingletonContainer.Resolve<GameCanvas>().TurnOffkeyBindingPanel();
            }
        }

        private void BindingKey()
        {
            if(bindingStage == BindingStage.Prepare)
            {
                if(gameInput.Player.Move.WasPressedThisFrame())
                {
                    InputControl activeControl = gameInput.Player.Move.activeControl;
                    keyBindingPanel.SetOldKeyText(activeControl.displayName);
                    if(activeControl == gameInput.Player.Move.controls[0])
                    {
                        bindingStage = BindingStage.Start;
                        keyBindingType = Binding.Up;
                    }
                    if(activeControl == gameInput.Player.Move.controls[1])
                    {
                        bindingStage = BindingStage.Start;
                        keyBindingType = Binding.Down;
                    }
                    if(activeControl == gameInput.Player.Move.controls[2])
                    {
                        bindingStage = BindingStage.Start;
                        keyBindingType = Binding.Left;
                    }
                    if(activeControl == gameInput.Player.Move.controls[3])
                    {
                        bindingStage = BindingStage.Start;
                        keyBindingType = Binding.Right;
                    }
                }
                if(gameInput.Player.PickupOrThrow.WasPressedThisFrame())
                {
                    keyBindingPanel.SetOldKeyText(gameInput.Player.PickupOrThrow.activeControl.displayName);
                    bindingStage = BindingStage.Start;
                    keyBindingType = Binding.PickUpOrThrow;
                }
                if(gameInput.Player.SwordAttack.WasPressedThisFrame())
                {
                    keyBindingPanel.SetOldKeyText(gameInput.Player.SwordAttack.activeControl.displayName);
                    bindingStage = BindingStage.Start;
                    keyBindingType = Binding.SwordAttack;
                }
                if(gameInput.Player.Dash.WasPerformedThisFrame())
                {
                    keyBindingPanel.SetOldKeyText(gameInput.Player.Dash.activeControl.displayName);
                    bindingStage = BindingStage.Start;
                    keyBindingType = Binding.Dash;
                }
            }

        }

        public string CheckIfPathValid(InputControl control, InputAction action, int bindingIndex)
        {
            string path = action.controls[bindingIndex].path;
            string overridePath = action.bindings[bindingIndex].overridePath;
            string newPath = (overridePath != null) ? overridePath : path;
            Debug.LogWarning(control.path);
            Debug.LogWarning(allBindings.Select(x => x.path).Contains(control.path));
            
            if(allBindings.Select(x => x.path).Contains(control.path))
            {
                bindingStage = BindingStage.Cancel;
            }
            else
            {
                newPath = control.path;
            }
            Debug.Log("path: " + path + " overridePath: " + overridePath + " newPath: " + newPath);

            return newPath;
        }

        public void KeyBinding(Binding binding)
        {
            switch(binding)
            {
                case Binding.Up:
                    InputActionRebindingExtensions.RebindingOperation rebinding = gameInput.Player.Move.PerformInteractiveRebinding(1);
                    rebinding.OnGeneratePath(callback => CheckIfPathValid(callback, rebinding.action, 0));
                    rebinding.OnComplete(callback =>
                    {
                        if(bindingStage != BindingStage.Cancel)
                        {
                            keyBindingPanel.SetNewKeyText(callback.action.controls[0].displayName);
                        }
                        // Debug.Log("OldKey:" + callback.action.bindings[1].path);
                        // Debug.Log("NewKey:" + callback.action.bindings[1].overridePath);
                        callback.Dispose();
                        bindingStage = BindingStage.Complete;
                    });
                    rebinding.Start();
                    break;
                case Binding.Down:
                    rebinding = gameInput.Player.Move.PerformInteractiveRebinding(2);
                    rebinding.OnGeneratePath(callback => CheckIfPathValid(callback, rebinding.action, 1));
                    rebinding.OnComplete(callback =>
                    {
                        if(bindingStage != BindingStage.Cancel)
                        {
                            keyBindingPanel.SetNewKeyText(callback.action.bindings[1].ToDisplayString());
                        }
                        callback.Dispose();
                        bindingStage = BindingStage.Complete;
                    });
                    rebinding.Start();
                    break;
                case Binding.Left:
                    rebinding = gameInput.Player.Move.PerformInteractiveRebinding(3);
                    rebinding.OnGeneratePath(callback => CheckIfPathValid(callback, rebinding.action, 2));
                    rebinding.OnComplete(callback =>
                    {
                        if(bindingStage != BindingStage.Cancel)
                        {
                            keyBindingPanel.SetNewKeyText(callback.action.bindings[2].ToDisplayString());
                        }
                        callback.Dispose();
                        bindingStage = BindingStage.Complete;
                    });
                    rebinding.Start();
                    break;
                case Binding.Right:
                    rebinding = gameInput.Player.Move.PerformInteractiveRebinding(4);
                    rebinding.OnGeneratePath(callback => CheckIfPathValid(callback, rebinding.action, 3));
                    rebinding.OnComplete(callback =>
                    {
                        if(bindingStage != BindingStage.Cancel)
                        {
                            keyBindingPanel.SetNewKeyText(callback.action.bindings[3].ToDisplayString());
                        }
                        callback.Dispose();
                        bindingStage = BindingStage.Complete;
                    });
                    rebinding.Start();               
                    break;
                case Binding.Dash:
                    rebinding = gameInput.Player.Dash.PerformInteractiveRebinding(0);
                    rebinding.OnGeneratePath(callback => CheckIfPathValid(callback, rebinding.action, 0));
                    rebinding.OnComplete(callback =>
                    {
                        if(bindingStage != BindingStage.Cancel)
                        {
                            keyBindingPanel.SetNewKeyText(callback.action.bindings[0].ToDisplayString());
                        }
                        callback.Dispose();
                        bindingStage = BindingStage.Complete;
                    });
                    rebinding.Start();               
                    break;
                case Binding.SwordAttack:
                    rebinding = gameInput.Player.SwordAttack.PerformInteractiveRebinding(0);
                    rebinding.OnGeneratePath(callback => CheckIfPathValid(callback, rebinding.action, 0));
                    rebinding.OnComplete(callback =>
                    {
                        if(bindingStage != BindingStage.Cancel)
                        {
                            keyBindingPanel.SetNewKeyText(callback.action.bindings[0].ToDisplayString());
                        }
                        callback.Dispose();
                        bindingStage = BindingStage.Complete;
                    });
                    rebinding.Start();                
                    break;
                case Binding.PickUpOrThrow:
                    rebinding = gameInput.Player.PickupOrThrow.PerformInteractiveRebinding(0);
                    rebinding.OnGeneratePath(callback => CheckIfPathValid(callback, rebinding.action, 0));
                    rebinding.OnComplete(callback =>
                    {
                        if(bindingStage != BindingStage.Cancel)
                        {
                            keyBindingPanel.SetNewKeyText(callback.action.bindings[0].ToDisplayString());
                        }
                        callback.Dispose();
                        bindingStage = BindingStage.Complete;
                    });
                    rebinding.Start(); 
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
