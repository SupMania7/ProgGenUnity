using UnityEngine;
using UnityEngine.InputSystem;

public class meshController : MonoBehaviour
{
    void Awake()
    {
        if (!Mouse.current.added)
            InputSystem.AddDevice<Mouse>();
    }
    public float radius = 2f;
    public float deformationStrength = 2f;
    private Mesh mesh;
    private Vector3[] verticies, modifiedVerts;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log(Camera.main);

        mesh = GetComponent<MeshFilter>().mesh;
        verticies=mesh.vertices;
        modifiedVerts=mesh.vertices;
    }
    void recalculatemesh()
    {
        mesh.vertices = modifiedVerts;
        GetComponent<MeshCollider>().sharedMesh = mesh;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

      
    }


    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray=Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);

        if (Physics.Raycast(ray, out hit,Mathf.Infinity) )
        {
            Vector3 localHit = transform.InverseTransformPoint(hit.point);
            Debug.Log("hit!");
                for (int v = 0; v < verticies.Length; v++)
            {
               
                Vector3 distance = modifiedVerts[v] - localHit;

                float smoothfactor = 2f;
                float force = deformationStrength / (1f + distance.sqrMagnitude);

                if (distance.sqrMagnitude<radius)
                {
                    if(Mouse.current.leftButton.isPressed)
                    {
                        modifiedVerts[v] += (Vector3.up * force) / smoothfactor;
                    }
                    if (Mouse.current.rightButton.isPressed)
                    {
                        modifiedVerts[v] += (Vector3.down * force) / smoothfactor;
                    }
                }
            }

        }
        recalculatemesh();
    }
}

