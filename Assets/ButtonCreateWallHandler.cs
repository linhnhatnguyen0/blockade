using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCreateWallHandler : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject wallPrefab; // The prefab to instantiate
    public bool isHorizontal;
    public void CreateWall()
    {
        if (PlayerPrefs.GetInt("currentPhase") != 1)
        {
            return;
        }
        GameObject wallExisted = GameObject.FindGameObjectWithTag("WallDrag");
        if (wallExisted != null)
        {
            Destroy(wallExisted);
        }
        Vector3 worldPosition = new Vector3(-7, 11, -16); // The position in world space to instantiate the prefab
                                                          //get array component with the tag "WallCreation"
        if (isHorizontal)
        {
            Instantiate(wallPrefab, worldPosition, Quaternion.identity); // Instantiate the prefab at the world position
        }
        else
        {
            Quaternion rotation = Quaternion.Euler(0, 90, 0);
            Instantiate(wallPrefab, worldPosition, rotation); // Instantiate the prefab at the world position
        }
    }


}
