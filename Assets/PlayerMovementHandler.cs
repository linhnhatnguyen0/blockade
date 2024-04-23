using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private float speed = 7f;   // La vitesse de d�placement
    private float rotationSpeed = 500f; // La vitesse de rotation

    private Animator anim;  // Le controlleur de l'animation

    private Transform cubeHit;

    private GameObject currentPlayer;

    private PlayerID currentPlayerID;

    private bool isMoving = false;
    void Start()
    {
        board = GameObject.Find("Board");
        PlayerPrefs.SetInt("clickCounter", 0);
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

    List<Point> UpdateMovablePosition(Point currentPosition)
    {
        List<Point> mouvablePositions = new List<Point>();
        mouvablePositions.Add(new Point(currentPosition.X - 2, currentPosition.Y));
        mouvablePositions.Add(new Point(currentPosition.X - 1, currentPosition.Y + 1));
        mouvablePositions.Add(new Point(currentPosition.X, currentPosition.Y + 2));
        mouvablePositions.Add(new Point(currentPosition.X + 1, currentPosition.Y + 1));
        mouvablePositions.Add(new Point(currentPosition.X + 2, currentPosition.Y));
        mouvablePositions.Add(new Point(currentPosition.X + 1, currentPosition.Y - 1));
        mouvablePositions.Add(new Point(currentPosition.X, currentPosition.Y - 2));
        mouvablePositions.Add(new Point(currentPosition.X - 1, currentPosition.Y - 1));
        Point point1 = new Point(currentPosition.X, currentPosition.Y + 1);
        Point point2 = new Point(currentPosition.X + 1, currentPosition.Y);
        Point point3 = new Point(currentPosition.X, currentPosition.Y - 1);
        Point point4 = new Point(currentPosition.X - 1, currentPosition.Y);
        List<Point> listPointDestination = new List<Point>();
        listPointDestination.Add(point1);
        listPointDestination.Add(point2);
        listPointDestination.Add(point3);
        listPointDestination.Add(point4);

        List<Point> copy = new List<Point>(mouvablePositions);
        foreach (Point point in copy)
        {
            if (point.X > 13 || point.X < 0 || point.Y < 0 || point.Y > 10)
            {
                mouvablePositions.Remove(point);
            }
            //verifyMovablePosition(point); 
        }
        if (currentPlayerID == PlayerID.Player1)
        {
            foreach (var point in listPointDestination)
            {
                if ((point.X == 10 && point.Y == 3) || (point.X == 10 && point.Y == 7))
                {
                    mouvablePositions.Add(point);
                }
            }
        }
        else
        {
            foreach (var point in listPointDestination)
            {
                if ((point.X == 3 && point.Y == 3) || (point.X == 3 && point.Y == 7))
                {
                    mouvablePositions.Add(point);
                }
            }
        }
        return mouvablePositions;
    }

    /// <summary>
    /// La fonction de gestion du rotation et du mouvement du pion
    /// </summary>
    /// <param name="targetPosition"></param>
    void movePlayerHandler(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - currentPlayer.transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            currentPlayer.transform.rotation = Quaternion.RotateTowards(currentPlayer.transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            if (currentPlayer.transform.rotation == toRotation)
            {
                currentPlayer.transform.position = Vector3.MoveTowards(currentPlayer.transform.position, targetPosition, Time.deltaTime * speed);
                isMoving = true;
                anim.SetBool("isFlying", true);
            }
        }
        else
        {
            currentPlayer.transform.position = Vector3.MoveTowards(currentPlayer.transform.position, targetPosition, Time.deltaTime * speed);
            isMoving = true;
            anim.SetBool("isFlying", true);
        }
        if (currentPlayer.transform.position == targetPosition)
        {
            cubeHit = null;
            anim.SetBool("isFlying", false);
            isMoving = false;
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

    private void Update()
    {
        // Mise � jour du joueur courant
        currentPlayerID = PlayerPrefs.GetInt("currentPlayer") == 1 ? PlayerID.Player1 : PlayerID.Player2;

        // Si le pion et la position de destination sont d�finis
        if (cubeHit != null && currentPlayer != null)
        {
            Vector3 targetPosition = new Vector3(cubeHit.position.x, currentPlayer.transform.position.y, cubeHit.position.z);
            movePlayerHandler(targetPosition);
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
                                PlayerPrefs.SetInt("clickCounter", 2);
                                cubeHit = hit.transform;
                                currentPlayer.GetComponent<PlayerPositionHandler>().initialPosition = GetCubeFromBoard(cubeHit);
                                deletePlaneAndRemoveMouvable();
                                GameObject btn = currentPlayerID == PlayerID.Player1 ? GameObject.Find("endturn_btnP1") : GameObject.Find("endturn_btnP2");
                                btn.GetComponent<Button>().interactable = true;
                            }
                            else
                            {
                                PlayerPrefs.SetInt("clickCounter", 0);
                                deletePlaneAndRemoveMouvable();
                                GameObject btn = currentPlayerID == PlayerID.Player1 ? GameObject.Find("endturn_btnP1") : GameObject.Find("endturn_btnP2");
                                btn.GetComponent<Button>().interactable = true;
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
                                    anim = currentPlayer.GetComponent<Animator>();
                                    currentPlayerID = currentPlayer.GetComponent<PlayerPositionHandler>().playerID;
                                    List<Point> mouvablePositions = UpdateMovablePosition(currentPlayer.GetComponent<PlayerPositionHandler>().initialPosition);
                                    foreach (var item in mouvablePositions)
                                    {
                                        Transform cube = board.transform.GetChild(item.X).GetChild(item.Y);
                                        cube.tag = "Mouvable";
                                        cube.gameObject.layer = 0;
                                        Instantiate(plate, new Vector3(cube.transform.position.x, cube.transform.position.y + (float)1.1, cube.transform.position.z), Quaternion.identity).tag = "Plate";
                                    }
                                    GameObject btn = currentPlayerID == PlayerID.Player1 ? GameObject.Find("endturn_btnP1") : GameObject.Find("endturn_btnP2");
                                    btn.GetComponent<Button>().interactable = false;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

