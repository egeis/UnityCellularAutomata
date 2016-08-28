using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameObjectUtil
{
    private static Dictionary<RecycleGameObject, ObjectPool> pools = new Dictionary<RecycleGameObject, ObjectPool>();

    public static GameObject Instantiate(GameObject prefab, Vector3 pos)
    {
        GameObject instance = null;

        RecycleGameObject recycledScript = prefab.GetComponent<RecycleGameObject>();
        if (recycledScript != null)
        {
            ObjectPool pool = GetObjectPool(recycledScript);
            instance = pool.NextObject(pos).gameObject;
        }
        else
        {
            instance = GameObject.Instantiate(prefab);
            instance.transform.position = pos;
        }

        return instance;
    }

    public static GameObject Instantiate(GameObject prefab, Vector3 pos, int i, int j)
    {
        GameObject instance = Instantiate(prefab, pos);

        instance.name = "cell_" + i + "_" + j;

        instance.GetComponent<GridCoordinates>().x = i;
        instance.GetComponent<GridCoordinates>().y = j;

        return instance;
    }

    public static void Destroy(GameObject gameObject)
    {
        RecycleGameObject recycleGameObject = gameObject.GetComponent<RecycleGameObject>();
        recycleGameObject.name = "_";

        if (recycleGameObject != null)
        {
            recycleGameObject.Shutdown();
        }
        else
            GameObject.Destroy(gameObject);
    }

    private static ObjectPool GetObjectPool(RecycleGameObject reference)
    {
        ObjectPool pool = null;

        if (pools.ContainsKey(reference))
            pool = pools[reference];
        else
        {
            GameObject poolContainer = new GameObject(reference.gameObject.name + "ObjectPool");
            pool = poolContainer.AddComponent<ObjectPool>();
            pool.prefab = reference;
            pools.Add(reference, pool);
        }

        return pool;
    }
}
