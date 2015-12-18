using UnityEngine;
using System.Collections;

public class AutoSpawn : MonoBehaviour
{

    public GameObject obj;
    public static bool spawnedThisFrom = false;

    // Use this for initialization
    void Start()
    {
        Vector3 position = transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);
        position.z = BuildCamera.singleton.buildingZ;
        Instantiate(obj, position, transform.rotation);
    }
}
