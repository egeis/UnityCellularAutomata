using UnityEngine;
using System.Collections;

public class GlobalSettings : MonoBehaviour
{
    public float GridSize = 32f;

    public Vector2 CellCount = new Vector2(100, 100);

    public GameObject CellPrefab;

    private static GlobalSettings _instance;

    public GridVisible Visible
    {
        get { return Camera.main.gameObject.GetComponent<CameraGridVisible>().gridV; }
    }

    public static GlobalSettings Instance
    {
        get { return _instance; }
    }

    void Awake()
    {
        _instance = this;
    }
}
