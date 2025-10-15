using System.Collections;
using UnityEngine;

public class genWorld : MonoBehaviour
{
    public GameObject BlockGO;
    public GameObject player;
    [SerializeField] private int worldSizeX = 20;
    [SerializeField] private int worldSizeZ = 20;
    
    private int noiseH = 3;
    private Vector3 startPos=Vector3.zero;
    private Hashtable blockContainer = new Hashtable();
    private int xPlayermove
    {
        get
        {
            return (int)(player.transform.position.x - startPos.x);
        }
    }
    private int zPlayermove
    {
        get
        {
            return (int)(player.transform.position.z - startPos.z);
        }
    }
    private int xplayerloc
    {
        get
        {
            return (int)Mathf.Floor(player.transform.position.x);
        }
    }
    private int zplayerloc
    {
        get
        {
            return (int)Mathf.Floor(player.transform.position.z);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
        for (int x = -worldSizeX; x < worldSizeX; x++)
        {
            for (int z = -worldSizeZ; z < worldSizeZ; z++)
            {
                Vector3 pos = new Vector3(x+startPos.x , noiseH * genNoise(x, z, 8f), z + startPos.z);
                GameObject block = Instantiate(BlockGO, pos, Quaternion.identity) as GameObject;
                blockContainer.Add(pos, block);
                block.transform.SetParent(this.transform);

            }
        }
    }

    // Update is called once per fram
    void Update()
    {
        if (Mathf.Abs(xPlayermove) >= 1 || Mathf.Abs(zPlayermove) >= 1)
        {
            for (int x = -worldSizeX; x < worldSizeX; x++)
            {
                for (int z = -worldSizeZ; z < worldSizeZ; z++)
                {
                    Vector3 pos = new Vector3(x + xplayerloc, noiseH * genNoise(x + xplayerloc, z + zplayerloc, 8f), z + zplayerloc);
                    if (!blockContainer.ContainsKey(pos))
                    {
                    GameObject block = Instantiate(BlockGO, pos, Quaternion.identity) as GameObject;
                        blockContainer.Add(pos, block);
                        block.transform.SetParent(this.transform);
                    }
                }
            }
        }

    }
    private float genNoise(int x, int z, float detail)
    {
        float xNoise = (x + this.transform.position.x) / detail;
        float zNoise = (z + this.transform.position.z) / detail;
        return Mathf.PerlinNoise(xNoise, zNoise);
    }

}
