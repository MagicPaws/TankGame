using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndEnemyTower : TankBase
{
    public Texture textureHpBg;
    public Texture textureHp;
    private Rect hpRect;

    private float hpShowTime = 0;

    private void Update()
    {
        head.transform.Rotate(Vector3.up * headRotateSpeed * Time.deltaTime);
    }
    private void OnGUI()
    {
        if (hpShowTime > 0)
        {
            hpShowTime -= Time.deltaTime;

            Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position);

            hpRect.height = 400 / screenPoint.z;
            hpRect.width = 1500 / screenPoint.z;
            hpRect.x = screenPoint.x - hpRect.width / 2;
            hpRect.y = Screen.height - screenPoint.y - hpRect.height * 2.5f;
            GUI.DrawTexture(hpRect, textureHpBg);
            hpRect.width *= (float)hp / maxHp;
            GUI.DrawTexture(hpRect, textureHp);
        }
    }
    public override void Fire()
    {

    }
    public override void Hurt(TankBase other)
    {
        base.Hurt(other);
        hpShowTime = 2;
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
        ShowWinPanel();
    }
    private void ShowWinPanel()
    {
        WinPanel.Instance.ShowMe();
    }
}
