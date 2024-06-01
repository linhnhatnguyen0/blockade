using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;



public class DropDownChampionHandler : MonoBehaviour
{
    public TextMeshProUGUI textP1;
    public TextMeshProUGUI textP2;
    private int indexP1 = 0;
    private int indexP2 = 0;
    public GameObject btnPlayGame;
    public TextMeshProUGUI playerName1;
    public TextMeshProUGUI playerName2;

    public void OnDropdownValueChangedP1()
    {
        Debug.Log(textP1);
        if (this.name == "DroiteP1")
        {
            indexP1++;
        }
        else
        {
            indexP1--;
        }
        if (indexP1 == 4)
        {
            indexP1 = 0;
        }
        else if (indexP1 == -1)
        {
            indexP1 = 3;
        }
        switch (indexP1)
        {
            case 0:
                textP1.text = "Squid";
                PlayerPrefs.SetInt("SpawnCharacterP1", 0);
                break;
            case 1:
                textP1.text = "Sparrow";
                PlayerPrefs.SetInt("SpawnCharacterP1", 1);
                break;
            case 2:
                textP1.text = "Pudu";
                PlayerPrefs.SetInt("SpawnCharacterP1", 2);
                break;
            case 3:
                textP1.text = "Gekko";
                PlayerPrefs.SetInt("SpawnCharacterP1", 3);
                break;
            default:
                Debug.Log("Default");
                break;
        }
    }

    public void OnDropdownValueChangedP2()
    {
        if (this.name == "DroiteP2")
        {
            indexP2++;
        }
        else
        {
            indexP2--;
        }
        if (indexP2 == 4)
        {
            indexP2 = 0;
        }
        else if (indexP2 == -1)
        {
            indexP2 = 3;
        }
        switch (indexP2)
        {
            case 0:
                PlayerPrefs.SetInt("SpawnCharacterP2", 0);
                textP2.text = "Squid";
                break;
            case 1:
                textP2.text = "Sparrow";
                PlayerPrefs.SetInt("SpawnCharacterP2", 1);
                break;
            case 2:
                textP2.text = "Pudu";
                PlayerPrefs.SetInt("SpawnCharacterP2", 2);
                break;
            case 3:
                textP2.text = "Gekko";
                PlayerPrefs.SetInt("SpawnCharacterP2", 3);
                break;
            default:
                Debug.Log("Default");
                break;
        }
    }

    public void buttonHandler()
    {
        PlayerPrefs.SetString("PlayerName1", playerName1.text);
        PlayerPrefs.SetString("PlayerName2", playerName2.text);
        Debug.Log(PlayerPrefs.GetString("PlayerName1"));
        Debug.Log(PlayerPrefs.GetString("PlayerName2"));
        SceneManager.LoadScene("InGameScene");
    }
    public void buttonHandlerBOT()
    {
        PlayerPrefs.SetString("PlayerName1", playerName1.text);
        PlayerPrefs.SetString("PlayerName2", playerName2.text);
        Debug.Log(PlayerPrefs.GetString("PlayerName1"));
        Debug.Log(PlayerPrefs.GetString("PlayerName2"));
        SceneManager.LoadScene("InGameSceneBOT");
    }

    private void Awake()
    {
        PlayerPrefs.SetInt("SpawnCharacterP1", 0);
        PlayerPrefs.SetInt("SpawnCharacterP2", 0);
    }
}
