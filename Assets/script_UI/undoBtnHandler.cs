using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class undoBtnHandler : MonoBehaviour
{
    public GameObject playerMovement;
    public GameObject wallPut;
    public TMP_Text undoText;
    public GameObject hp1;
    public GameObject vp1;
    public GameObject hp2;
    public GameObject vp2;
    public void undo()
    {
        if (PlayerPrefs.GetInt("currentPhase") == 0)
        {
            playerMovement.GetComponent<PlayerMovementHandler>().undo();
        }
        else if (PlayerPrefs.GetInt("currentPhase") == 1)
        {
            Destroy(wallPut);
            undoText.text = (int.Parse(undoText.text) + 1).ToString();
            PlayerID playerID = wallPut.GetComponent<wallVerification>().playerID;
            if (playerID == PlayerID.Player1)
            {
                hp1.GetComponent<Button>().interactable = true;
                vp1.GetComponent<Button>().interactable = true;
                Color colorH1 = hp1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color;
                Color colorV1 = vp1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color;
                colorH1.a = 1f;
                colorV1.a = 1f;
                hp1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = colorH1;
                vp1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = colorV1;

            }
            else
            {
                hp2.GetComponent<Button>().interactable = true;
                vp2.GetComponent<Button>().interactable = true;
                Color colorH2 = hp1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color;
                Color colorV2 = vp1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color;
                colorH2.a = 1f;
                colorV2.a = 1f;
                hp2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = colorH2;
                vp2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = colorV2;
            }
        }
        this.GetComponent<Button>().interactable = false;
    }
}
