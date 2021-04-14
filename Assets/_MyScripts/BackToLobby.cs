using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Photon.Pun;

public class BackToLobby : MonoBehaviourPunCallbacks
{
    public InputHelpers.Button btn = InputHelpers.Button.MenuButton;
    public XRNode controller = XRNode.LeftHand;

    void Update()
    {
        InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(controller), btn, out bool isPressed);
        if (isPressed)
        {
            PhotonNetwork.Disconnect();
            PhotonNetwork.LoadLevel(0);
        }
    }
}
