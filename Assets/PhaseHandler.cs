using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class PhaseHandler : MonoBehaviour
{
    private PlayerID playerID;
    private int state = 0;
    private Color newcolor;
    private Color basecolor;
    public Slider PawnWall;
    public Slider WallEnd;
    public GameObject isMyTurnBtnP1;
    public GameObject isMyTurnBtnP2;
    public GameObject hp1;
    public GameObject vp1;
    public GameObject hp2;
    public GameObject vp2;
    public GameObject endturn_btnP1;
    public GameObject endturn_btnP2;
    public GameObject phaseBar1;
    public GameObject phaseBar2;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("currentPhase", 0);
        playerID = PlayerID.Player1;
        hp1.GetComponent<Button>().enabled = false;
        vp1.GetComponent<Button>().enabled = false;
        hp2.GetComponent<Button>().enabled = false;
        vp2.GetComponent<Button>().enabled = false;
        isMyTurnBtnP1.SetActive(true);
        isMyTurnBtnP2.SetActive(false);
        phaseBar1.GetComponent<Image>().fillAmount = 0;
        phaseBar2.GetComponent<Image>().fillAmount = 0;
    }
    public void changePhaseHandler()
    {
        state++;
        ChangeColor(state);
        PlayerPrefs.SetInt("currentPhase", state);
        if (state == 1)
        {
            PlayerPrefs.SetInt("clickCounter", 0);
            if (PlayerPrefs.GetInt("currentPlayer") == 1)
            {
                hp1.GetComponent<Button>().enabled = true;
                vp1.GetComponent<Button>().enabled = true;
                hp2.GetComponent<Button>().enabled = false;
                vp2.GetComponent<Button>().enabled = false;
            }
            else
            {
                hp1.GetComponent<Button>().enabled = false;
                vp1.GetComponent<Button>().enabled = false;
                hp2.GetComponent<Button>().enabled = true;
                vp2.GetComponent<Button>().enabled = true;
            }
        }
        if (state == 2)
        {
            hp1.GetComponent<Button>().enabled = false;
            vp1.GetComponent<Button>().enabled = false;
            hp2.GetComponent<Button>().enabled = false;
            vp2.GetComponent<Button>().enabled = false;
            StartCoroutine(DelayResetState());
        }
        // This is a placeholder for the function that will change the phase of the game
    }
    IEnumerator DelayResetState()
    {
        yield return new WaitForSeconds(1); // waits 3 seconds
        state = 0;
        ChangeColor(state);
        PlayerPrefs.SetInt("currentPhase", state);
        if (PlayerPrefs.GetInt("currentPlayer") == 1)
        {
            PlayerPrefs.SetInt("currentPlayer", 2);
            isMyTurnBtnP1.SetActive(false);
            isMyTurnBtnP2.SetActive(true);
        }
        else
        {
            PlayerPrefs.SetInt("currentPlayer", 1);
            isMyTurnBtnP1.SetActive(true);
            isMyTurnBtnP2.SetActive(false);
        }
    }
    public void ChangeColor(int state)
    {
        switch (state)
        {
            case 0:
                phaseBar1.GetComponent<Image>().fillAmount = 0;
                phaseBar2.GetComponent<Image>().fillAmount = 0;
                break;
            case 1:
                StartCoroutine(FillImage(phaseBar1.GetComponent<Image>()));
                break;
            case 2:
                StartCoroutine(FillImage(phaseBar2.GetComponent<Image>()));
                break;
            default:
                break;
        }
    }

    IEnumerator FillImage(Image image)
    {
        float timeElapsed = 0;
        float duration = 0.5f; // Duration in seconds

        while (timeElapsed < duration)
        {
            image.fillAmount = timeElapsed / duration;
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        image.fillAmount = 1; // Ensure the fill amount is set to 1 at the end
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("currentPlayer") == 1)
        {
            endturn_btnP1.GetComponent<Button>().enabled = true;
            endturn_btnP2.GetComponent<Button>().enabled = false;
        }
        else
        {
            endturn_btnP1.GetComponent<Button>().enabled = false;
            endturn_btnP2.GetComponent<Button>().enabled = true;
        }
    }
}
