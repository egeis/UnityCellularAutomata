using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
            _gs._x = i;

            for (int j = _gs.Visible.minimumY; j < _gs.Visible.maximumY; j++)
            {
                _gs._y = j;
                GameObject cell;

                if (_gs.ActiveObjects.TryGetValue("cell_" + i + "_" + j, out cell))  
                   continue;

                cell = GameObjectUtil.Instantiate(
                    _gs.CellPrefab,
                    new Vector3(i * _gs.GridSize, j * _gs.GridSize, 0),
                    gameObject,
                    "cell_" + i + "_" + j
                ) as GameObject;

                _gs.ActiveObjects.Add(cell.name, cell);
            }
        }
    }

}
