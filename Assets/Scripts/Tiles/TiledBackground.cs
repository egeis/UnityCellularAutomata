using UnityEngine;
using System.Collections;

public class TiledBackground : MonoBehaviour
{
    private float textureSize = 0;

    public bool scaleHorizontially = true;
    public bool scaleVertically = true;

    public Vector2 offset = Vector2.zero;
    public Vector2 size = Vector2.zero;

    private Vector2 cOff = Vector2.zero;

    private Material mat;

    private GlobalSettings _gs;

    void Awake()
    {
        _gs = GlobalSettings.Instance;
    }

    // Use this for initialization
    void Start()
    {
        textureSize = _gs.GridSize;

        size.x = !scaleHorizontially ? 1f : Mathf.Ceil(Screen.width / (textureSize * PixelPerfectCamera.scale));
        size.y = !scaleVertically ? 1f : Mathf.Ceil(Screen.height / (textureSize * PixelPerfectCamera.scale));

        cOff.x = ( (size.x % 2f) == 0f ) ? 0.5f : 0f;
        cOff.y = ( (size.y % 2f) == 0f ) ? 0.5f : 0f;

        transform.localScale = new Vector3(size.x * textureSize, size.y * textureSize, 1f);

        GetComponent<Renderer>().material.mainTextureScale = new Vector3(size.x, size.y, 1f);

        mat = GetComponent<Renderer>().material;
        offset = mat.GetTextureOffset("_MainTex");

        offset += cOff;

        mat.SetTextureOffset("_MainTex", offset);
    }

    void Update()
    {
        var nPos = Camera.main.transform.position;

        offset.x = (nPos.x / textureSize) + cOff.x;
        offset.y = (nPos.y / textureSize) + cOff.y;

        mat.SetTextureOffset("_MainTex", offset);
    }
}
