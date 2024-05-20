using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInput inputs;
    private void Awake()
    {
        inputs = new();
        inputs.Player.Enable();
    }
    public Vector2 GetInputVectorNomarlized()
    {
        Vector2 inputVector = inputs.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;
    }
}
