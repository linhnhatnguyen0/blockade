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
    public GameObject btnValidP1;
    public GameObject btnValidP2;
    private int indexP1 = 0;
    private int indexP2 = 0;
    private bool validP1;
    private bool validP2;
    public GameObject btnPlayGame;

    private void Start()
    {
        btnPlayGame.GetComponent<Button>().interactable = false;
        validP1 = false;
        validP2 = false;
    }

    public void validHandlerP1()
    {
        if (btnValidP2.GetComponent<Button>().interactable == false)
        {
            btnPlayGame.GetComponent<Button>().interactable = true;
        }
        this.GetComponent<Button>().interactable = false;
    }
    public void validHandlerP2()
    {
        if (btnValidP1.GetComponent<Button>().interactable == false)
        {
            btnPlayGame.GetComponent<Button>().interactable = true;
        }
        this.GetComponent<Button>().interactable = false;
    }

    public void OnDropdownValueChangedP1()
    {
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
        SceneManager.LoadScene("InGameScene");
    }

    private void Awake()
    {
        PlayerPrefs.SetInt("SpawnCharacterP1", 0);
        PlayerPrefs.SetInt("SpawnCharacterP2", 0);
    }
}
