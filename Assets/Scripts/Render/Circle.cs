using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class Circle : MonoBehaviour
{
    public float radius = 1f;
    public int segments = 20;
    public bool noise = false;

    [Range(0,1)]
    public float noiseIntensity = 0.4f;

    float step;
    float radialFactor;
    float tangencialFactor;

    void Awake()
    {
        Rebuild();
    }

    void Rebuild()
    {
        //MeshFilter Componenet
        MeshFilter mf = GetComponent<MeshFilter>();

        if(mf == null)
        {
            Debug.LogError("MeshFilter not found!");
            return;
        }

        Mesh mesh = mf.sharedMesh;
        if(mesh == null)
        {
            mf.mesh = new Mesh();
            mesh = mf.sharedMesh;
        }
        mesh.Clear();

        //Mesh Collider Componenet
        MeshCollider mc = GetComponent<MeshCollider>();
        if(mc == null)
        {
            Debug.LogError("MeshCollider not found!");
            return;
        }

        Mesh meshCol = mc.sharedMesh;
        if (meshCol == null)
        {
            mc.sharedMesh = new Mesh();
            meshCol = mc.sharedMesh;
        }
        meshCol.Clear();

        if(segments < 3)
        {
            Debug.LogError("Segments must be greather than or equal to 3.");
            return;
        }

        //Pre-drawing calculations
        float x = radius;
        float y = 0;
        int idx = 1;
        int indices = (segments) * 3;
        int[] cTri = new int[indices];

        step = (2 * Mathf.PI) / segments;

        tangencialFactor = Mathf.Tan(step);
        radialFactor = Mathf.Cos(step);

        //Generate vertices
        Vector3[] cVert = new Vector3[segments + 1];

        cVert[0] = new Vector3(0, 0, 0);

        for(int i = 1; i < (segments + 1); i++)
        {
            float tx = -y;
            float ty = x;

            x += tx * tangencialFactor;
            y += ty * tangencialFactor;

            x *= radialFactor;
            y *= radialFactor;

            cVert[i] = new Vector3(x, y, 0);
        }

        //Generate triangles
        for(int i = 0; i < indices; i+=3)
        {
            cTri[i + 1] = 0;
            cTri[i] = idx;

            if (i >= (indices - 3))
                cTri[i + 2] = 1;
            else
                cTri[i + 2] = idx + 1;

            idx++;
        }

        mesh.vertices = cVert;
        mesh.triangles = cTri;

        meshCol.name = "circleCollider";
        meshCol.vertices = cVert;
        meshCol.triangles = cTri;

        Material mat = GetComponent<Renderer>().sharedMaterial;
        if(mat == null)
            GetComponent<Renderer>().sharedMaterial = new Material(Shader.Find("Diffuse"));

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        mesh.Optimize();
    }

}
