using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Blockade;


public class PhaseHandler : MonoBehaviour
{
    public Announcer announcer;

    private int state = 0;
    public GameObject isMyTurnBtnP1;
    public GameObject isMyTurnBtnP2;
    public GameObject hp1;
    public GameObject vp1;
    public GameObject hp2;
    public GameObject vp2;
    public GameObject endturn_btnP1;
    public GameObject endturn_btnP2;
    public GameObject undo_btnP1;
    public GameObject undo_btnP2;
    // Phase Bar
    public GameObject phaseBar1;
    public GameObject phaseBar2;
    public Sprite phaseBarP1;
    public Sprite phaseBarP2;

    public GameObject phaseBarIconPion;
    public GameObject phaseBarIconWall;
    public GameObject phaseBarIconValid;
    public Sprite phaseBarIconPionP1;
    public Sprite phaseBarIconPionP2;
    public Sprite phaseBarIconWallP1;
    public Sprite phaseBarIconWallP2;
    public Sprite phaseBarIconValidP1;
    public Sprite phaseBarIconValidP2;

    public GameObject winPanel;
    public TextMeshProUGUI winText;
    public Point cubeTopLeftPosition;
    public bool isHorizontal;

    public RectTransform winnerPicture1;
    public RectTransform winnerPicture2;
    public RectTransform winnerName1;
    public RectTransform winnerName2;
    public ParticleSystem confetti;

    public AudioSource audioSource; // Référence à l'AudioSource
    public AudioClip winner1Clip; // Référence au clip audio à jouer
    public AudioClip winner2Clip; // Référence au clip audio à jouer
    public AudioClip winnerEffectClip; // Référence au clip audio à jouer
    public AudioClip victoryClip; // Référence au clip audio à jouer

    private bool movementStarted = false;

