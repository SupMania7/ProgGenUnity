using UnityEngine;

public class MeshGen : MonoBehaviour
{
    public int worldx;
    public int worldz;
    private Mesh mesh;
    private int[] triangles;
    private Vector3[] vertices;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mesh=new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;
        GenerateMesh();
        UpdateMesh();
    }

    // Update is called once per frame
 
    void GenerateMesh()
    {
        triangles = new int[worldx * worldz * 6];
        vertices = new Vector3[(worldx + 1) * (worldz + 1)];
        for (int i = 0, z = 0; z <= worldz; z++)
        {
            for (int x = 0; x <= worldx; x++)
            {
                vertices[i] = new Vector3(x, 0, z);
                i++;
            }
        }
        int tris = 0;
        int verts = 0;
        for(int z = 0; z < worldz;z++)
        {
            for(int x = 0;x < worldx; x++)
            {
                triangles[tris] = verts;
                triangles[tris + 1]=verts+worldz+1;
                triangles[tris + 2]=verts+1;

                triangles[tris + 3] = verts + 1; 
                triangles[tris + 4] = verts +worldz+1;
                triangles[tris + 5]=verts+  worldz+2;
                verts++;
                tris += 6;

            }
            verts++;
        }
    }
    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
