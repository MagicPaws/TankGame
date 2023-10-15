using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraControl : MonoBehaviour
{
    public Transform target;
    public PlayerTank player;
    private void LateUpdate()
    {
        if (player != null)
        {
            transform.position = player.camerPostion.position;
            transform.rotation = player.camerPostion.rotation;
        }
        
    }
}
