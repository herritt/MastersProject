using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    InputControls controls;

    Vector2 leftThumbstickMovement;
    Vector2 rightThumbstickMovement;

    void Awake()
    {
        controls = new InputControls();

        controls.ActionMap.X.performed += ctx => Select();

        controls.ActionMap.LeftStick.performed += ctx => leftThumbstickMovement = ctx.ReadValue<Vector2>();
        controls.ActionMap.LeftStick.canceled += ctx => leftThumbstickMovement = Vector2.zero;

        controls.ActionMap.RightStick.performed += ctx => rightThumbstickMovement = ctx.ReadValue<Vector2>();
        controls.ActionMap.RightStick.canceled += ctx => rightThumbstickMovement = Vector2.zero;

    }

    void Select()
    {
        Debug.Log("Select");
    }

    void Update()
    {
        //process movement of left thumbstick
        Vector2 leftStickVector2 = new Vector2(leftThumbstickMovement.x, leftThumbstickMovement.y) * Time.deltaTime;

        //process movement of right thumbstick
        Vector2 rightStickVector2 = new Vector2(leftThumbstickMovement.x, leftThumbstickMovement.y) * Time.deltaTime;

        Debug.Log("left " + leftThumbstickMovement);
        Debug.Log("right " + rightThumbstickMovement);
    }

    void OnEnable()
    {
        controls.ActionMap.Enable();
      
    }
}
