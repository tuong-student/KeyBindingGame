using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.System;

public class KeyBindingManager : MonoBehaviour, ISingleton
{
    private static GameInputSystem gameInput;
    private Dictionary<Binding, string> bindingDictionary = new Dictionary<Binding, string>();

    void Awake()
    {
        RegisterToContainer();
    }

    void Start()
    {
        gameInput = SingletonContainer.Resolve<GameInput>().gameInputSystem;
        gameInput.Player.BindingKey.performed += OnPlayerBindingKey;
    }

    public enum Binding
    {
        Up,
        Down,
        Left,
        Right,
        Dash,
        Attack,
        PickUpOrThrow
    }

    private void GetAllBindingKey()
    {
        
    }

    private void ResetKeyBind()
    {
        
    }

    private void OnPlayerBindingKey(InputAction.CallbackContext callbackContext)
    {
        bool isBinding = true;
        if(isBinding)
        {
            gameInput.Player.Dash.started += OnPlayerBindingDash;

            gameInput.Player.Dash.started -= OnPlayerBindingDash;
        }
    }

    private void OnPlayerBindingDash(InputAction.CallbackContext callbackContext)
    {
        KeyBinding(Binding.Dash);
    }


    public static void KeyBinding(string action)
    {
        switch (action)
        {
            case "Dash":
                Debug.Log("Action Dash");
                break;
        }
    }

    public static void KeyBinding(Binding binding)
    {
        switch(binding)
        {
            case Binding.Up:
                gameInput.Player.Disable();

                gameInput.Player.Move.PerformInteractiveRebinding(1).OnComplete(callback =>
                {
                    Debug.Log("OldKey:" + callback.action.bindings[1].path);
                    Debug.Log("NewKey:" + callback.action.bindings[1].overridePath);
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
                }).Start();
                break;
            case Binding.Left:
                gameInput.Player.Disable();

                gameInput.Player.Move.PerformInteractiveRebinding(3).OnComplete(callback =>
                {
                    callback.Dispose();
                    gameInput.Player.Enable();
                }).Start();
                break;
            case Binding.Right:
                gameInput.Player.Disable();

                gameInput.Player.Move.PerformInteractiveRebinding(4).OnComplete(callback =>
                {
                    callback.Dispose();
                    gameInput.Player.Enable();
                }).Start();               
                break;
            case Binding.Dash:
                gameInput.Player.Disable();

                gameInput.Player.Dash.PerformInteractiveRebinding(0).OnComplete(callback =>
                {
                    callback.Dispose();
                    gameInput.Player.Enable();
                }).Start();               
                break;
            case Binding.Attack:
                gameInput.Player.Disable();

                gameInput.Player.SwordAttack.PerformInteractiveRebinding(0).OnComplete(callback =>
                {
                    callback.Dispose();
                    gameInput.Player.Enable();
                }).Start();               
                break;
            case Binding.PickUpOrThrow:
                gameInput.Player.Disable();

                gameInput.Player.PickupOrThrow.PerformInteractiveRebinding(0).OnComplete(callback =>
                {
                    callback.Dispose();
                    gameInput.Player.Enable();
                }).Start();            
                break;
        }
    }

    public void ResetKeyBinding(Binding binding)
    {
        switch(binding)
        {
            case Binding.Up:
                gameInput.Player.Move.ApplyBindingOverride(1, "<keyboard>/p");
                break;
            case Binding.Down:
                break;
            case Binding.Left:
                break;
            case Binding.Right:
                break;
            case Binding.Dash:
                break;
            case Binding.Attack:
                break;
            case Binding.PickUpOrThrow:
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
