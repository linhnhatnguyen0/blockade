using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour
{
    public GameObject wallPreviewPrefab;
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
            wall.GetComponent<WallHandler>().playerID = PlayerPrefs.GetInt("currentPlayer") == 1 ? PlayerID.Player1 : PlayerID.Player2;
        }
        else
        {
            Destroy(GameObject.FindGameObjectWithTag("WallPreview"));
            getClosestPoint(transform.position);
            wall = Instantiate(wallPreviewPrefab, closestPoint, rotation);
            wall.GetComponent<WallHandler>().playerID = PlayerPrefs.GetInt("currentPlayer") == 1 ? PlayerID.Player1 : PlayerID.Player2;
            // Changement de la couleur du mur en fonction du joueur
        }
    }

    private void OnMouseUp()
    {
        wall.tag = "Untagged";
        Destroy(GameObject.FindGameObjectWithTag("WallDrag"));
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void getClosestPoint(Vector3 position)
    {
        //float distance = Vector3.Distance(wallPosition, transform.position);
        Vector3 hitPosition = Vector3.zero;
        SortedList<float, Vector2> closestPoints = new SortedList<float, Vector2>();

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Create a ray from the mouse position
        // Log ray line to console
        Debug.DrawRay(ray.origin, ray.direction * 40, Color.yellow);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            Debug.Log("Position wall" + position);
            Debug.Log("Hit: " + hit.collider.name);
            if (hit.collider.tag == "MappingPoint")
            {
                closestPoint = hit.transform.gameObject.transform.position;
            }
        }
    }
}