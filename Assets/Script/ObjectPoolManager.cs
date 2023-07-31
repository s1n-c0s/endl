using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class ObjectPoolManager : MonoBehaviour
{
    public static List<PooledObjectInfo> ObjectPools = new List<PooledObjectInfo>();

    private GameObject _objectPoolEmptyHolder;
    
    private static GameObject _particleSystemPoolEmpty;
    private static GameObject _gameObjectPoolEmpty;
    private static GameObject _bulletPoolEmpty;


    public enum PoolType
    {
        ParticleSystem,
        GameObject,
        Bullet,
        None
    }
    public static PoolType PoolingType;

    private void Awake()
    {
        SetupEmpties();
    }

    private void SetupEmpties()
    {
        _objectPoolEmptyHolder = new GameObject("Pooled Objects");

        _particleSystemPoolEmpty = new GameObject("Particle Systems");
        _particleSystemPoolEmpty.transform.SetParent(_objectPoolEmptyHolder.transform);

        _gameObjectPoolEmpty = new GameObject("Game Objects");
        _gameObjectPoolEmpty.transform.SetParent(_objectPoolEmptyHolder.transform);

        _bulletPoolEmpty = new GameObject("Bullet");
        _bulletPoolEmpty.transform.SetParent(_objectPoolEmptyHolder.transform);
    }

    public static GameObject SpawnObject(GameObject objectToSpawn, Vector3 spawPosition, Quaternion spawnRotation, PoolType poolType = PoolType.None)
    {
        PooledObjectInfo pool = ObjectPools.Find(p => p.LookupString == objectToSpawn.name);

        /*PooledObjectInfo pool = null;
        foreach (PooledObjectInfo pooled in ObjectPools)
        {
            if (p.LookupString == objectToSpawn.name)
            {
                pool = p;
                break;
            }
        }*/

        //If the pool is null, create a new pool
        if (pool == null)
        {
            pool = new PooledObjectInfo() { LookupString = objectToSpawn.name };
            ObjectPools.Add(pool);
        }
        //Check if there are any inactive objects in the pool
        GameObject spawnableObj = pool.InactiveObject.FirstOrDefault();
        
        
        /*   GameObject spawnableObj = null;
        foreach (GameObject obj in pool.InactiveObject)
        {
            if (obj != null)
            {
                spawnableObj = obj;
                break;
            }
        }*/

        if (spawnableObj == null)
        {
            //Find the parent object for the pool
            GameObject parentObject = SetParentObject(poolType);

            //If there are no inactive objects in the pool, create a new one
            spawnableObj= Instantiate(objectToSpawn, spawPosition, spawnRotation);

            if (parentObject != null)
            {
                spawnableObj.transform.SetParent(parentObject.transform);
            }
        }
        else
        {
            //if there are inactive objects in the pool, reactivate the first one
            spawnableObj.transform.position = spawPosition;
            spawnableObj.transform.rotation = spawnRotation;
            pool.InactiveObject.Remove(spawnableObj);
            spawnableObj.SetActive(true);
        }

        return spawnableObj;
    }

    public static GameObject SpawnObject(GameObject objectToSpawn, Transform parentTransform)
    {
        PooledObjectInfo pool = ObjectPools.Find(p => p.LookupString == objectToSpawn.name);


        //If the pool is null, create a new pool
        if (pool == null)
        {
            pool = new PooledObjectInfo() { LookupString = objectToSpawn.name };
            ObjectPools.Add(pool);
        }
        //Check if there are any inactive objects in the pool
        GameObject spawnableObj = pool.InactiveObject.FirstOrDefault();


        if (spawnableObj == null)
        {
            
            //If there are no inactive objects in the pool, create a new one
            spawnableObj = Instantiate(objectToSpawn, parentTransform);
        }
        else
        {
            //if there are inactive objects in the pool, reactivate the first one
           
            pool.InactiveObject.Remove(spawnableObj);
            spawnableObj.SetActive(true);
        }

        return spawnableObj;
    }

    public static void ReturnObjectToPool(GameObject obj)
    {
        string goName = obj.name.Substring(0, obj.name.Length - 7); //Remove "(Clone)" from the name

        PooledObjectInfo pool = ObjectPools.Find(p => p.LookupString == goName);
        if (pool == null)
        {
            Debug.LogWarning("No pool found for " + obj.name);
            return;
        }
        else
        {
            obj.SetActive(false);
            pool.InactiveObject.Add(obj);
        }
    }
    private static GameObject SetParentObject(PoolType poolType)
    {
        switch (poolType)
        {
            case PoolType.ParticleSystem:
                return _particleSystemPoolEmpty;
            case PoolType.GameObject:
                return _gameObjectPoolEmpty;
            case PoolType.Bullet:
                return _bulletPoolEmpty;
            case PoolType.None:
                return null;
            default:
                return null;
        }
    }
}

public class PooledObjectInfo
{
    public string LookupString;
    public List<GameObject> InactiveObject = new List<GameObject>();
}
