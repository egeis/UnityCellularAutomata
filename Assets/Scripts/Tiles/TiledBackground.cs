using UnityEngine;
using System.Collections;

public class TiledBackground : MonoBehaviour
{
    public int textureSize = 32;

    public bool scaleHorizontially = true;
    public bool scaleVertically = true;

    private Vector2 speed = Vector2.zero;
    private Vector2 offset = Vector2.zero;

    private Material mat;

    private Vector3 cPos;

    // Use this for initialization
    void Start()
    {
        float newWidth = !scaleHorizontially ? 1f : Mathf.Ceil(Screen.width / (textureSize * PixelPerfectCamera.scale));
        float newHeight = !scaleVertically ? 1f : Mathf.Ceil(Screen.height / (textureSize * PixelPerfectCamera.scale));

        transform.localScale = new Vector3(newWidth * textureSize, newHeight * textureSize, 1f);

        GetComponent<Renderer>().material.mainTextureScale = new Vector3(newWidth, newHeight, 1f);

        mat = GetComponent<Renderer>().material;
        offset = mat.GetTextureOffset("_MainTex");

        cPos = Camera.main.transform.position;
    }

    void LateUpdate()
    {
        var nPos = Camera.main.transform.position;
        speed = nPos - cPos;

        offset += (speed * (textureSize) ) * Time.deltaTime;
        mat.SetTextureOffset("_MainTex", offset);

        cPos = nPos;
    }
}
