using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum E_Input_Type
{
    Normal,
    Password,
}

public class CustomGUIInput : CustomGUIControl
{
    public E_Input_Type inputType = E_Input_Type.Normal;
    public event UnityAction<string> changeText;
    public string nowText;
    public char passwordReplaceSymbol = '*';
    private string frontStr = "";
    protected override void StyleOffDraw()
    {
        switch (inputType)
        {
            case E_Input_Type.Normal:
                nowText = GUI.TextField(pos.rect, nowText);
                if (nowText != frontStr)
                {
                    changeText?.Invoke(nowText);
                }
                break;
            case E_Input_Type.Password:
                nowText = GUI.PasswordField(pos.rect, nowText, passwordReplaceSymbol);
                if (nowText != frontStr)
                {
                    changeText?.Invoke(nowText);
                }
                break;
        }
    }

    protected override void StyleOnDraw()
    {
        switch (inputType)
        {
            case E_Input_Type.Normal:
                nowText = GUI.TextField(pos.rect, nowText, style);
                if (nowText != frontStr)
                {
                    changeText?.Invoke(nowText);
                }
                break;
            case E_Input_Type.Password:
                nowText = GUI.PasswordField(pos.rect, nowText, passwordReplaceSymbol, style);
                if (nowText != frontStr)
                {
                    changeText?.Invoke(nowText);
                }
                break;
        }
    }
}
