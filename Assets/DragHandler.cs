using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

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
        if (!isHorizontal)
        {
            if (PlayerPrefs.GetInt("currentPlayer") == 1)
            {
                GameObject.FindGameObjectsWithTag("vp1")[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (int.Parse(GameObject.FindGameObjectsWithTag("vp1")[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text) - 1).ToString();
                Debug.Log("vp1" + GameObject.FindGameObjectsWithTag("vp1")[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);
            }
            else
            {
                GameObject.FindGameObjectsWithTag("vp2")[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (int.Parse(GameObject.FindGameObjectsWithTag("vp2")[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text) - 1).ToString();
                Debug.Log("vp2" + GameObject.FindGameObjectsWithTag("vp2")[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);
            }
        }
        else
        {
            if (PlayerPrefs.GetInt("currentPlayer") == 1)
            {
                GameObject.FindGameObjectsWithTag("hp1")[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (int.Parse(GameObject.FindGameObjectsWithTag("hp1")[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text) - 1).ToString();
                Debug.Log("hp1" + GameObject.FindGameObjectsWithTag("hp1")[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);
            }
            else
            {
                GameObject.FindGameObjectsWithTag("hp2")[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (int.Parse(GameObject.FindGameObjectsWithTag("hp2")[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text) - 1).ToString();
                Debug.Log("hp2" + GameObject.FindGameObjectsWithTag("hp2")[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);
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