using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCameraMove : MonoBehaviour
{
    public MainCameraControl mainCamera;
    public int hight = 10;
    private Vector3 pos;

    void LateUpdate()
    {
        if (mainCamera.target == null)
            return;
        pos.x = mainCamera.target.position.x;
        pos.y = hight;
        pos.z = mainCamera.target.position.z;
        transform.position = pos;
    }
}
