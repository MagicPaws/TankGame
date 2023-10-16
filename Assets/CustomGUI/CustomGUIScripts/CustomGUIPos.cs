using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_Alignment_Type
{
    Up,
    Down,
    Left,
    Right,
    Center,
    Left_Up,
    Right_Up,
    Left_Down,
    Right_Down,
}

[System.Serializable]
public class CustomGUIPos
{
    // 控件相对屏幕对齐方式
    public E_Alignment_Type screen_Alignment_Type = E_Alignment_Type.Center;
    // 控件自身中心对齐方式
    public E_Alignment_Type control_Center_Alignment_Type = E_Alignment_Type.Center;
    // 控件的宽
    public float width = 100;
    // 控件的高
    public float height = 50;
    // 控件的偏移坐标
    public Vector2 offset = new Vector2(0, 0);

    // 控件在屏幕上的真正Rect值
    private Rect rRect = new Rect(0, 0, 100, 50);
    // 控件中心点偏移位置
    private Vector2 centerPos = new Vector2(0, 0);

    // 得到控件的真正Rect值
    public Rect rect
    {
        get
        {
            // 根据当前控件中心的对齐方式计算出控件中心点的偏移坐标;
            CalculateCenterPos();
            // 根据当前控件相对屏幕的对齐方式和控件中心点的偏移坐标及控件的偏移位置计算出控件在屏幕中真正的Rect值
            CalculateRect();
            rRect.width = width;
            rRect.height = height;
            return rRect;
        }
    }

    private void CalculateCenterPos()
    {
        switch (control_Center_Alignment_Type)
        {
            case E_Alignment_Type.Up:
                centerPos.x = -width / 2;
                centerPos.y = 0;
                break;
            case E_Alignment_Type.Down:
                centerPos.x = -width / 2;
                centerPos.y = -height;
                break;
            case E_Alignment_Type.Left:
                centerPos.x = 0;
                centerPos.y = -height / 2;
                break;
            case E_Alignment_Type.Right:
                centerPos.x = -width;
                centerPos.y = -height / 2;
                break;
            case E_Alignment_Type.Center:
                centerPos.x = -width / 2;
                centerPos.y = -height / 2;
                break;
            case E_Alignment_Type.Left_Up:
                centerPos.x = 0;
                centerPos.y = 0;
                break;
            case E_Alignment_Type.Right_Up:
                centerPos.x = -width;
                centerPos.y = 0;
                break;
            case E_Alignment_Type.Left_Down:
                centerPos.x = 0;
                centerPos.y = -height;
                break;
            case E_Alignment_Type.Right_Down:
                centerPos.x = -width;
                centerPos.y = -height;
                break;
        }
    }
    private void CalculateRect()
    {
        switch (screen_Alignment_Type)
        {
            case E_Alignment_Type.Up:
                rRect.x = Screen.width / 2 + centerPos.x + offset.x;
                rRect.y = 0 + centerPos.y + offset.y;
                break;
            case E_Alignment_Type.Down:
                rRect.x = Screen.width / 2 + centerPos.x + offset.x;
                rRect.y = Screen.height + centerPos.y - offset.y;
                break;
            case E_Alignment_Type.Left:
                rRect.x = 0 + centerPos.x + offset.x;
                rRect.y = Screen.height / 2 + centerPos.y + offset.y;
                break;
            case E_Alignment_Type.Right:
                rRect.x = Screen.width + centerPos.x - offset.x;
                rRect.y = Screen.height / 2 + centerPos.y + offset.y;
                break;
            case E_Alignment_Type.Center:
                rRect.x = Screen.width / 2 + centerPos.x + offset.x;
                rRect.y = Screen.height / 2 + centerPos.y + offset.y;
                break;
            case E_Alignment_Type.Left_Up:
                rRect.x = 0 + centerPos.x + offset.x;
                rRect.y = 0 + centerPos.y + offset.y;
                break;
            case E_Alignment_Type.Right_Up:
                rRect.x = Screen.width + centerPos.x - offset.x;
                rRect.y = 0 + centerPos.y + offset.y;
                break;
            case E_Alignment_Type.Left_Down:
                rRect.x = 0 + centerPos.x + offset.x;
                rRect.y = Screen.height + centerPos.y - offset.y;
                break;
            case E_Alignment_Type.Right_Down:
                rRect.x = Screen.width + centerPos.x - offset.x;
                rRect.y = Screen.height + centerPos.y - offset.y;
                break;
        }
    }
}