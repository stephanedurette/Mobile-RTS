using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolManager : MonoBehaviour
{
    [SerializeField] private Transform parentTransform;

    private Dictionary<GameObject, ObjectPool<GameObject>> objectPools;
    private Dictionary<GameObject, Transform> spawnParentTransforms;

    private void Awake()
    {
        objectPools = new();
        spawnParentTransforms = new();
    }

    public T SpawnObject<T>(GameObject objectToSpawn, Vector3 position)
    {
        GameObject objectSourcePrefab = PrefabUtility.GetCorrespondingObjectFromOriginalSource(objectToSpawn);

        if (!objectPools.ContainsKey(objectSourcePrefab))
        {
            //Initialize Pool
            objectPools.Add(objectSourcePrefab, InitializePool(objectSourcePrefab));

            //Initialize Transform
            Transform newParentObject = new GameObject($"{objectSourcePrefab.name} Instances").transform;
            newParentObject.parent = parentTransform;
            spawnParentTransforms.Add(objectSourcePrefab, newParentObject);
        }

        //Spawn
        GameObject spawnedObject = objectPools[objectToSpawn].Get();

        //Set Transform
        spawnedObject.transform.parent = spawnParentTransforms[objectToSpawn];

        //Set Position
        spawnedObject.transform.position = position;

        //Assign Poolable
        if (!spawnedObject.TryGetComponent(out PoolableObject _))
        {
            PoolableObject poolable = spawnedObject.AddComponent<PoolableObject>();
            poolable.Pool = objectPools[objectToSpawn];
        }

        return spawnedObject.GetComponent<T>();
    }

    private ObjectPool<GameObject> InitializePool(GameObject prefab)
    {
        ObjectPool<GameObject> pool = new(
            OnCreate,
            (p) => p.SetActive(true),
            (p) => p.SetActive(false),
            (p) => Destroy(p),
            true
        );

        GameObject OnCreate()
        {
            GameObject p = Instantiate(prefab);
            p.SetActive(false);
            return p;
        }

        return pool;
    }
}
