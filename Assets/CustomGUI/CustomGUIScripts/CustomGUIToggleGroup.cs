using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGUIToggleGroup : MonoBehaviour
{
    public CustomGUIToggle[] toggles;
    private CustomGUIToggle frontToggle;

    private void Start()
    {
        if (toggles == null)
        {
            return;
        }
        for (int i = 0; i < toggles.Length; i++)
        {
            CustomGUIToggle toggle = toggles[i];
            toggle.changeValueEvent += (value) =>
            {
                if (value)
                {
                    for (int j = 0; j < toggles.Length; j++)
                    {
                        if (toggles[j] != toggle)
                        {
                            toggles[j].isSelect = false;
                        }
                    }
                    frontToggle = toggle;
                }
                else
                {
                    if (frontToggle == toggle)
                    {
                        toggle.isSelect = true;
                    }
                }
            };
        }
    }
}
