using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class DropDownChampionHandler : MonoBehaviour
{
    public void OnDropdownValueChangedP1(int index)
    {
        switch (index)
        {
            case 0:
                Debug.Log("Squid");
                PlayerPrefs.SetInt("Player1", 0);
                Debug.Log("Player1: " + PlayerPrefs.GetInt("Player1"));
                break;
            case 1:
                Debug.Log("Sparrow");
                PlayerPrefs.SetInt("Player1", 1);
                Debug.Log("Player1: " + PlayerPrefs.GetInt("Player1"));
                break;
            case 2:
                Debug.Log("Pudu");
                PlayerPrefs.SetInt("Player1", 2);
                Debug.Log("Player1: " + PlayerPrefs.GetInt("Player1"));
                break;
            case 3:
                Debug.Log("Gekko");
                PlayerPrefs.SetInt("Player1", 3);
                Debug.Log("Player1: " + PlayerPrefs.GetInt("Player1"));
                break;
            default:
                Debug.Log("Default");
                break;
        }
    }

    public void OnDropdownValueChangedP2(int index)
    {
        switch (index)
        {
            case 0:
                Debug.Log("Squid");
                PlayerPrefs.SetInt("Player2", 0);
                Debug.Log("Player2: " + PlayerPrefs.GetInt("Player2"));
                break;
            case 1:
                Debug.Log("Sparrow");
                PlayerPrefs.SetInt("Player2", 1);
                Debug.Log("Player2: " + PlayerPrefs.GetInt("Player2"));
                break;
            case 2:
                Debug.Log("Pudu");
                PlayerPrefs.SetInt("Player2", 2);
                Debug.Log("Player2: " + PlayerPrefs.GetInt("Player2"));
                break;
            case 3:
                Debug.Log("Gekko");
                PlayerPrefs.SetInt("Player2", 3);
                Debug.Log("Player2: " + PlayerPrefs.GetInt("Player2"));
                break;
            default:
                Debug.Log("Default");
                break;
        }
    }

    public void buttonHandler()
    {
        SceneManager.LoadScene("InGameScene");
    }

    private void Awake()
    {
        PlayerPrefs.SetInt("Player1", 0);
        PlayerPrefs.SetInt("Player2", 0);
    }
}
