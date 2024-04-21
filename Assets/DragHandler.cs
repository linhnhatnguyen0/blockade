using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHandler : MonoBehaviour
{
    public GameObject wallPreviewPrefab;
    public bool isHorizontal;
    private Vector3 closestPoint;
    private GameObject wall;
    Vector3 mOffset;
    public LayerMask layerMask;
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
        if (closestPoint == null)
        {
            getClosestPoint(transform.position);
            wall = Instantiate(wallPreviewPrefab, closestPoint, rotation);
        }
        Destroy(GameObject.FindGameObjectWithTag("WallPreview"));
        getClosestPoint(transform.position);
        wall = Instantiate(wallPreviewPrefab, closestPoint, rotation);
        // Changement de la couleur du mur en fonction du joueur
    }


    private void OnMouseUp()
    {
        wall.tag = "Untagged";
        wall.GetComponent<wallHandler>().playerID = PlayerPrefs.GetInt("currentPlayer") == 1 ? PlayerID.Player1 : PlayerID.Player2;
        Destroy(GameObject.FindGameObjectWithTag("WallDrag"));
        GameObject vp1 = GameObject.FindGameObjectsWithTag("vp1")[0];
        GameObject hp1 = GameObject.FindGameObjectsWithTag("hp1")[0];
        GameObject vp2 = GameObject.FindGameObjectsWithTag("vp2")[0];
        GameObject hp2 = GameObject.FindGameObjectsWithTag("hp2")[0];
        if (!isHorizontal)
        {
            if (PlayerPrefs.GetInt("currentPlayer") == 1)
            {
                vp1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (int.Parse(vp1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text) - 1).ToString();
                hp1.GetComponent<Button>().enabled = false;
                vp1.GetComponent<Button>().enabled = false;
            }
            else
            {
                vp2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (int.Parse(vp2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text) - 1).ToString();
                hp2.GetComponent<Button>().enabled = false;
                vp2.GetComponent<Button>().enabled = false;
            }
        }
        else
        {
            if (PlayerPrefs.GetInt("currentPlayer") == 1)
            {
                hp1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (int.Parse(hp1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text) - 1).ToString();
                hp1.GetComponent<Button>().enabled = false;
                vp1.GetComponent<Button>().enabled = false;
            }
            else
            {
                hp2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (int.Parse(hp2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text) - 1).ToString();
                hp2.GetComponent<Button>().enabled = false;
                vp2.GetComponent<Button>().enabled = false;
            }
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void getClosestPoint(Vector3 position)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Create a ray from the mouse position
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            if (hit.collider.tag == "MappingPoint")
            {
                closestPoint = hit.transform.gameObject.transform.position;
            }
        }
    }
}