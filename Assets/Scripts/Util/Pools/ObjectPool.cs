using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    public RecycleGameObject prefab;

    private List<RecycleGameObject> poolInstances = new List<RecycleGameObject>();

    public int count = 0;

    void Awake()
    {
        gameObject.isStatic = true;
    }
  
    private RecycleGameObject CreateInstance(Vector3 pos)
    {
        RecycleGameObject clone = GameObject.Instantiate(prefab);
        clone.transform.position = pos;
        clone.transform.parent = transform;

        poolInstances.Add(clone);

        return clone;
    }

    public RecycleGameObject NextObject(Vector3 pos)
    {
        RecycleGameObject instance = null;

        foreach (RecycleGameObject go in poolInstances)
        {
            if (go.gameObject.activeSelf != true)
            {
                instance = go;
                instance.transform.position = pos;
            }
        }

        if (instance == null)
            instance = CreateInstance(pos);

        instance.Restart();

        return instance;
    }
}
