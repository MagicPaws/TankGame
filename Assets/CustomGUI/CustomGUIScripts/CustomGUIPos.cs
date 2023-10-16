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
    // �ؼ������Ļ���뷽ʽ
    public E_Alignment_Type screen_Alignment_Type = E_Alignment_Type.Center;
    // �ؼ��������Ķ��뷽ʽ
    public E_Alignment_Type control_Center_Alignment_Type = E_Alignment_Type.Center;
    // �ؼ��Ŀ�
    public float width = 100;
    // �ؼ��ĸ�
    public float height = 50;
    // �ؼ���ƫ������
    public Vector2 offset = new Vector2(0, 0);

    // �ؼ�����Ļ�ϵ�����Rectֵ
    private Rect rRect = new Rect(0, 0, 100, 50);
    // �ؼ����ĵ�ƫ��λ��
    private Vector2 centerPos = new Vector2(0, 0);

    // �õ��ؼ�������Rectֵ
    public Rect rect
    {
        get
        {
            // ���ݵ�ǰ�ؼ����ĵĶ��뷽ʽ������ؼ����ĵ��ƫ������;
            CalculateCenterPos();
            // ���ݵ�ǰ�ؼ������Ļ�Ķ��뷽ʽ�Ϳؼ����ĵ��ƫ�����꼰�ؼ���ƫ��λ�ü�����ؼ�����Ļ��������Rectֵ
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