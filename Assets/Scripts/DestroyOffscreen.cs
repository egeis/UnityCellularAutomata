using UnityEngine;
using System.Collections;

public class DestroyOffscreen : MonoBehaviour
{
    public float offset = 16f;

    public delegate void OnDestroy();
    public event OnDestroy DestroyCallback;

    private bool offscreen;
    private float offscreenX = 0f;

    void Awake()
    {
    }

    // Use this for initialization
    void Start()
    {
        //offscreenX = (Screen.width / PixelPerfectCamera.pixelsToUnits) / 2 + offset;
    }

    // Update is called once per frame
    void Update()
    {
        /*float posX = transform.position.x;

        if (Mathf.Abs(posX) > offscreenX)
        {
            if (dirX < 0 && posX < -offscreenX)
                offscreen = true;
            else if (dirX > 0 && posX > offscreenX)
                offscreen = true;
        }
        else
        {
            offscreen = false;
        }

        if (offscreen)
        {
            OutOfBounds();
        }*/
    }

    void OutOfBounds()
    {
        offscreen = false;
        GameObjectUtil.Destroy(gameObject);

        if (DestroyCallback != null)
        {
            DestroyCallback();
        }
    }
}
