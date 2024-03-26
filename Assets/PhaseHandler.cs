using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhaseHandler : MonoBehaviour
{
    private PlayerID playerID = PlayerID.Player1;
    private int state = 0;
    private Color newcolor;
    private Color basecolor;
    public Slider PawnWall;
    public Slider WallEnd;
    // Start is called before the first frame update
    public void changePhaseHandler()
    {
        ChangeColor(state);
        state++;
        if(state == 3)
        {
            state = 0;
            if(playerID == PlayerID.Player1)
            {
                playerID = PlayerID.Player2;
                PlayerPrefs.SetInt("currentPlayer", 2);
            }
            else
            {
                playerID = PlayerID.Player1;
                PlayerPrefs.SetInt("currentPlayer", 1);
            }
        }
        // This is a placeholder for the function that will change the phase of the game
    }
    public void ChangeColor(int state)
    {
        newcolor = new Color(255, 0, 0, 1f);
        basecolor = new Color(255, 255, 255, 1f);
        GameObject backgroundObj_PawnWall = PawnWall.transform.Find("Background").gameObject;
        GameObject backgroundObj_WallEnd = WallEnd.transform.Find("Background").gameObject;
        switch (state)
        {
            case 0:
                backgroundObj_PawnWall.GetComponent<Image>().color = newcolor;
                break;
            case 1:
                backgroundObj_WallEnd.GetComponent<Image>().color = newcolor;
                break;
            case 2:
                backgroundObj_PawnWall.GetComponent<Image>().color = basecolor;
                backgroundObj_WallEnd.GetComponent<Image>().color = basecolor;
                break;
            default:
                break;
        }

    }
}
