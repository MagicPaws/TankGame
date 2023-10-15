using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomGUIToggle : CustomGUIControl
{
    public bool isSelect;
    public event UnityAction<bool> changeValueEvent;
    private bool oldSelect;
    protected override void StyleOffDraw()
    {
        isSelect = GUI.Toggle(pos.rect, isSelect, content);
        if (oldSelect != isSelect)
        {
            changeValueEvent?.Invoke(isSelect);
            oldSelect = isSelect;
        }
    }

    protected override void StyleOnDraw()
    {
        isSelect = GUI.Toggle(pos.rect, isSelect, content, style);
        if (oldSelect != isSelect)
        {
            changeValueEvent?.Invoke(isSelect);
            oldSelect = isSelect;
        }
    }
}
