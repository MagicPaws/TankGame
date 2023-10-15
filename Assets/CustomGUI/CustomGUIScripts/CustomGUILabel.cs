using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGUILabel : CustomGUIControl
{
    protected override void StyleOffDraw()
    {
        GUI.Label(pos.rect, content.text);
    }

    protected override void StyleOnDraw()
    {
        GUI.Label(pos.rect, content.text, style);
    }
}
