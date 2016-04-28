using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PoolObject
{
    public GameObject obj;
    public int amount;

    public PoolObject()
    {
        obj = null;
        amount = 0;
    }

    public PoolObject(GameObject objRef, int instAmount)
    {
        obj = objRef;
        amount = instAmount;
    }
}

public class PoolManager : MonoBehaviour
{
    public List<PoolObject> pooledObjects = new List<PoolObject>();

    List<Powerup> mPooledObjects = null;

    void Start()
    {
        mPooledObjects = new List<Powerup>();

        for(int i = 0; i < pooledObjects.Count; i++)
        {
            for (int j = 0; j < pooledObjects[i].amount; j++)
            {
                GameObject newInst = (GameObject)Instantiate(pooledObjects[i].obj);
                newInst.name = pooledObjects[i].obj.name;

                Powerup powerupComponent = newInst.GetComponent<Powerup>();
                if(!powerupComponent)
                {
                    Debug.LogError(newInst.gameObject.name + " : Does not have a powerup component");
                    continue;
                }

                newInst.transform.parent = this.transform;
                newInst.SetActive(false);

                mPooledObjects.Add(powerupComponent);
            }
        }
    }

    public Powerup PullObject(PowerupEnum powerupType)
    {
        for (int i = 0; i < mPooledObjects.Count; i++)
        {
            if (!mPooledObjects[i].gameObject.activeSelf && 
                mPooledObjects[i].powerupType == powerupType)
            {
                return mPooledObjects[i];
            }
        }
        return null;
    }
}
