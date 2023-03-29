using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : NOOD.MonoBehaviorInstance<GameInput>
{
    GameInputSystem gameInputSystem;

    // Start is called before the first frame update
    void Start()
    {
        gameInputSystem = new GameInputSystem();
        gameInputSystem.Player.Enable();
    }

    public Vector2 GetMoveDirection()
    {
        Vector2 direction = gameInputSystem.Player.Move.ReadValue<Vector2>();
        return direction;
    }
}
