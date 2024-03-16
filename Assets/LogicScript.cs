using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class LogicScript : MonoBehaviour
{
    public GameObject plateau;
    private List<Vector3> p1Spawn = new List<Vector3>();
    private List<Vector3> p2Spawn = new List<Vector3>();

    private void spawnChampionP1(string champNamePrefab)
    public GameObject cube;
    public Texture[] textures; // Liste des textures
    public Material[] materials; // Liste des materials

    // Start is called before the first frame update
    void Start()
    {
        GameObject champPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Champions/" + champNamePrefab + ".prefab");
        GameObject champPrefab2 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Champions/" + champNamePrefab + ".prefab");
        GameObject champion = Instantiate(champPrefab, p1Spawn[0], Quaternion.Euler(0, 90, 0));
        champion.GetComponent<PlayerPositionHandler>().initialPosition = new Point(3, 3);
        champion.GetComponent<PlayerPositionHandler>().playerID = PlayerID.Player1;
        GameObject champion2 = Instantiate(champPrefab2, p1Spawn[1], Quaternion.Euler(0, 90, 0));
        champion2.GetComponent<PlayerPositionHandler>().initialPosition = new Point(3, 7);
        champion2.GetComponent<PlayerPositionHandler>().playerID = PlayerID.Player1;
    }

    private void spawnChampionP2(string champNamePrefab)
    {
        GameObject champPrefab3 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Champions/" + champNamePrefab + ".prefab");
        GameObject champPrefab4 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Champions/" + champNamePrefab + ".prefab");
        GameObject champion = Instantiate(champPrefab3, p2Spawn[0], Quaternion.Euler(0, -90, 0));
        champion.GetComponent<PlayerPositionHandler>().initialPosition = new Point(10, 3);
        champion.GetComponent<PlayerPositionHandler>().playerID = PlayerID.Player2;
        GameObject champion2 = Instantiate(champPrefab4, p2Spawn[1], Quaternion.Euler(0, -90, 0));
        champion2.GetComponent<PlayerPositionHandler>().initialPosition = new Point(10, 7);
        champion2.GetComponent<PlayerPositionHandler>().playerID = PlayerID.Player2;
    }
    void Awake()
    {
        int player1 = PlayerPrefs.GetInt("Player1");
        int player2 = PlayerPrefs.GetInt("Player2");
        Debug.Log("Player1: " + player1);
        Debug.Log("Player2: " + player2);
        p1Spawn.Add(setSpawnPosition(3, 3));
        p1Spawn.Add(setSpawnPosition(3, 7));
        p2Spawn.Add(setSpawnPosition(10, 3));
        p2Spawn.Add(setSpawnPosition(10, 7));
        switch (player1)
        {
            case 0:
                spawnChampionP1("Squid");
                break;
            case 1:
                spawnChampionP1("Sparrow");
                break;
            case 2:
                spawnChampionP1("Pudu");
                break;
            case 3:
                spawnChampionP1("Gekko");
                break;
            default:
                spawnChampionP1("Squid");
                break;
        }

        switch (player2)
        {
            case 0:
                spawnChampionP2("Squid");
                break;
            case 1:
                spawnChampionP2("Sparrow");
                break;
            case 2:
                spawnChampionP2("Pudu");
                break;
            case 3:
                spawnChampionP2("Gekko");
                break;
            default:
                spawnChampionP2("Squid");
                break;
        }
    }

    Vector3 setSpawnPosition(int x, int y)
        int indexSol = PlayerPrefs.GetInt("IndexSol");
    Transform line = cube.transform.parent;
    Transform board = line.parent;
    int indexCube = 0;
    int indexLine = 0;
        for (int i = 0; i<line.childCount; i++)
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
    return new Vector3(plateau.transform.GetChild(x).GetChild(y).transform.position.x, 2.1f, plateau.transform.GetChild(x).GetChild(y).transform.position.z);
}
}
