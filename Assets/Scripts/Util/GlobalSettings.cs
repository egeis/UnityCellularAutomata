using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalSettings : MonoBehaviour
{
    public float transitionDuration = 1f;

    public float GridSize = 32f;

    public Vector2 CellCount = new Vector2(100, 100);

    public GameObject CellPrefab;

    public Dictionary<Vector2, int> States = new Dictionary<Vector2, int>();

    static GlobalSettings _instance;

    public static GlobalSettings Instance {
        get {
            return _instance;
        }
    }

    public GridVisible Visible
    {
        get { return Camera.main.gameObject.GetComponent<CameraGridVisible>().gridV; }
    }

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        for (int i = 0; i < CellCount.x; i++)
            for (int j = 0; j < CellCount.y; j++)
                States.Add(new Vector2(i, j), (Random.Range(0f, 1f) <= 0.25f) ? 1 : 0);

        //Not working...
    }
}
