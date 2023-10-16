using System;
using Unity.VisualScripting;
using UnityEngine;
using Update = UnityEngine.PlayerLoop.Update;

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
    private float verticalInput;
    private float horizontalInput;
    public Transform tankBody;

    private void Awake()
    {
        // 单例初始化
        if (instance == null)
        {
            instance = this;
        }
    }

    private void FixedUpdate()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        // 移动坦克
        transform.Translate(Vector3.forward * (verticalInput * moveSpeed * Time.fixedDeltaTime));
        transform.Translate(Vector3.right * (horizontalInput * moveSpeed * Time.fixedDeltaTime));

        // 旋转tankBody
        float targetRotation = 0;

        if (verticalInput > 0)
        {
            if (horizontalInput > 0)
                // W + D，45度
                targetRotation = 45;
            else if (horizontalInput < 0)
                // W + A，-45度
                targetRotation = -45;
            else
                // W键，0度
                targetRotation = 0;
        }
        else if (verticalInput < 0)
        {
            if (horizontalInput > 0)
                // S + D，135度
                targetRotation = 135;
            else if (horizontalInput < 0)
                // S + A，-135度
                targetRotation = -135;
            else
                // S键，180度
                targetRotation = 180;
        }
        else if (horizontalInput > 0)
        {
            // D键，90度
            targetRotation = 90;
        }
        else if (horizontalInput < 0)
        {
            // A键，-90度
            targetRotation = -90;
        }

        // 逐渐旋转tankBody
        var rotationSpeed = 120.0f; // 旋转速度，可以根据需要调整

        if (verticalInput != 0 || horizontalInput != 0)
            tankBody.rotation = Quaternion.RotateTowards(tankBody.rotation, Quaternion.Euler(0, targetRotation, 0),
                rotationSpeed * Time.deltaTime);
        // 让tankHead始终朝向鼠标
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var groundPlane = new Plane(Vector3.up, Vector3.zero);
        float distance;
        if (groundPlane.Raycast(ray, out distance))
        {
            var targetPoint = ray.GetPoint(distance);
            targetPoint.y = 0; // 将目标点限制在y=0平面上
            head.LookAt(targetPoint);
        }
    }

    private void Update()
    {
        // 按下空格键
        if (Input.GetMouseButtonDown(0) && Time.timeScale != 0)
        {
            // 发射子弹
            Fire();
        }
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
        GamePanel.Instance.ChangeWeapon(nowWeapon.WeaponIcon, nowWeapon.maxBulletNum, nowWeapon.reloadTime,
            nowWeapon.reloadAudioClip);
    }
}