using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum E_Slider_Type
{
    Horizontal,
    Vertical,
}

public class CustomGUISlider : CustomGUIControl
{
    public float minValue = 0f;
    public float maxValue = 1f;
    public float nowValue = 1f;
    public E_Slider_Type type = E_Slider_Type.Horizontal;
    public GUIStyle thumbStyle;

    private float frontValue = 0;

    public event UnityAction<float> changeValueEvent;
    protected override void StyleOffDraw()
    {
        switch (type)
        {
            case E_Slider_Type.Horizontal:
                nowValue = GUI.HorizontalSlider(pos.rect, nowValue, minValue, maxValue);
                if (nowValue != frontValue)
                {
                    changeValueEvent?.Invoke(nowValue);
                    frontValue = nowValue;
                }
                break;
            case E_Slider_Type.Vertical:
                nowValue = GUI.VerticalSlider(pos.rect, nowValue, minValue, maxValue);
                if (nowValue != frontValue)
                {
                    changeValueEvent?.Invoke(nowValue);
                    frontValue = nowValue;
                }
                break;
        }
    }

    protected override void StyleOnDraw()
    {
        switch (type)
        {
            case E_Slider_Type.Horizontal:
                nowValue = GUI.HorizontalSlider(pos.rect, nowValue, minValue, maxValue, style, thumbStyle);
                if (nowValue != frontValue)
                {
                    changeValueEvent?.Invoke(nowValue);
                    frontValue = nowValue;
                }
                break;
            case E_Slider_Type.Vertical:
                nowValue = GUI.VerticalSlider(pos.rect, nowValue, minValue, maxValue, style, thumbStyle);
                if (nowValue != frontValue)
                {
                    changeValueEvent?.Invoke(nowValue);
                    frontValue = nowValue;
                }
                break;
        }
    }
}
