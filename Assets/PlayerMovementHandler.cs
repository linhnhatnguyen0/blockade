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
    public Vector2 initial;
    // Start is called before the first frame update
    private Point currentPosition;

    public GameObject board;
    public GameObject plate;
    public LayerMask layerMask;
    private List<Point> mouvablePositions = new List<Point>();

    private int clickCounter = 0;
    void Start()
    {
        currentPosition = new Point((int)initial.x, (int)initial.y);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(clickCounter);
            if (clickCounter == 1)
            {
                Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray2, out RaycastHit hitP, Mathf.Infinity, layerMask))
                {
                    Debug.Log("hitP: " + hitP.transform.name);
                    if (hitP.transform.tag == "Mouvable")
                    {
                        clickCounter = 0;
                        Debug.Log("Mouvable clicked");
                        Transform cube = hitP.transform;
                        transform.position = new Vector3(cube.position.x, transform.position.y, cube.position.z);
                        GameObject[] plates = GameObject.FindGameObjectsWithTag("Plate");
                        foreach (GameObject plate in plates)
                        {
                            Destroy(plate);
                        }
                        GameObject[] mouvables = GameObject.FindGameObjectsWithTag("Mouvable");
                        foreach (GameObject mouvable in mouvables)
                        {
                            mouvable.tag = "Untagged";
                        }
                        mouvablePositions.Clear();
                        currentPosition = GetCubeFromBoard(cube);
                    }
                }
            }
            if (clickCounter == 0)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
                {
                    Debug.Log("hit: " + hit.transform.name);
                    if (hit.transform.tag == "Pions")
                    {
                        clickCounter++;
                        Debug.Log("Pion clicked");
                        Debug.Log(currentPosition);
                        mouvablePositions.Add(new Point(currentPosition.X - 2, currentPosition.Y));
                        mouvablePositions.Add(new Point(currentPosition.X - 1, currentPosition.Y + 1));
                        mouvablePositions.Add(new Point(currentPosition.X, currentPosition.Y + 2));
                        mouvablePositions.Add(new Point(currentPosition.X + 1, currentPosition.Y + 1));
                        mouvablePositions.Add(new Point(currentPosition.X + 2, currentPosition.Y));
                        mouvablePositions.Add(new Point(currentPosition.X + 1, currentPosition.Y - 1));
                        mouvablePositions.Add(new Point(currentPosition.X, currentPosition.Y - 2));
                        mouvablePositions.Add(new Point(currentPosition.X - 1, currentPosition.Y - 1));
                        List<Point> copy = new List<Point>(mouvablePositions);
                        foreach (Point point in copy)
                        {
                            if (point.X > 13 || point.X < 0 || point.Y < 0 || point.Y > 10)
                            {
                                mouvablePositions.Remove(point);
                            }
                        }
                        foreach (var item in mouvablePositions)
                        {
                            Debug.Log("Mouvable" + item);
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

    public Point GetCubeFromBoard(Transform cube)
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
}
