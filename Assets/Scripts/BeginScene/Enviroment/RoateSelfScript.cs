using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoateSelfScript : MonoBehaviour
{
    // 旋转速度
    public float rotateSpeed = 10f;
    void Update()
    {
        // 旋转自身
        this.transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }
}