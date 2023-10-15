using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoateSelfScriptTank : MonoBehaviour
{
    public float rotateSpeed = 10f;
    void Update()
    {
        this.transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
        if (this.transform.localEulerAngles.y >= 45 && this.transform.localEulerAngles.y < 180 && rotateSpeed > 0)
        {
            rotateSpeed = -rotateSpeed;
        }
        if (this.transform.localEulerAngles.y <= 315 && this.transform.localEulerAngles.y > 180 && rotateSpeed < 0)
        {
            rotateSpeed = -rotateSpeed;
        }
    }
}
