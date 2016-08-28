using UnityEngine;
using System.Collections;

public class CellManager : MonoBehaviour
{
    GlobalSettings _gs;

    void Awake()
    {
        _gs = GlobalSettings.Instance;
    }

    void Start()
    {
        DrawVisible();
    }

    void Update()
    {
        DrawVisible();
    }

    public void DrawVisible()
    {
        for (int i = _gs.Visible.minimumX; i < _gs.Visible.maximumX; i++)
        {
            for (int j = _gs.Visible.minimumY; j < _gs.Visible.maximumY; j++)
            {
                if (GameObject.Find("cell_" + i + "_" + j) != null)
                    continue;

                GameObject cell = GameObjectUtil.Instantiate(
                    _gs.CellPrefab,
                    new Vector3(i * _gs.GridSize, j * _gs.GridSize, 0),
                    i,
                    j
                ) as GameObject;
            }
        }
    }

}
