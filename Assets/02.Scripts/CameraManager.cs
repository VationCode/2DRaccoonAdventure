using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    CinemachineVirtualCamera VirtualCamera;
    private void Awake()
    {
        VirtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
    }
    void Start()
    {
        VirtualCamera.Follow = Player.Instance.gameObject.transform;
    }
}
