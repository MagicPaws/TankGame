using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CustomGUIRoot : MonoBehaviour
{
    private CustomGUIControl[] controls;
    void Start()
    {
        controls = this.transform.GetComponentsInChildren<CustomGUIControl>();
    }

    private void OnGUI()
    {
        controls = this.transform.GetComponentsInChildren<CustomGUIControl>();
        for (int i = 0; i < controls.Length; i++)
        {
            controls[i].DrawGUI();
        }
    }
}
