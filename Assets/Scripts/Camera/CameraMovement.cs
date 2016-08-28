using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
    public float minX = 0;
    public float minY = 0;
    public float maxX = 0;
    public float maxY = 0;

    public float headerOffset = 0f;
    public float footerOffset = 0f;

    GlobalSettings _gs;

    void Awake()
    {
        _gs = GlobalSettings.Instance;
        
        //Center Camera
        Camera.main.transform.position = new Vector3((_gs.CellCount.x * _gs.GridSize) / 2f, 
            (_gs.CellCount.y * _gs.GridSize) / 2f,
            Camera.main.transform.position.z);        
    }

    void Start()
    {
        Camera.main.gameObject.GetComponent<CameraGridVisible>().calculateVisibleGrid();

        float tilesize = _gs.GridSize;

        int x = _gs.Visible.maximumX - _gs.Visible.minimumX;
        int y = _gs.Visible.maximumY - _gs.Visible.minimumY;

        Debug.Log(x + " " + y);

        minX = (x * tilesize) / 2f;
        minY = (y * tilesize) / 2f;

        maxX = (_gs.CellCount.x * tilesize) - minX;
        maxY = (_gs.CellCount.y * tilesize) - minY;

        minX -= (tilesize * 2);
        minY -= (tilesize * 2);

        maxX += tilesize;
        maxY += tilesize;
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
    }
}