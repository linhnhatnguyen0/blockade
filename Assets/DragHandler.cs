using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHandler : MonoBehaviour
{
    public GameObject wallPreviewPrefab;
    public GameObject wallPutterPrefab;
    public bool isHorizontal;
    private Transform closestPoint;
    private GameObject wall;
    Vector3 mOffset;
    public LayerMask layerMask;
    private GameObject undoBtnP1;
    private GameObject undoBtnP2;

    private GameObject hp1;
    private GameObject vp1;
    private GameObject hp2;
    private GameObject vp2;

    private GameObject endturn_btnP1;
    private GameObject endturn_btnP2;

    private Partie partie;
    private int closestPointIndex;
    private Point cubeTopLeftPosition;

    private void Start()
    {
        hp1 = GameObject.FindGameObjectsWithTag("hp1")[0];
        vp1 = GameObject.FindGameObjectsWithTag("vp1")[0];
        hp2 = GameObject.FindGameObjectsWithTag("hp2")[0];
        vp2 = GameObject.FindGameObjectsWithTag("vp2")[0];
        endturn_btnP1 = GameObject.Find("endturn_btnP1");
        endturn_btnP2 = GameObject.Find("endturn_btnP2");
        undoBtnP1 = GameObject.Find("undo_btnP1");
        undoBtnP2 = GameObject.Find("undo_btnP2");
        partie = GameObject.Find("Logic").GetComponent<LogicScript>().partie;
    }

    private void OnMouseDown()
    {
        mOffset = transform.position - GetMouseWorldPos();
    }
    /// <summary>
    /// 
    /// </summary>
    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + mOffset;
        Quaternion rotation = Quaternion.Euler(0, this.transform.rotation.eulerAngles.y, 0);
        if (closestPoint != null)
        {
            Destroy(GameObject.FindGameObjectWithTag("WallPreview"));
        }
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Create a ray from the mouse position
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            if (hit.collider.tag == "MappingPoint")
            {
                closestPoint = hit.transform.gameObject.transform;
            }
        }
        wall = Instantiate(wallPreviewPrefab, closestPoint.position, rotation);
        wall.GetComponent<wallVerification>().isHorizontal = isHorizontal;
        closestPointIndex = int.Parse(Regex.Match(closestPoint.name, @"\d+").Value);
        cubeTopLeftPosition = new Point(closestPointIndex / 10, closestPointIndex % 10);
        if (partie.canPlaceWall(cubeTopLeftPosition.X, cubeTopLeftPosition.Y, isHorizontal))
        {
            wall.GetComponent<Renderer>().material.color = Color.green;
        }
        else
        {
            wall.GetComponent<Renderer>().material.color = Color.red;
        }
    }


    private void OnMouseUp()
    {
        Destroy(GameObject.FindGameObjectWithTag("WallDrag"));
        if (wall.GetComponent<Renderer>().material.color == Color.red)
        {
            Destroy(wall);
            return;
        }
        wall.GetComponent<Renderer>().material.color = Color.blue;
        wall.tag = "Untagged";
        wall.GetComponent<wallVerification>().playerID = PlayerPrefs.GetInt("currentPlayer") == 1 ? PlayerID.Player1 : PlayerID.Player2;
        Debug.Log("Cube attached: " + cubeTopLeftPosition);
        partie.placeWall(cubeTopLeftPosition.X, cubeTopLeftPosition.Y, isHorizontal);
        if (PlayerPrefs.GetInt("currentPlayer") == 1)
        {
            undoBtnP1.GetComponent<undoBtnHandler>().wallPut = wall;
            if (!isHorizontal)
            {
                vp1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (int.Parse(vp1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text) - 1).ToString();
                undoBtnP1.GetComponent<undoBtnHandler>().undoText = vp1.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                changeWallButtonColor(false, false, false, false);
            }
            else
            {
                hp1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (int.Parse(hp1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text) - 1).ToString();
                undoBtnP1.GetComponent<undoBtnHandler>().undoText = hp1.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                changeWallButtonColor(false, false, false, false);
            }
            endturn_btnP1.GetComponent<Button>().interactable = true;
            undoBtnP1.GetComponent<Button>().interactable = true;
        }
        else
        {
            undoBtnP2.GetComponent<undoBtnHandler>().wallPut = wall;
            if (!isHorizontal)
            {
                vp2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (int.Parse(vp2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text) - 1).ToString();
                undoBtnP2.GetComponent<undoBtnHandler>().undoText = vp2.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                changeWallButtonColor(false, false, false, false);
            }
            else
            {
                hp2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (int.Parse(hp2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text) - 1).ToString();
                undoBtnP2.GetComponent<undoBtnHandler>().undoText = hp2.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                changeWallButtonColor(false, false, false, false);
            }
            endturn_btnP2.GetComponent<Button>().interactable = true;
            undoBtnP2.GetComponent<Button>().interactable = true;
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    public void changeWallButtonColor(bool hp1b, bool vp1b, bool hp2b, bool vp2b)
    {
        hp1.GetComponent<Button>().interactable = hp1b;
        vp1.GetComponent<Button>().interactable = vp1b;
        hp2.GetComponent<Button>().interactable = hp2b;
        vp2.GetComponent<Button>().interactable = vp2b;
        Color colorH1 = hp1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color;
        Color colorV1 = vp1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color;
        Color colorH2 = hp1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color;
        Color colorV2 = vp1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color;
        colorH1.a = hp1b ? 1f : 0.5f;
        colorV1.a = vp1b ? 1f : 0.5f;
        colorH2.a = hp2b ? 1f : 0.5f;
        colorV2.a = vp2b ? 1f : 0.5f;
        hp1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = colorH1;
        vp1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = colorV1;
        hp2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = colorH2;
        vp2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = colorV2;
    }
}