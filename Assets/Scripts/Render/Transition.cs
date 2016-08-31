using UnityEngine;
using System.Collections;

public class Transition : MonoBehaviour, IRecycle
{
    private SpriteRenderer spriteRenderer;

    public IEnumerator Fade(Color next)
    {
        Color current = spriteRenderer.color;

        if (current.Equals(next)) yield break;

        for (float t = 0f; t < 1.0f; t += Time.deltaTime / 2f)
        {
            /*Color nc = new Color(
                MathHelper.LerpUnclamped(current.r, next.r, t),
                MathHelper.LerpUnclamped(current.g, next.g, t),
                MathHelper.LerpUnclamped(current.b, next.b, t),
                MathHelper.LerpUnclamped(current.a, next.a, t)
            );*/

            spriteRenderer.color = Color.Lerp(current, next, t);

            //renderer.color = nc;
            yield return null;
        }
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Toggle(Color next)
    {
        StartCoroutine("Fade", next);
    }

    public void SetColor(Color next)
    {
        spriteRenderer.color = next;
    }

    public void Restart()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Shutdown()
    {
        StopCoroutine("Transition");
    }
}