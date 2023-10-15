using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTank : TankBase
{
    private static PlayerTank instance;
    public static PlayerTank Instance => instance;
    public CustomWeapon nowWeapon;
    public int nowWeaponIndex = -1;
    public Transform weaponFather;

    public Transform camerPostion;
    public float cameraMaxHeight = 10f;
    public float cameraMinHeight = 5f;
    public float cameraMoveSpeed = 20f;
    public float cameraMoveUpRatio = 1f;
    public float cameraMoveDownRatio = 1f;
    private float time = 0f;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        if (Camera.main != null)
        {
            MainCameraControl control;
            if (Camera.main.TryGetComponent<MainCameraControl>(out control))
            {
                control.target = this.transform;
                control.player = this;
            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.timeScale != 0)
        {
            Fire();
        }
        print(Input.GetAxis("Vertical"));
        if (Input.GetAxis("Vertical") >= 0.5f || Input.GetAxis("Vertical") <= -0.5f)
        {
            time = 0f;
            if (camerPostion.position.y >= cameraMaxHeight)
            {
                if (cameraMoveDownRatio != 1f)
                {
                    cameraMoveDownRatio = 1f;
                }
            }
            else
            {
                camerPostion.Translate(Vector3.forward * -cameraMoveSpeed * Time.deltaTime * cameraMoveUpRatio);
                cameraMoveUpRatio -= Time.deltaTime;
                if (cameraMoveUpRatio < 0.5f)
                {
                    cameraMoveUpRatio = 0.5f;
                }
            }

        }
        else if (camerPostion.position.y >= cameraMinHeight && time < 0f)
        {
            camerPostion.Translate(Vector3.forward * cameraMoveSpeed * Time.deltaTime * cameraMoveDownRatio);
            cameraMoveDownRatio -= Time.deltaTime;
            if (cameraMoveDownRatio < 0.5f)
            {
                cameraMoveDownRatio = 0.5f;
            }
            if (cameraMoveUpRatio != 1f)
            {
                cameraMoveUpRatio = 1f;
            }
        }
    }
    private void FixedUpdate()
    {
        time -= Time.fixedDeltaTime;
        transform.Translate(Input.GetAxis("Vertical") * transform.GetChild(0).forward * moveSpeed * Time.fixedDeltaTime);
        transform.GetChild(0).Rotate(Input.GetAxis("Horizontal") * Vector3.up * rotateSpeed * Time.fixedDeltaTime);
        head.transform.Rotate(Input.GetAxis("Mouse X") * Vector3.up * headRotateSpeed * Time.fixedDeltaTime);
        
    }
    public override void Hurt(TankBase other)
    {
        base.Hurt(other);
        // 更新游戏面板血量
        GamePanel.Instance.UpdateHp(maxHp, hp);
    }
    public override void Death()
    {
        gameObject.SetActive(false);
        if (effectPrefab != null)
        {
            GameObject effectObj = Instantiate(effectPrefab, transform.position, transform.rotation);
            AudioSource audioSource = effectObj.GetComponent<AudioSource>();
            EffectManager.Instance.SetEffect(audioSource);
            audioSource.Play();
        }
        // 失败界面
        Invoke("ShowLosePanel", 1f);
    }
    private void ShowLosePanel()
    {
        LosePanel.Instance.ShowMe();
    }
    public override void Fire()
    {
        if (nowWeapon != null)
        {
            nowWeapon.Fire();
        }
    }
    public void ChangeWeapon(GameObject weapon, int weaponIndex)
    {
        if (nowWeapon != null)
        {
            Destroy(nowWeapon.gameObject);
            nowWeapon = null;
        }
        nowWeapon = Instantiate(weapon, weaponFather).GetComponent<CustomWeapon>();
        nowWeaponIndex = weaponIndex;
        nowWeapon.SetFather(this);

        GamePanel.Instance.ChangeWeapon(nowWeapon.WeaponIcon, nowWeapon.maxBulletNum, nowWeapon.reloadTime, nowWeapon.reloadAudioClip);
    }
}
