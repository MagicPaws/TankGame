using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetPlayer : NetworkBehaviour
{
    public override void OnStartLocalPlayer()
    {
        Camera.main.transform.SetParent(transform);
        Camera.main.transform.localPosition = new Vector3(0, 3, -8);
        Camera.main.transform.eulerAngles = new Vector3(30, 0, 0);
    }
    private void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        transform.Translate(Input.GetAxis("Vertical") * Vector3.forward * 10 * Time.deltaTime);
        transform.Translate(Input.GetAxis("Horizontal") * Vector3.right * 10 * Time.deltaTime);

        transform.Rotate(Input.GetAxis("Mouse X") * Vector3.up * 10 * Time.deltaTime);
    }
}
