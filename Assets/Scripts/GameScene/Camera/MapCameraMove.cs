using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCameraMove : MonoBehaviour
{
    // 声明一个MainCameraControl类型的变量
    public MainCameraControl mainCamera;
    // 声明一个int类型的变量
    public int hight = 10;
    // 声明一个Vector3类型的变量
    private Vector3 pos;

    void LateUpdate()
    {
        // 如果mainCamera的target为空，则返回
        if (mainCamera.target == null)
            return;
        // 将pos的x赋值为mainCamera的target的position的x
        pos.x = mainCamera.target.position.x;
        // 将pos的y赋值为hight
        pos.y = hight;
        // 将pos的z赋值为mainCamera的target的position的z
        pos.z = mainCamera.target.position.z;
        // 将transform的位置赋值为pos
        transform.position = pos;
    }
}