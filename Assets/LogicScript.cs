using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LogicScript : MonoBehaviour
{
    public GameObject cube;
    public Texture[] textures; // Liste des textures
    public Material[] materials; // Liste des materials

    // Start is called before the first frame update
    void Start()
    {
        int indexSol = PlayerPrefs.GetInt("IndexSol");
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
        foreach (Transform lines in board)
        {
            // Parcourir tous les cubes de la ligne actuelle
            foreach (Transform cubes in lines)
            {
                Renderer renderer = cubes.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material = materials[indexSol];
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
