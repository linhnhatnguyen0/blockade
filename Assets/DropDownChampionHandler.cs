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
                PlayerPrefs.SetInt("SpawnCharacterP1", 0);
                Debug.Log("SpawnCharacterP1: " + PlayerPrefs.GetInt("SpawnCharacterP1"));
                break;
            case 1:
                Debug.Log("Sparrow");
                PlayerPrefs.SetInt("SpawnCharacterP1", 1);
                Debug.Log("SpawnCharacterP1: " + PlayerPrefs.GetInt("SpawnCharacterP1"));
                break;
            case 2:
                Debug.Log("Pudu");
                PlayerPrefs.SetInt("SpawnCharacterP1", 2);
                Debug.Log("SpawnCharacterP1: " + PlayerPrefs.GetInt("SpawnCharacterP1"));
                break;
            case 3:
                Debug.Log("Gekko");
                PlayerPrefs.SetInt("SpawnCharacterP1", 3);
                Debug.Log("SpawnCharacterP1: " + PlayerPrefs.GetInt("SpawnCharacterP1"));
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
                PlayerPrefs.SetInt("SpawnCharacterP2", 0);
                Debug.Log("SpawnCharacterP2: " + PlayerPrefs.GetInt("SpawnCharacterP2"));
                break;
            case 1:
                Debug.Log("Sparrow");
                PlayerPrefs.SetInt("SpawnCharacterP2", 1);
                Debug.Log("SpawnCharacterP2: " + PlayerPrefs.GetInt("SpawnCharacterP2"));
                break;
            case 2:
                Debug.Log("Pudu");
                PlayerPrefs.SetInt("SpawnCharacterP2", 2);
                Debug.Log("SpawnCharacterP2: " + PlayerPrefs.GetInt("SpawnCharacterP2"));
                break;
            case 3:
                Debug.Log("Gekko");
                PlayerPrefs.SetInt("SpawnCharacterP2", 3);
                Debug.Log("SpawnCharacterP2: " + PlayerPrefs.GetInt("SpawnCharacterP2"));
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
        PlayerPrefs.SetInt("SpawnCharacterP1", 0);
        PlayerPrefs.SetInt("SpawnCharacterP2", 0);
    }
}
