﻿using UnityEngine;
using System.Collections;

public class DestroyOffscreen : MonoBehaviour
{
    public delegate void OnDestroy();
    public event OnDestroy DestroyCallback;

    private bool offscreen;

    void Update()
    {
        GridVisible grid =  GlobalSettings.Instance.Visible;
        CellBehavior _gc = gameObject.GetComponent<CellBehavior>();

        if (_gc.x < grid.minimumX || _gc.x > grid.maximumX)
            offscreen = true;
        else if (_gc.y < grid.minimumY || _gc.y > grid.maximumY)
            offscreen = true;
        else
            offscreen = false;

        if (offscreen)
        {
            OutOfBounds();
        }
    }

    void OutOfBounds()
    {
        offscreen = false;

        GlobalSettings.Instance.ActiveObjects.Remove(gameObject.name);

        GameObjectUtil.Destroy(gameObject);

        if (DestroyCallback != null)
        {
            DestroyCallback();
        }
    }
}
