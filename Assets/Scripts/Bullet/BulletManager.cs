using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletManager : MonoBehaviour {

    public GameObject bulletPrefab;
    public int maxBullets = 20;

    private List<GameObject> mBulletPool;

    void Start()
    {
        if (maxBullets < 0)
            maxBullets = 0;

        mBulletPool = new List<GameObject>();

        GameObject bulletPoolContainer = new GameObject("bulletpool");
        for (int i = 0; i < maxBullets; i++)
        {
            GameObject newBullet = Instantiate(bulletPrefab) as GameObject;
            newBullet.name = "Bullet";
            newBullet.transform.parent = bulletPoolContainer.transform;

            mBulletPool.Add(newBullet);
        }
    }


    public void Shoot(Vector3 position, Quaternion rotation)
    {
        for (int i = 0; i < mBulletPool.Count; i++)
        {
            if(!mBulletPool[i].activeSelf)
            {
                mBulletPool[i].transform.rotation = rotation;
                mBulletPool[i].transform.position = position;
                mBulletPool[i].SetActive(true);
                break;
            }
        }
    }
}
