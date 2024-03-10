using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LogicScript : MonoBehaviour
{
    public GameObject cube;
    // Start is called before the first frame update
    void Start()
    {
        Transform line = cube.transform.parent;
        Transform board = line.parent;
        int indexCube = 0;
        int indexLine = 0;
        for (int i = 0; i < line.childCount; i++)
        {
            if (line.GetChild(i).gameObject == cube)
            {
                indexCube = i;
            }
        }
        for (int i = 0; i < board.childCount; i++)
        {
            if (board.GetChild(i).gameObject == line.gameObject)
            {
                indexLine = i;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
