using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInput inputs;
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    private void Awake()
    {
        inputs = new();
        inputs.Player.Enable();

        inputs.Player.Interaction.performed += Interaction_performed;
        inputs.Player.InteractAlternate.performed += InteractAlternate_performed;
    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interaction_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetInputVectorNomarlized()
    {
        Vector2 inputVector = inputs.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;
    }
}
