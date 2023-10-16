using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoateSelfScriptTank : MonoBehaviour
{
    // 旋转速度
    public float rotateSpeed = 10f;
    void Update()
    {
        // 旋转自身
        this.transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
        // 如果当前角度大于45度，小于180度，且旋转速度大于0，则将旋转速度取反
        if (this.transform.localEulerAngles.y >= 45 && this.transform.localEulerAngles.y < 180 && rotateSpeed > 0)
        {
            rotateSpeed = -rotateSpeed;
        }
        // 如果当前角度小于315度，大于180度，且旋转速度小于0，则将旋转速度取反
        if (this.transform.localEulerAngles.y <= 315 && this.transform.localEulerAngles.y > 180 && rotateSpeed < 0)
        {
            rotateSpeed = -rotateSpeed;
        }
    }
}