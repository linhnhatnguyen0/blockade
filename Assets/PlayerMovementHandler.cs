using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private GameObject board;
    public GameObject plate;
    public LayerMask layerMask;

    private float speed = 7f;
    private float rotationSpeed = 500f;

    private Animator anim;

    private int clickCounter = 0;
    private Transform cubeHit;

    private GameObject currentPlayer;

    private PlayerID currentPlayerID;

    private bool isMoving = false;
    void Start()
    {
        board = GameObject.Find("Board");
    }

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
            // Set cubeHit to null
            cubeHit = null;
            anim.SetBool("isFlying", false);
            isMoving = false;
        }
    }

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
        if (cubeHit != null && currentPlayer != null)
        {
            Vector3 targetPosition = new Vector3(cubeHit.position.x, currentPlayer.transform.position.y, cubeHit.position.z);
            movePlayerHandler(targetPosition);
            //Vector3 routePosition = new Vector3(cubeHit.position.x, currentPlayer.transform.position.y, cubeHit.position.z);
            // Rotate the object to face the target
        }
        if (!isMoving)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray2, out RaycastHit hit, Mathf.Infinity, layerMask))
                {
                    Debug.Log("Hit: " + hit.collider.tag);
                    Debug.Log(clickCounter);

                    if (clickCounter == 1)
                    {
                        if (hit.transform.tag == "Mouvable") // If the object hit is a position mouvable
                        {
                            cubeHit = hit.transform;
                            currentPlayer.GetComponent<PlayerPositionHandler>().initialPosition = GetCubeFromBoard(cubeHit);
                            deletePlaneAndRemoveMouvable();
                            clickCounter = 0;
                        }
                        else
                        {
                            deletePlaneAndRemoveMouvable();
                            clickCounter = 0;
                        }
                    }
                    if (clickCounter == 0)
                    {
                        if (hit.transform.tag == "Pions") // If the object hit is a player
                        {
                            clickCounter++;
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
                        }
                    }
                }
            }
        }
    }
}
