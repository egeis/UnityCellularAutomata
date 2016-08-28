using UnityEngine;
using System.Collections;

[System.Serializable]
public class GridVisible
{
    public int minimumX = 0;
    public int minimumY = 0;
    public int maximumX = 0;
    public int maximumY = 0;

    public override string ToString()
    {
        return base.ToString() + ": x=(" + minimumX + "," + maximumX + ") y=(" + minimumY + "," + maximumY + ")";
    }
}

[RequireComponent(typeof(CameraMovement))]
public class CameraGridVisible : MonoBehaviour
{
    public GridVisible gridV;

    GlobalSettings _gs;

    void Awake()
    {
        gridV = new GridVisible();
        _gs = GlobalSettings.Instance;
    }
	
	void FixedUpdate ()
    {
        calculateVisibleGrid();
    }

    public void calculateVisibleGrid()
    {
        Vector2 screen = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));

        gridV.minimumX = (int) Mathf.Round(Mathf.Clamp((screen.x / _gs.GridSize) - 1f, 0f, _gs.CellCount.x));
        gridV.minimumY = (int) Mathf.Round(Mathf.Clamp((screen.y / _gs.GridSize) - 1f, 0f, _gs.CellCount.y));

        screen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.nearClipPlane));
        gridV.maximumX = (int) Mathf.Round(Mathf.Clamp((screen.x / _gs.GridSize) + 1f, 0f, _gs.CellCount.x));
        gridV.maximumY = (int) Mathf.Round(Mathf.Clamp((screen.y / _gs.GridSize) + 1f, 0f, _gs.CellCount.y));
    }
}
