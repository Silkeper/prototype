using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inputs : MonoBehaviour
{
    BoatControls inputActions;

    public Vector2 MoveVector;
    public bool ActionValue;
    public bool menufishValue;

    private void Awake()
    {
        inputActions = new BoatControls();
    }

    private void Update()
    {
        MoveVector = inputActions.Boat.Move.ReadValue<Vector2>();

        ActionValue = inputActions.Boat.Action.triggered;

        menufishValue = inputActions.Boat.FishMenu.triggered;
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
