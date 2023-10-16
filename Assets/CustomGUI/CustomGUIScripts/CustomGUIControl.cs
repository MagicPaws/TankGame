using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_Style_OnOff
{
    On,
    Off,
}

public abstract class CustomGUIControl : MonoBehaviour
{
    // 控件位置信息
    public CustomGUIPos pos = new CustomGUIPos();
    // 内容信息
    public GUIContent content;
    // 是否开启自定义样式
    public E_Style_OnOff styleOnOrOff = E_Style_OnOff.Off;
    // 设置自定义样式
    public GUIStyle style;

    public void DrawGUI()
    {
        switch (styleOnOrOff)
        {
            case E_Style_OnOff.On:
                StyleOnDraw();
                break;
            case E_Style_OnOff.Off:
                StyleOffDraw();
                break;
        }
    }
    protected abstract void StyleOnDraw();
    protected abstract void StyleOffDraw();
}