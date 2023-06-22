using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    public int XSize = 20;
    public int ZSize = 20;

    public int TextureWidth = 1024;
    public int TextureHeight = 1024;

    public float Noise01Scale = 2f;
    public float Noise01Amp = 2f;

    public float Noise02Scale = 4f;
    public float Noise02Amp = 4f;

    public float Noise03Scale = 6f;
    public float Noise03Amp = 6f;

    private Mesh mesh;

    private Vector3[] vertices;
    private int[] triangles;
    private Vector2[] uvs;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        UpdateShape();
    }

    private void CreateShape()
    {
        vertices = new Vector3[(XSize + 1) * (ZSize + 1)];

        var i = 0;
        for(var z = 0; z < ZSize + 1; z++) {
            for (var x = 0; x < XSize + 1; x++) {
                float y = Mathf.PerlinNoise(x * 0.3f, z * 0.3f) * 2f;
                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }

        triangles = new int[XSize * ZSize * 6];

        int vert = 0;
        int tris = 0;

        for (var z = 0; z < ZSize; z++) {
            for (var x = 0; x < XSize; x++) {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + XSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + XSize + 1;
                triangles[tris + 5] = vert + XSize + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }

        uvs = new Vector2[vertices.Length];

        i = 0;
        for (var z = 0; z < ZSize + 1; z++) {
            for (var x = 0; x < XSize + 1; x++) {
                uvs[i] = new Vector2((float)x / XSize, (float)z / ZSize);
                i++;
            }
        }
    }

    private void UpdateShape()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;

        mesh.RecalculateNormals();
    }

    private void OnDrawGizmos()
    {
        if(vertices == null) {
            return;
        }

        for(var i = 0; i < vertices.Length; i++) {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }
}
