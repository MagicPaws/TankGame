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
    // �ؼ�λ����Ϣ
    public CustomGUIPos pos = new CustomGUIPos();
    // ������Ϣ
    public GUIContent content;
    // �Ƿ����Զ�����ʽ
    public E_Style_OnOff styleOnOrOff = E_Style_OnOff.Off;
    // �����Զ�����ʽ
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