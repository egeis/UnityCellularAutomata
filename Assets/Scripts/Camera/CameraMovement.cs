using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
    private float minX = 0;
    private float minY = 0;
    private float maxX = 0;
    private float maxY = 0;

    private Vector2 size;

    public float headerOffset = 0;

    public GameObject tileMap;
    public GameObject background;

    public Vector2 targetMin;
    public Vector2 targetMax;

    public float screenX;
    public float screenY;

    private float tilesize;

    void Awake()
    {
        screenX = Screen.width;
        screenY = Screen.height;
    }

    void Start()
    {
        Vector2 size = tileMap.GetComponent<TileMap>().mapSize;
        Vector2 grid = background.GetComponent<TiledBackground>().size;
        tilesize = (tileMap.GetComponent<TileMap>().prefab.GetComponent<Circle>().radius + 1) * 2;

        maxX = ((size.x - Mathf.Ceil(grid.x / 2) + (((size.x % 2f) == 0f) ? 0 : 1 )) * tilesize);
        maxY = ((size.y - Mathf.Ceil(grid.y / 2) + (((size.y % 2f) == 0f) ? 0 : 1 )) * tilesize) + headerOffset;

        minX = ((Mathf.Ceil(grid.x / 2) -1 ) * tilesize);
        minY = ((Mathf.Ceil(grid.y / 2) -1 )  * tilesize);

        Camera.main.transform.position = new Vector3((size.x * tilesize) / 2, (size.y * tilesize) / 2, Camera.main.transform.position.z);
    }

    void Update()
    {
        float xAxisValue = Input.GetAxis("Horizontal");
        float yAxisValue = Input.GetAxis("Vertical");

        Vector3 pos = new Vector3(xAxisValue, yAxisValue, this.transform.position.z);

        pos.x = Mathf.Clamp(pos.x + this.transform.position.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y + this.transform.position.y, minY, maxY);
        pos.z = Camera.main.transform.position.z;

        Camera.main.transform.position = (pos);

        targetMin = Camera.main.ScreenToWorldPoint(new Vector3(0,0, Camera.main.nearClipPlane));
        targetMin.x = Mathf.Round(Mathf.Clamp((targetMin.x / tilesize) - 1f, 0f, size.x));
        targetMin.y = Mathf.Round(Mathf.Clamp((targetMin.y / tilesize) - 1f, 0f, size.y));

        targetMax = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.nearClipPlane));
        targetMax.x = Mathf.Round(Mathf.Clamp((targetMax.x / tilesize) + 1f, 0f, size.x));
        targetMax.y = Mathf.Round(Mathf.Clamp((targetMax.y / tilesize) + 1f, 0f, size.y));
    }
}