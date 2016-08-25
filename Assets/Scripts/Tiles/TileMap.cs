using UnityEngine;
using System.Collections;

public class TileMap : MonoBehaviour
{
    public Vector2 mapSize = new Vector2(20, 20);
    public GameObject prefab;

    private Vector2 tileSize = new Vector2();

    void Awake()
    {
        var r = prefab.GetComponent<Circle>().radius;
        tileSize.x = tileSize.y = (r + 1) * 2;
    }

    void Start()
    {
        for (int i = 0; i < mapSize.x; i++)
        {
            for (int j = 0; j < mapSize.y; j++)
            {
                GameObject cell = Instantiate(
                    prefab,
                    new Vector3(i * tileSize.x, j * tileSize.y, 0),
                    Quaternion.identity
                ) as GameObject;

                cell.transform.parent = gameObject.transform;

                cell.name = "cell_" + i + "_" + j;
            }

        }

    }
}
