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
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Untagged")
        {
            Point cube = PlayerMovementHandler.GetCubeFromBoard(other.transform);
            cubeAttached.Add(cube);
        }
    }

    public Point getCubeAttached()
    {
        //TODO: return the top left cube attached to the wall
        return new Point(0, 0);
    }
}
