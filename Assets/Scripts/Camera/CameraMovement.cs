using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
    private float minX = -200;
    private float minY = -200;
    private float maxX = 200;
    private float maxY = 200;

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