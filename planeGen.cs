using System.Collections;
using UnityEngine;

public class planeGen : MonoBehaviour
{
    public GameObject plane;
    public GameObject player;
    private int radius = 5;
    private int planeOffset = 10;
    private Vector3 startPos = Vector3.zero;
    private int XplayerMove => (int)(player.transform.position.x - startPos.x);
    private int ZplayerMove => (int)(player.transform.position.z - startPos.z);
    private int xPlayerLocation => (int)Mathf.Floor(player.transform.position.x / planeOffset) * planeOffset;
    private int zPlayerLocation => (int)Mathf.Floor(player.transform.position.z / planeOffset) * planeOffset;

    private Hashtable tileplane = new Hashtable();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (startPos == Vector3.zero)
        {
            for (int x = -radius; x < radius; x++)
            {
                for (int z = -radius; z < radius; z++)
                {
                    Vector3 pos = new Vector3(x * planeOffset + xPlayerLocation, 0, z * planeOffset + zPlayerLocation);
                    if (!tileplane.ContainsKey(pos))
                    {
                        GameObject _plane = Instantiate(plane, pos, Quaternion.identity);
                        tileplane.Add(pos, _plane);
                        _plane.transform.SetParent(this.transform);
                    }
                }

            }
            if (hasPlayerMoved())
            {
                for (int x = -radius; x < radius; x++)
                {
                    for (int z = -radius; z < radius; z++)
                    {
                        Vector3 pos = new Vector3(x * planeOffset + xPlayerLocation, 0, z * planeOffset + zPlayerLocation);
                        if (!tileplane.ContainsKey(pos))
                        {
                            GameObject _plane = Instantiate(plane, pos, Quaternion.identity);
                            tileplane.Add(pos, _plane);
                            _plane.transform.SetParent(this.transform);
                        }
                    }

                }
            }
        }
    }
    bool hasPlayerMoved()
    {

        if (Mathf.Abs(XplayerMove) >= planeOffset || Mathf.Abs(ZplayerMove) >= planeOffset) return true;
        return false;
    }
}
