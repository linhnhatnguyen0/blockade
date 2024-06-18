using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Blockade;
using UnityEngine.SceneManagement;

public struct Point
{
    public int X { get; set; }
    public int Y { get; set; }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override string ToString() => "(" + X + ", " + Y + ")";
}

public class PlayerMovementHandler : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject board;   // Le plateau de jeu
    public GameObject plate;    // Le prefab de la position mouvable
    public LayerMask layerMask;
    public GameObject endTurnBtnP1;
    public GameObject endTurnBtnP2;
    public GameObject undoBtnP1;
    public GameObject undoBtnP2;

    private float speed = 7f;   // La vitesse de d�placement
    private float rotationSpeed = 500f; // La vitesse de rotation

    private Animator anim;  // Le controlleur de l'animation

    private Transform cubeHit;

    public GameObject currentPlayer;

    private PlayerID currentPlayerID;

    private IHMLink partie;

    private bool isMoving = false;

    public Point previousPosition;
    private Vector3 previousRotation;

    private Vector3 targetPosition;

    public GameObject phaseHandler;

    public GameObject soundEffect;

    public SoundEffect sfx;
    void Start()
    {
        board = GameObject.Find("Board");
        PlayerPrefs.SetInt("clickCounter", 0);
        sfx = GameObject.Find("SFXAudioSource").GetComponent<SoundEffect>();
    }


    /// <summary>
    /// La fonction de r�cup�rer la position du cube sur le board
    /// </summary>
    /// <param name="cube"></param>
    /// <returns></returns>
    public static Point GetCubeFromBoard(Transform cube)
    {
        Transform line = cube.parent;
        Transform board = line.parent;
        int indexCube = 0;
        int indexLine = 0;
        for (int i = 0; i < line.childCount; i++)
        {
            if (line.GetChild(i).gameObject.transform == cube)
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
        return new Point(indexLine, indexCube);
    }
    public static Vector3 GetCubePositionFromBoard(Point cube)
    {
        GameObject board = GameObject.Find("Board");
        return board.transform.GetChild(cube.X).GetChild(cube.Y).position;
    }
    /// <summary>
    /// La fonction de gestion du rotation et du mouvement du pion
    /// </summary>
    /// <param name="targetPawn" name="targetPosition"></param>
    public void movePlayerHandler(GameObject targetPawn, Vector3 targetPosition, Transform cubeHit)
    {
        anim = targetPawn.GetComponent<Animator>();
        Vector3 direction = (targetPosition - targetPawn.transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            targetPawn.transform.rotation = Quaternion.RotateTowards(targetPawn.transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            if (targetPawn.transform.rotation == toRotation)
            {
                Debug.Log("Direction2");
                if (SceneManager.GetActiveScene().name == "InGameSceneBOT" && targetPawn.GetComponent<PlayerPositionHandler>().playerID == PlayerID.Player2)
                {
                    targetPawn.transform.position = targetPosition;
                }
                else
                {
                    targetPawn.transform.position = Vector3.MoveTowards(targetPawn.transform.position, targetPosition, Time.deltaTime * speed);
                }
                isMoving = true;
                anim.SetBool("isFlying", true);
            }
        }
        else
        {
            targetPawn.transform.position = Vector3.MoveTowards(targetPawn.transform.position, targetPosition, Time.deltaTime * speed);
            isMoving = true;
            anim.SetBool("isFlying", true);
        }
        if (targetPawn.transform.position == targetPosition)
        {
            anim.SetBool("isFlying", false);
            isMoving = false;
            Point point = GetCubeFromBoard(cubeHit);
            if (currentPlayer.GetComponent<PlayerPositionHandler>().playerID == PlayerID.Player1 && ((point.X == 10 && point.Y == 3) || (point.X == 10 && point.Y == 7)))
            {
                if (!phaseHandler.GetComponent<PhaseHandler>())
                {
                    phaseHandler.GetComponent<PhaseHandlerBOT>().victore(PlayerID.Player1);
                }
                phaseHandler.GetComponent<PhaseHandler>().victore(PlayerID.Player1);
            }
            if (currentPlayer.GetComponent<PlayerPositionHandler>().playerID == PlayerID.Player2 && ((point.X == 3 && point.Y == 3) || (point.X == 3 && point.Y == 7)))
            {
                if (!phaseHandler.GetComponent<PhaseHandler>())
                {
                    phaseHandler.GetComponent<PhaseHandlerBOT>().victore(PlayerID.Player2);
                }
                phaseHandler.GetComponent<PhaseHandler>().victore(PlayerID.Player2);
            }
        }
    }

    /// <summary>
    /// La fonction de supprimer les planes et le tag "Mouvable" sur les cubes
    /// </summary>
    private void deletePlaneAndRemoveMouvable()
    {
        GameObject[] plates = GameObject.FindGameObjectsWithTag("Plate");
        GameObject[] mouvables = GameObject.FindGameObjectsWithTag("Mouvable");
        if (plates.Length == 0 || mouvables.Length == 0)
        {
            return;
        }
        foreach (GameObject plate in plates)
        {
            Destroy(plate);
        }
        foreach (GameObject mouvable in mouvables)
        {
            mouvable.tag = "Untagged";
        }
    }

    public void undo()
    {
        PlayerPrefs.SetInt("clickCounter", 0);
        currentPlayer.GetComponent<PlayerPositionHandler>().initialPosition = previousPosition;
        Vector3 targetPosition = new Vector3(board.transform.GetChild(previousPosition.X).GetChild(previousPosition.Y).position.x, (float)2.1, board.transform.GetChild(previousPosition.X).GetChild(previousPosition.Y).position.z);
        currentPlayer.transform.position = targetPosition;
        currentPlayer.transform.eulerAngles = previousRotation;
        _ = currentPlayerID == PlayerID.Player1 ? endTurnBtnP1.GetComponent<Button>().interactable = false : endTurnBtnP2.GetComponent<Button>().interactable = false;
        _ = currentPlayerID == PlayerID.Player1 ? undoBtnP1.GetComponent<Button>().interactable = false : undoBtnP2.GetComponent<Button>().interactable = false;
    }

    private void Update()
    {
        partie = GameObject.Find("Logic").GetComponent<LogicScript>().partie;
        // Mise � jour du joueur courant
        currentPlayerID = PlayerPrefs.GetInt("currentPlayer") == 1 ? PlayerID.Player1 : PlayerID.Player2;

        // Si le pion et la position de destination sont d�finis
        if (cubeHit != null && currentPlayer != null)
        {
            targetPosition = new Vector3(cubeHit.position.x, currentPlayer.transform.position.y, cubeHit.position.z);
            Debug.Log("Target position: " + targetPosition);
            Debug.Log("Current player " + currentPlayer);
            movePlayerHandler(currentPlayer, targetPosition, cubeHit);
        }
        if (!isMoving)
        {
            //----La gestion du click sur l'�cran----//
            // Le clic est s�par� en deux etapes.
            // Le premier �tape est � d�tecter le pion cliqu� et afficher les positions mouvables.
            // Le deuxi�me �tape est � d�tecter la position mouvable cliqu�e et d�placer le pion ou si le joueur clique sur un autre pion, on annule le premier �tape.
            //---------------------------------------//
            if (PlayerPrefs.GetInt("currentPhase") == 0)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray2, out RaycastHit hit, Mathf.Infinity, layerMask))
                    {
                        if (PlayerPrefs.GetInt("clickCounter") == 1)
                        {
                            if (hit.transform.tag == "Mouvable") // Si la position cliqu�e est mouvable, on d�place le pion
                            {
                                //PLAY SOUND
                                sfx.PawnSound();
                                PlayerPrefs.SetInt("clickCounter", 2);
                                cubeHit = hit.transform;
                                previousPosition = currentPlayer.GetComponent<PlayerPositionHandler>().initialPosition;
                                currentPlayer.GetComponent<PlayerPositionHandler>().initialPosition = GetCubeFromBoard(cubeHit);
                                deletePlaneAndRemoveMouvable();
                                _ = currentPlayerID == PlayerID.Player1 ? endTurnBtnP1.GetComponent<Button>().interactable = true : endTurnBtnP2.GetComponent<Button>().interactable = true;
                                _ = currentPlayerID == PlayerID.Player1 ? undoBtnP1.GetComponent<Button>().interactable = true : undoBtnP2.GetComponent<Button>().interactable = true;
                                previousRotation = currentPlayer.transform.eulerAngles;
                            }
                            else
                            {
                                PlayerPrefs.SetInt("clickCounter", 0);
                                deletePlaneAndRemoveMouvable();
                            }
                        }
                        if (PlayerPrefs.GetInt("clickCounter") == 0)
                        {
                            if (hit.transform.tag == "Pions") // Si le pion cliqu� est valide, on affiche les positions mouvables
                            {
                                if (hit.transform.GetComponent<PlayerPositionHandler>().playerID == currentPlayerID)
                                {
                                    PlayerPrefs.SetInt("clickCounter", 1);
                                    currentPlayer = hit.transform.gameObject;
                                    currentPlayerID = currentPlayer.GetComponent<PlayerPositionHandler>().playerID;
                                    List<Point> mouvablePositions = partie.canMovePosition(currentPlayer.GetComponent<PlayerPositionHandler>().initialPosition);
                                    foreach (var item in mouvablePositions)
                                    {
                                        Transform cube = board.transform.GetChild(item.X).GetChild(item.Y);
                                        cube.tag = "Mouvable";
                                        cube.gameObject.layer = 0;
                                        Instantiate(plate, new Vector3(cube.transform.position.x, cube.transform.position.y + (float)1.1, cube.transform.position.z), Quaternion.identity).tag = "Plate";
                                    }
                                    _ = currentPlayerID == PlayerID.Player1 ? endTurnBtnP1.GetComponent<Button>().interactable = false : endTurnBtnP2.GetComponent<Button>().interactable = false;
                                    _ = currentPlayerID == PlayerID.Player1 ? undoBtnP1.GetComponent<Button>().interactable = false : undoBtnP2.GetComponent<Button>().interactable = false;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

