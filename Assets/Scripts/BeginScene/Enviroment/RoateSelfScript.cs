using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoateSelfScript : MonoBehaviour
{
    public float rotateSpeed = 10f;
    void Update()
    {
        this.transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }
}