    void Start()
    {
        //PAWN
        announcer.Message(1, 1);
        PlayerPrefs.SetInt("currentPhase", 0);
        changeWallButtonColor(false, false, false, false);
        isMyTurnBtnP1.SetActive(true);
        isMyTurnBtnP2.SetActive(false);
        phaseBar1.GetComponent<Image>().fillAmount = 0;
        phaseBar2.GetComponent<Image>().fillAmount = 0;
        endturn_btnP1.GetComponent<Button>().interactable = false;
        endturn_btnP2.GetComponent<Button>().interactable = false;
        undo_btnP1.GetComponent<Button>().interactable = false;
        undo_btnP2.GetComponent<Button>().interactable = false;
        winPanel.SetActive(false);
    }
    public void changePhaseHandler()
    {
        IHMLink partie = GameObject.Find("Logic").GetComponent<LogicScript>().partie;
        PlayerMovementHandler playerMovementHandler = GameObject.Find("PlayerMovementHandler").GetComponent<PlayerMovementHandler>();
        endturn_btnP1.GetComponent<Button>().interactable = false;
        endturn_btnP2.GetComponent<Button>().interactable = false;
        undo_btnP1.GetComponent<Button>().interactable = false;
        undo_btnP2.GetComponent<Button>().interactable = false;
        state++;
        ChangeColor(state);
        PlayerPrefs.SetInt("currentPhase", state);
        if (state == 1)
        {
            //WALL
            partie.updatePawnPosition(playerMovementHandler.previousPosition.X, playerMovementHandler.previousPosition.Y, playerMovementHandler.currentPlayer.GetComponent<PlayerPositionHandler>().initialPosition.X, playerMovementHandler.currentPlayer.GetComponent<PlayerPositionHandler>().initialPosition.Y);
            PlayerPrefs.SetInt("clickCounter", 0);
            if (PlayerPrefs.GetInt("currentPlayer") == 1)
            {
                announcer.Message(2, 1);
                changeWallButtonColor(true, true, false, false);
            }
            else
            {
                announcer.Message(2, 2);
                changeWallButtonColor(false, false, true, true);
            }

        }
        if (state == 2)
        {
            partie.placeWall(cubeTopLeftPosition.X, cubeTopLeftPosition.Y, isHorizontal);
            //changeWallButtonColor(false, false, false, false);
            StartCoroutine(DelayResetState());
        }
        // This is a placeholder for the function that will change the phase of the game
    }
    IEnumerator DelayResetState()
    {
        yield return new WaitForSeconds(1); // waits 1 seconds
        state = 0;
        //PAWN
        announcer.Message(1, PlayerPrefs.GetInt("currentPlayer"));
        ChangeColor(state);
        PlayerPrefs.SetInt("currentPhase", state);
        if (PlayerPrefs.GetInt("currentPlayer") == 1)
        {
            announcer.Message(1, 2);
            PlayerPrefs.SetInt("currentPlayer", 2);
            isMyTurnBtnP1.SetActive(false);
            isMyTurnBtnP2.SetActive(true);
            phaseBar1.GetComponent<Image>().sprite = phaseBarP2;
            phaseBar2.GetComponent<Image>().sprite = phaseBarP2;
            phaseBarIconPion.GetComponent<Image>().sprite = phaseBarIconPionP2;
            phaseBarIconWall.GetComponent<Image>().sprite = phaseBarIconWallP2;
            phaseBarIconValid.GetComponent<Image>().sprite = phaseBarIconValidP2;
        }
        else
        {
            announcer.Message(1, 1);
            PlayerPrefs.SetInt("currentPlayer", 1);
            isMyTurnBtnP1.SetActive(true);
            isMyTurnBtnP2.SetActive(false);
            phaseBar1.GetComponent<Image>().sprite = phaseBarP1;
            phaseBar2.GetComponent<Image>().sprite = phaseBarP1;
            phaseBarIconPion.GetComponent<Image>().sprite = phaseBarIconPionP1;
            phaseBarIconWall.GetComponent<Image>().sprite = phaseBarIconWallP1;
            phaseBarIconValid.GetComponent<Image>().sprite = phaseBarIconValidP1;
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

    public void changeWallButtonColor(bool hp1b, bool vp1b, bool hp2b, bool vp2b)
    {
        if (vp1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text == "0")
        {
            vp1b = false;
        }
        if (hp1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text == "0")
        {
            hp1b = false;
        }
        if (vp2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text == "0")
        {
            vp2b = false;
        }
        if (hp2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text == "0")
        {
            hp2b = false;
        }
        hp1.GetComponent<Button>().interactable = hp1b;
        vp1.GetComponent<Button>().interactable = vp1b;
        hp2.GetComponent<Button>().interactable = hp2b;
        vp2.GetComponent<Button>().interactable = vp2b;
        Color colorH1 = hp1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color;
        Color colorV1 = vp1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color;
        Color colorH2 = hp1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color;
        Color colorV2 = vp1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color;
        colorH1.a = hp1b ? 1f : 0.5f;
        colorV1.a = vp1b ? 1f : 0.5f;
        colorH2.a = hp2b ? 1f : 0.5f;
        colorV2.a = vp2b ? 1f : 0.5f;
        hp1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = colorH1;
        vp1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = colorV1;
        hp2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = colorH2;
        vp2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = colorV2;
    }

    public void victore(PlayerID playerID)
    {
        if (playerID == PlayerID.Player1)
        {
            if (!movementStarted)
            {
                StartCoroutine(MoveElements1());
                movementStarted = true;
            }
            string p1Name = PlayerPrefs.GetString("PlayerName1");
            winText.text = "Player " + p1Name + " a gagné";
        }
        else
        {
            if (!movementStarted)
            {
                StartCoroutine(MoveElements2());
                movementStarted = true;
            }
            string p2Name = PlayerPrefs.GetString("PlayerName2");
            winText.text = "Player " + p2Name + " a gagné";
        }
        StartCoroutine(ShowWinPanelWithDelay());
    }

    private IEnumerator ShowWinPanelWithDelay()
    {
        yield return new WaitForSeconds(5f); // Attendre 3 secondes
        winPanel.SetActive(true);
    }
    IEnumerator MoveElements1()
    {
        
        Vector2 winnerPicture1Position = new Vector2(0f, 90f);
        Vector2 winnerName1Position = new Vector2(0f, -325f);
        float duration = 5f; // Durée du déplacement en secondes

        // Jouer le son
        audioSource.clip = winner1Clip;
        audioSource.Play();

        // Jouer le second son en utilisant PlayOneShot
        audioSource.PlayOneShot(winnerEffectClip);

        // Jouer le second son en utilisant PlayOneShot
        audioSource.PlayOneShot(victoryClip);

        confetti.Play(); // Démarrer le système de particules

        float timer = 0f;
        while (timer < duration)
        {
            float t = timer / duration;
            winnerPicture1.anchoredPosition = Vector2.Lerp(winnerPicture1.anchoredPosition, winnerPicture1Position, t);
            winnerName1.anchoredPosition = Vector2.Lerp(winnerName1.anchoredPosition, winnerName1Position, t);
            timer += Time.deltaTime;
            yield return null;
        }

        // Correction des positions pour s'assurer qu'ils soient exactement à la position finale
        winnerPicture1.anchoredPosition = winnerPicture1Position;
        winnerName1.anchoredPosition = winnerName1Position;

        // Cacher les RectTransforms à la fin de la coroutine
        winnerPicture1.gameObject.SetActive(false);
        winnerName1.gameObject.SetActive(false);
        confetti.gameObject.SetActive(false);
    }

    IEnumerator MoveElements2()
    {
        
        Vector2 winnerPicture2Position = new Vector2(0f, 90f);
        Vector2 winnerName2Position = new Vector2(0f, -325f);
        float duration = 5f; // Durée du déplacement en secondes

        // Jouer le son
        audioSource.clip = winner2Clip;
        audioSource.Play();

        // Jouer le second son en utilisant PlayOneShot
        audioSource.PlayOneShot(winnerEffectClip);

        // Jouer le second son en utilisant PlayOneShot
        audioSource.PlayOneShot(victoryClip);

        confetti.Play(); // Démarrer le système de particules

        float timer = 0f;
        while (timer < duration)
        {
            float t = timer / duration;
            winnerPicture2.anchoredPosition = Vector2.Lerp(winnerPicture2.anchoredPosition, winnerPicture2Position, t);
            winnerName2.anchoredPosition = Vector2.Lerp(winnerName2.anchoredPosition, winnerName2Position, t);
            timer += Time.deltaTime;
            yield return null;
        }

        // Correction des positions pour s'assurer qu'ils soient exactement à la position finale
        winnerPicture2.anchoredPosition = winnerPicture2Position;
        winnerName2.anchoredPosition = winnerName2Position;

        // Cacher les RectTransforms à la fin de la coroutine
        winnerPicture2.gameObject.SetActive(false);
        winnerName2.gameObject.SetActive(false);
    }

}
