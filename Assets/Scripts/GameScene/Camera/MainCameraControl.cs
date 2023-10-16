using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraControl : MonoBehaviour
{
    // 目标Transform
    public Transform target;
    // 玩家Tank
    public PlayerTank player;
    private void LateUpdate()
    {
        // 如果玩家不为空
        if (player != null)
        {
            // 设置摄像机的位置等于玩家的位置
            transform.position = player.camerPostion.position;
            // 设置摄像机的旋转等于玩家的旋转
            transform.rotation = player.camerPostion.rotation;
        }
        
    }
}