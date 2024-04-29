using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerID playerID;
    private GameObject[] cubeAttache;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        cubeAttache = new GameObject[2];
        cubeAttache[0] = other.gameObject;

    }
}

