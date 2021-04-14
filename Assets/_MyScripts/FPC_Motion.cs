using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class FPC_Motion : MonoBehaviour
{
    private InputDevice lHandControllerDevice;
    private InputDevice rHandControllerDevice;
    public InputDeviceCharacteristics characteristicsLeft, characteristicsRight;
    private CharacterController FPC;
    private Vector2 joyStick;
    private XRRig myRig;
    public float speed = 1;
    public float jumpSpeed = 10.0f;
    private float movY = 0;
    private float gravity = 20f;
    private bool primBtbValue;

    void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(characteristicsLeft, devices);
        if (devices.Count > 0) lHandControllerDevice = devices[0];

        InputDevices.GetDevicesWithCharacteristics(characteristicsRight, devices);
        if (devices.Count > 0) rHandControllerDevice = devices[0];

        myRig = GetComponent<XRRig>();

        FPC = GetComponent<CharacterController>();

    }

    void Update()
    {
        lHandControllerDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out joyStick);
        rHandControllerDevice.TryGetFeatureValue(CommonUsages.primaryButton, out primBtbValue);
    }

    void FixedUpdate()
    {
        if (FPC.isGrounded && primBtbValue)
            movY = jumpSpeed;
        if (movY > -150)        // -150 seems like a good value to come down fast. -1 moves you down too slowly.
            movY -= gravity * Time.fixedDeltaTime;
        else
            movY = -150;

        Quaternion headRotation = Quaternion.Euler(0, myRig.cameraGameObject.transform.eulerAngles.y, 0);
        Vector3 moveDir = headRotation * new Vector3(joyStick.x, movY, joyStick.y);
        FPC.Move(moveDir * Time.fixedDeltaTime * speed);
    }
}
