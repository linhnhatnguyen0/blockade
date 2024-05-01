using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallVerification : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerID playerID;
    List<Point> cubeAttached = new List<Point>(4);
    public bool isHorizontal;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Untagged")
        {
            Point cube = PlayerMovementHandler.GetCubeFromBoard(other.transform);
            cubeAttached.Add(cube);
        }
        Debug.Log("Cube attached: " + cubeAttached.Count);
        if(cubeAttached.Count == 4)
        {
            Debug.Log("Cube attached to the wall");
        }
    }

    public Point getCubeAttached()
    {
        //TODO: return the top left cube attached to the wall
        cubeAttached.ForEach(c =>
        {
            Debug.Log(c.ToString());
        });
        return cubeAttached[0];
    }
}
