using UnityEngine;
using System.Collections;

public class LockedToCamera : MonoBehaviour
{
    public Vector3 pos;
    
    // Update is called once per frame
	void Update ()
    {
        Vector3 v3 = new Vector3(Camera.main.transform.position.x,
            Camera.main.transform.position.y,
            this.gameObject.transform.position.z);

        this.gameObject.transform.position = v3;
        pos = v3;
	}
}
