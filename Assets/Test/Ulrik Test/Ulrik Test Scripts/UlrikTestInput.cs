using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UlrikTestInput : MonoBehaviour
{
    UlrikTestActions inputActions;

    public Vector2 MoveVector;
    public bool ActionValue;

    private void Awake()
    {
        inputActions = new UlrikTestActions();
    }

    private void Update()
    {
        MoveVector = inputActions.Player.Move.ReadValue<Vector2>();

        ActionValue = inputActions.Player.Action.triggered;
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }
}
