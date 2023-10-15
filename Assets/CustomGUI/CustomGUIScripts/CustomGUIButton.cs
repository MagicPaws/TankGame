using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomGUIButton : CustomGUIControl
{
    public event UnityAction clickEvent;
    protected override void StyleOffDraw()
    {
        if (GUI.Button(pos.rect, content))
        {
            clickEvent?.Invoke();
        }
    }

    protected override void StyleOnDraw()
    {
        if (GUI.Button(pos.rect, content, style))
        {
            clickEvent?.Invoke();
        }
    }
}
