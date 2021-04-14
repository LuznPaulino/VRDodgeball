using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit;

public class NetworkPlayer : MonoBehaviourPunCallbacks
{
    private PhotonView pView;
    public Transform VRHelmet, lHandCtrl, rHandCtrl;
    public Transform head, body, lHand, rHand;

    void Start()
    {
        pView = GetComponent<PhotonView>();

        XRRig playerRig = FindObjectOfType<XRRig>();
        VRHelmet = playerRig.transform.Find("Camera Offset/Main Camera");
        lHandCtrl = playerRig.transform.Find("Camera Offset/LeftHand Controller");
        rHandCtrl = playerRig.transform.Find("Camera Offset/RightHand Controller");

        if (pView.IsMine)
        {
            head.gameObject.SetActive(false);
            body.gameObject.SetActive(false);
            lHand.gameObject.SetActive(false);
            rHand.gameObject.SetActive(false);
        }

        body.GetComponent<Renderer>().material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }

    void Update()
    {
        if (pView.IsMine)
        {
            SetPositionAndRotation(head, VRHelmet);
            SetPositionAndRotation(lHand, lHandCtrl);
            SetPositionAndRotation(rHand, rHandCtrl);
            SetBodyPositionAndRotation(body, VRHelmet);
        }
    }

    void SetPositionAndRotation(Transform avatarPart, Transform device)
    {
        avatarPart.position = device.position;
        avatarPart.rotation = device.rotation;
    }

    void SetBodyPositionAndRotation(Transform avatarPart, Transform device)
    {
        avatarPart.position = new Vector3(device.position.x, device.position.y - 0.4f, device.position.z);
        avatarPart.rotation = Quaternion.Euler(0, device.rotation.y, 0);
    }
}