using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Blockade;
using TMPro;

public class LogicScript : MonoBehaviour
{
    public GameObject plateau;
    private List<Vector3> p1Spawn = new List<Vector3>();
    private List<Vector3> p2Spawn = new List<Vector3>();
    public Material[] materials; // Liste des materials
    public Texture2D[] imageList;
    public IHMLink partie;
    public RawImage panelImageJ1;
    public RawImage panelImageJ2;
    public RawImage imageCinematiqueJ1;
    public RawImage imageCinematiqueJ2;
    public TextMeshProUGUI playerName1;
    public TextMeshProUGUI playerName2;
    public TextMeshProUGUI playerNameCine1;
    public TextMeshProUGUI playerNameCine2;

    // Start est appel� avant la premi�re frame de mise � jour
    // Changer la couleur du plateau en fonction de la couleur choisie par le joueur
    void Start()
    {
        partie = new IHMLink();
        int indexJ1 = PlayerPrefs.GetInt("IndexIconeJ1");
        int indexJ2 = PlayerPrefs.GetInt("IndexIconeJ2");
        //Debug.Log(indexJ1 + "-" + indexJ2);
        panelImageJ1.texture = imageList[indexJ1];
        panelImageJ2.texture = imageList[indexJ2];
        //imageCinematiqueJ1.texture = imageList[indexJ1];
        //imageCinematiqueJ2.texture = imageList[indexJ2];
        PlayerPrefs.SetInt("currentPlayer", 1);
        int indexSol = PlayerPrefs.GetInt("IndexSol");
        foreach (Transform lines in plateau.transform)
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

    /// <summary>
    /// Faire apparaitre les pions pour le joueur 1
    /// </summary>
    /// <param name="champNamePrefab"></param>
    private void spawnChampionP1(string champNamePrefab)
    {
        GameObject champPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Champions/" + champNamePrefab + ".prefab");
        GameObject champPrefab2 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Champions/" + champNamePrefab + ".prefab");
        GameObject champion = Instantiate(champPrefab, p1Spawn[0], Quaternion.Euler(0, 90, 0));
        champion.GetComponent<PlayerPositionHandler>().initialPosition = new Point(3, 3);
        champion.GetComponent<PlayerPositionHandler>().isUp = true;
        champion.GetComponent<PlayerPositionHandler>().playerID = PlayerID.Player1;
        GameObject champion2 = Instantiate(champPrefab2, p1Spawn[1], Quaternion.Euler(0, 90, 0));
        champion2.GetComponent<PlayerPositionHandler>().initialPosition = new Point(3, 7);
        champion2.GetComponent<PlayerPositionHandler>().isUp = false;
        champion2.GetComponent<PlayerPositionHandler>().playerID = PlayerID.Player1;
    }

    /// <summary>
    /// Faire apparaitre les pions pour le joueur 2 
    /// </summary>
    /// <param name="champNamePrefab"></param>
    private void spawnChampionP2(string champNamePrefab)
    {
        GameObject champPrefab3 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Champions/" + champNamePrefab + ".prefab");
        GameObject champPrefab4 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Champions/" + champNamePrefab + ".prefab");
        GameObject champion = Instantiate(champPrefab3, p2Spawn[0], Quaternion.Euler(0, -90, 0));
        champion.GetComponent<PlayerPositionHandler>().initialPosition = new Point(10, 3);
        champion.GetComponent<PlayerPositionHandler>().isUp = true;
        champion.GetComponent<PlayerPositionHandler>().playerID = PlayerID.Player2;
        GameObject champion2 = Instantiate(champPrefab4, p2Spawn[1], Quaternion.Euler(0, -90, 0));
        champion2.GetComponent<PlayerPositionHandler>().initialPosition = new Point(10, 7);
        champion2.GetComponent<PlayerPositionHandler>().isUp = false;
        champion2.GetComponent<PlayerPositionHandler>().playerID = PlayerID.Player2;
    }

    // Awake est appel� lorsque le script est charg�
    void Awake()
    {
        GameObject musicManager = GameObject.Find("MusicAudioSource");
        Destroy(musicManager);
        //R�cup�rer les personnages choisis par les joueurs et faire apparaitre les pions
        int player1 = PlayerPrefs.GetInt("SpawnCharacterP1");
        int player2 = PlayerPrefs.GetInt("SpawnCharacterP2");
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
        playerName1.text = PlayerPrefs.GetString("PlayerName1");
        playerName2.text = PlayerPrefs.GetString("PlayerName2");
        //playerNameCine1.text = PlayerPrefs.GetString("PlayerName1");
        //playerNameCine2.text = PlayerPrefs.GetString("PlayerName2");
    }

    /// <summary>
    /// Convertir les coordonn�es du plateau en position de spawn
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private Vector3 setSpawnPosition(int x, int y)
    {
        return new Vector3(plateau.transform.GetChild(x).GetChild(y).transform.position.x, 2.1f, plateau.transform.GetChild(x).GetChild(y).transform.position.z);
    }

}
