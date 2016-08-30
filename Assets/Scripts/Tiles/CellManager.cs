using UnityEngine;
using System.Collections;

public class CellManager : MonoBehaviour
{
    GlobalSettings _gs;

    void Start()
    {
        _gs = GlobalSettings.Instance;
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
                GameObject cell = GameObject.Find("cell_" + i + "_" + j);

                if (cell != null)
                    continue;

                cell = GameObjectUtil.Instantiate(
                    _gs.CellPrefab,
                    new Vector3(i * _gs.GridSize, j * _gs.GridSize, 0),
                    gameObject
                ) as GameObject;

                cell.name = "cell_" + i + "_" + j;
                cell.GetComponent<GridCoordinates>().x = i;
                cell.GetComponent<GridCoordinates>().y = j;
            }
        }
    }

}
