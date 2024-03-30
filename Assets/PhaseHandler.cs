using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class PhaseHandler : MonoBehaviour
{
    private PlayerID playerID = PlayerID.Player1;
    private int state = 0;
    private Color newcolor;
    private Color basecolor;
    public Slider PawnWall;
    public Slider WallEnd;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("currentPhase", 0);
    }
    public void changePhaseHandler()
    {
        state++;
        ChangeColor(state);
        PlayerPrefs.SetInt("currentPhase", state);
        if (state == 1)
        {
            PlayerPrefs.SetInt("clickCounter", 0);
        }
        if (state == 2)
        {
            StartCoroutine(DelayResetState());
        }
        // This is a placeholder for the function that will change the phase of the game
    }
    IEnumerator DelayResetState()
    {
        yield return new WaitForSeconds(3); // waits 3 seconds
        state = 0;
        ChangeColor(state);
        PlayerPrefs.SetInt("currentPhase", state);
        if (playerID == PlayerID.Player1)
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
    public void ChangeColor(int state)
    {
        newcolor = new Color(87 / 255f, 124 / 255f, 253 / 255f, 1f);
        basecolor = new Color(255, 255, 255, 1f);
        GameObject backgroundObj_PawnWall = PawnWall.transform.Find("Background").gameObject;
        GameObject backgroundObj_WallEnd = WallEnd.transform.Find("Background").gameObject;
        switch (state)
        {
            case 0:
                backgroundObj_PawnWall.GetComponent<Image>().color = basecolor;
                backgroundObj_WallEnd.GetComponent<Image>().color = basecolor;
                break;
            case 1:
                backgroundObj_PawnWall.GetComponent<Image>().color = newcolor;
                break;
            case 2:
                backgroundObj_WallEnd.GetComponent<Image>().color = newcolor;
                break;
            default:
                break;
        }

    }
}
