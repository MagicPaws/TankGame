using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_AlphaBlendOnOrOff
{
    On,
    Off,
}

public class CustomGUITexture : CustomGUIControl
{
    public ScaleMode scaleMode = ScaleMode.StretchToFill;
    public E_AlphaBlendOnOrOff alphaBlendOnOrOff;
    public float imageAspect = 0f;
    protected override void StyleOffDraw()
    {
        GUI.DrawTexture(pos.rect, content.image, scaleMode, (int)alphaBlendOnOrOff == 0 ? true : false, imageAspect);
    }

    protected override void StyleOnDraw()
    {
        GUI.DrawTexture(pos.rect, content.image, scaleMode, (int)alphaBlendOnOrOff == 0 ? true : false, imageAspect);
    }
}
