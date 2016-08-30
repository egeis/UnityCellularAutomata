using UnityEngine;
using System.Collections;

public class Transition : MonoBehaviour, IRecycle
{
    public IEnumerator Fade()
    {
        Color current = this.GetComponent<Renderer>().material.color;

        Color next = this.GetComponent<Renderer>().material.color;

        var _gs = GameObject.FindObjectOfType<GlobalSettings>().GetComponent<GlobalSettings>();

        if (current.Equals(next)) yield break;

        for (float t = 0f; t < 1.0f; t += Time.deltaTime / _gs.transitionDuration)
        {
            Color nc = new Color(
                Mathf.Lerp(current.r, next.r, t),
                Mathf.Lerp(current.g, next.g, t),
                Mathf.Lerp(current.b, next.b, t),
                Mathf.Lerp(current.a, next.a, t)
            );

            this.GetComponent<Renderer>().material.color = nc;
            yield return null;
        }
    }

    public void Toggle()
    {
        StartCoroutine("Fade");
    }

    public void Restart()
    {
        Color nc = this.GetComponent<Renderer>().material.color;

        this.GetComponent<Renderer>().material.color = nc;
    }

    public void Shutdown()
    {
        StopCoroutine("Transition");
    }
}