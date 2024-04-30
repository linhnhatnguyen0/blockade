using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallVerification : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerID playerID;
    List<Point> cubeAttached;
    public bool isHorizontal;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Untagged")
        {
            Point cube = PlayerMovementHandler.GetCubeFromBoard(other.transform);
            cubeAttached.Add(cube);
        }
        //cubeAttached.ForEach(c =>
        //{
        //    Debug.Log(c.ToString());
        //});
    }

    public void resetCubeAttached()
    {
        cubeAttached.Clear();
        cubeAttached = new List<Point>(4);
    }

    public Point getCubeAttached()
    {
        //TODO: return the top left cube attached to the wall
        cubeAttached.ForEach(c =>
        {
            Debug.Log(c.ToString());
        });
        return new Point(0, 0);
    }
}
