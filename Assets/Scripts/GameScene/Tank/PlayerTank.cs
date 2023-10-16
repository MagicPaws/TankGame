using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTank : TankBase
{
    // 单例模式
    private static PlayerTank instance;
    public static PlayerTank Instance => instance;
    // 当前武器
    public CustomWeapon nowWeapon;
    // 当前武器索引
    public int nowWeaponIndex = -1;
    // 武器父物体
    public Transform weaponFather;

    // 摄像机位置
    public Transform camerPostion;
    // 最大摄像机高度
    public float cameraMaxHeight = 10f;
    // 最小摄像机高度
    public float cameraMinHeight = 5f;
    // 摄像机移动速度
    public float cameraMoveSpeed = 20f;
    // 摄像机上移比例
    public float cameraMoveUpRatio = 1f;
    // 摄像机下移比例
    public float cameraMoveDownRatio = 1f;
    // 当前时间
    private float time = 0f;
    private void Awake()
    {
        // 判断是否是单例
        if (instance == null)
        {
            instance = this;
        }
        // 获取摄像机
        if (Camera.main != null)
        {
            MainCameraControl control;
            // 判断摄像机是否有MainCameraControl组件
            if (Camera.main.TryGetComponent<MainCameraControl>(out control))
            {
                // 设置目标
                control.target = this.transform;
                // 设置玩家
                control.player = this;
            }
        }
    }
    private void Update()
    {
        // 按下空格键
        if (Input.GetKeyDown(KeyCode.Space) && Time.timeScale != 0)
        {
            // 发射子弹
            Fire();
        }
        // 打印输入轴
        print(Input.GetAxis("Vertical"));
        // 判断输入轴是否大于0.5f或者小于-0.5f
        if (Input.GetAxis("Vertical") >= 0.5f || Input.GetAxis("Vertical") <= -0.5f)
        {
            // 重置时间
            time = 0f;
            // 判断摄像机位置是否大于最大高度
            if (camerPostion.position.y >= cameraMaxHeight)
            {
                // 判断摄像机下移比例是否不等于1f
                if (cameraMoveDownRatio != 1f)
                {
                    // 设置摄像机下移比例为1f
                    cameraMoveDownRatio = 1f;
                }
            }
            else
            {
                // 摄像机上移
                camerPostion.Translate(Vector3.forward * -cameraMoveSpeed * Time.deltaTime * cameraMoveUpRatio);
                // 摄像机上移比例减减
                cameraMoveUpRatio -= Time.deltaTime;
                // 判断摄像机上移比例是否小于0.5f
                if (cameraMoveUpRatio < 0.5f)
                {
                    // 设置摄像机上移比例为0.5f
                    cameraMoveUpRatio = 0.5f;
                }
            }

        }
        else if (camerPostion.position.y >= cameraMinHeight && time < 0f)
        {
            // 摄像机下移
            camerPostion.Translate(Vector3.forward * cameraMoveSpeed * Time.deltaTime * cameraMoveDownRatio);
            // 摄像机下移比例减减
            cameraMoveDownRatio -= Time.deltaTime;
            // 判断摄像机下移比例是否小于0.5f
            if (cameraMoveDownRatio < 0.5f)
            {
                // 设置摄像机下移比例为0.5f
                cameraMoveDownRatio = 0.5f;
            }
            // 判断摄像机上移比例是否不等于1f
            if (cameraMoveUpRatio != 1f)
            {
                // 设置摄像机上移比例为1f
                cameraMoveUpRatio = 1f;
            }
        }
    }
    private void FixedUpdate()
    {
        // 当前时间减减
        time -= Time.fixedDeltaTime;
        // 移动
        transform.Translate(Input.GetAxis("Vertical") * transform.GetChild(0).forward * moveSpeed * Time.fixedDeltaTime);
        // 旋转
        transform.GetChild(0).Rotate(Input.GetAxis("Horizontal") * Vector3.up * rotateSpeed * Time.fixedDeltaTime);
        // 头部旋转
        head.transform.Rotate(Input.GetAxis("Mouse X") * Vector3.up * headRotateSpeed * Time.fixedDeltaTime);
        
    }
    public override void Hurt(TankBase other)
    {
        // 调用父类方法
        base.Hurt(other);
        // 更新游戏面板血量
        GamePanel.Instance.UpdateHp(maxHp, hp);
    }
    public override void Death()
    {
        // 设置对象不可见
        gameObject.SetActive(false);
        // 判断效果预设体是否不为空
        if (effectPrefab != null)
        {
            // 实例化效果预设体
            GameObject effectObj = Instantiate(effectPrefab, transform.position, transform.rotation);
            // 获取音频源
            AudioSource audioSource = effectObj.GetComponent<AudioSource>();
            // 设置音频源
            EffectManager.Instance.SetEffect(audioSource);
            // 播放音频
            audioSource.Play();
        }
        // 失败界面
        Invoke("ShowLosePanel", 1f);
    }
    private void ShowLosePanel()
    {
        // 显示失败界面
        LosePanel.Instance.ShowMe();
    }
    public override void Fire()
    {
        // 判断当前武器是否为空
        if (nowWeapon != null)
        {
            // 发射子弹
            nowWeapon.Fire();
        }
    }
    public void ChangeWeapon(GameObject weapon, int weaponIndex)
    {
        // 判断当前武器是否不为空
        if (nowWeapon != null)
        {
            // 销毁当前武器
            Destroy(nowWeapon.gameObject);
            // 设置当前武器为空
            nowWeapon = null;
        }
        // 实例化当前武器
        nowWeapon = Instantiate(weapon, weaponFather).GetComponent<CustomWeapon>();
        // 设置当前武器索引
        nowWeaponIndex = weaponIndex;
        // 设置当前武器父物体
        nowWeapon.SetFather(this);

        // 更新游戏面板武器
        GamePanel.Instance.ChangeWeapon(nowWeapon.WeaponIcon, nowWeapon.maxBulletNum, nowWeapon.reloadTime, nowWeapon.reloadAudioClip);
    }
}