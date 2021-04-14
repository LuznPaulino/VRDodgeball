using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class HandCtrl : MonoBehaviour
{
    public InputActionReference controllerActionGrip;
    public InputActionReference controllerActionTrigger;
    private Animator anim;

    void Start()
    {
        controllerActionGrip.action.performed += GripPressed;
        controllerActionTrigger.action.performed += TriggerPressed;
        controllerActionGrip.action.canceled += GripCancel;
        controllerActionTrigger.action.canceled += TriggerCancel;

        anim = GetComponent<Animator>();
    }

    private void TriggerCancel(InputAction.CallbackContext obj)
    {
        anim.SetFloat("Trigger", 0);
    }

    private void GripCancel(InputAction.CallbackContext obj)
    {
        anim.SetFloat("Grip", 0);
    }

    private void TriggerPressed(InputAction.CallbackContext obj)
    {
        anim.SetFloat("Trigger", obj.ReadValue<float>());
    }

    private void GripPressed(InputAction.CallbackContext obj)
    {
        anim.SetFloat("Grip", obj.ReadValue<float>());
    }
}
