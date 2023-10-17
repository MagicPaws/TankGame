using System;
using Unity.VisualScripting;
using UnityEngine;

public class Ground : MonoBehaviour
{
    /// <summary>
    /// 存储当前ground的八方位ground对象
    /// 从正右开始，第一个方块为0，顺时针旋转，编号依次递增
    /// </summary>
    public GameObject[] grounds;
    /// <summary>
    /// 存储当前ground的四方位colliders对象
    /// 从正右开始，第一个方块为0，顺时针旋转，编号依次递增
    /// </summary>
    public BoxCollider[] colliders;

    private void OnTriggerEnter(Collider other)
    {
        //不是玩家碰撞 早返回
        if(!other.CompareTag("Player")) return;
        //判断碰撞的是哪一个碰撞盒
        int index = -1;
        for (int i = 0; i < colliders.Length; i++)
        {
            if (other.bounds.Intersects(colliders[i].bounds))
            {
                index = i;
                break;
            }
        }

        Vector3 spawnPos0 = this.gameObject.transform.position;
        Vector3 spawnPos1 = this.gameObject.transform.position;
        Vector3 spawnPos2 = this.gameObject.transform.position;
        switch (index)
        {
            //右侧
            case 0:
                //右上角位置
                spawnPos0.x += 100;
                spawnPos0.z += 100;
                //正右侧
                spawnPos1.x += 100;
                //右下角位置
                spawnPos2.x += 100;
                spawnPos2.z -= 100;
                //右上角位置 
                if (!grounds[7])
                {
                    GameObject obj = GameObject.Instantiate(this.gameObject,spawnPos0,Quaternion.identity);
                    grounds[7] = obj;
                }
                if (!grounds[0])
                {
                    //正右侧
                    GameObject obj = Instantiate(this.gameObject,spawnPos1,Quaternion.identity);
                    grounds[0] = obj;
                }
                //右下角位置
                if (!grounds[1])
                {
                    GameObject obj = Instantiate(this.gameObject,spawnPos2,Quaternion.identity);
                    grounds[1] = obj;
                }
                break;
            //下侧
            case 1:
                //右下角
                spawnPos0.z -= 100;
                spawnPos0.x += 100;
                //下侧
                spawnPos1.z -= 100;
                //左下角
                spawnPos2.x -= 100;
                spawnPos2.z -= 100;
                //右下角
                if (!grounds[1])
                {
                    GameObject obj = Instantiate(this.gameObject,spawnPos0,Quaternion.identity);
                    grounds[1] = obj;
                }
                //下侧
                if (!grounds[2])
                {
                    GameObject obj = Instantiate(this.gameObject,spawnPos1,Quaternion.identity);
                    grounds[2] = obj;
                }
                //左下角
                if (!grounds[3])
                {
                    GameObject obj = Instantiate(this.gameObject,spawnPos2,Quaternion.identity);
                    grounds[3] = obj;
                }
                break;
            case 2:
                //左下角
                spawnPos0.z -= 100;
                spawnPos0.x -= 100;
                //左侧
                spawnPos1.x -= 100;
                //左上角
                spawnPos2.x -= 100;
                spawnPos2.z += 100;
                //左下角
                if (!grounds[3])
                {
                    GameObject obj = Instantiate(this.gameObject,spawnPos0,Quaternion.identity);
                    grounds[3] = obj;
                }
                //左侧
                if (!grounds[4])
                {
                    GameObject obj = Instantiate(this.gameObject,spawnPos1,Quaternion.identity);
                    grounds[4] = obj;
                }
                //左上角
                if (!grounds[5])
                {
                    GameObject obj = Instantiate(this.gameObject,spawnPos2,Quaternion.identity);
                    grounds[5] = obj;
                }
                break;
            case 3:
                //左上角
                spawnPos0.z += 100;
                spawnPos0.x -= 100;
                //上侧
                spawnPos1.z += 100;
                //右上角
                spawnPos2.x += 100;
                spawnPos2.z += 100;
                //左上角
                if (!grounds[5])
                {
                    GameObject obj = Instantiate(this.gameObject,spawnPos0,Quaternion.identity);
                    grounds[5] = obj;
                }
                //左侧
                if (!grounds[6])
                {
                    GameObject obj = Instantiate(this.gameObject,spawnPos1,Quaternion.identity);
                    grounds[6] = obj;
                }
                //右上角
                if (!grounds[7])
                {
                    GameObject obj = Instantiate(this.gameObject,spawnPos2,Quaternion.identity);
                    grounds[7] = obj;
                }
                break;
            default:
                Debug.LogError("Collider的index出错，请立即检查");
                break;
        }
    }
}
