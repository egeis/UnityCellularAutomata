using UnityEngine;
using System.Collections;

public class DestroyOffscreen : MonoBehaviour
{
    public delegate void OnDestroy();
    public event OnDestroy DestroyCallback;

    private bool offscreen;

    GlobalSettings _gs;

    void Start()
    {
        _gs = GlobalSettings.Instance;
    }
 
    // Update is called once per frame
    void Update()
    {
        GridVisible grid = _gs.Visible;
        GridCoordinates gc = gameObject.GetComponent<GridCoordinates>();

        if (gc.x < grid.minimumX || gc.x > grid.maximumX)
            offscreen = true;
        else if (gc.y < grid.minimumY || gc.y > grid.maximumY)
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
        GameObjectUtil.Destroy(gameObject);

        if (DestroyCallback != null)
        {
            DestroyCallback();
        }
    }
}
