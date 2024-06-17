using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Blockade;


public class PhaseHandlerBOT : MonoBehaviour
{
    public Announcer announcer;
    public GameObject wallPrefab;

    private int state = 0;
    public GameObject isMyTurnBtnP1;
    public GameObject isMyTurnBtnP2;
    public GameObject hp1;
    public GameObject vp1;
    public GameObject hp2;
    public GameObject vp2;
    public GameObject endturn_btnP1;
    public GameObject undo_btnP1;
    // Phase Bar
    public GameObject phaseBarr1;
    public GameObject phaseBarr2;
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
    public IHMLink partie;


    void Start()
    {
        //PAWN
        announcer.Message(1, 1);
        PlayerPrefs.SetInt("currentPhase", 0);
        changeWallButtonColor(false, false, false, false);
        isMyTurnBtnP1.SetActive(true);
        isMyTurnBtnP2.SetActive(false);
        phaseBarr1.GetComponent<Image>().fillAmount = 0;
        phaseBarr2.GetComponent<Image>().fillAmount = 0;
        endturn_btnP1.GetComponent<Button>().interactable = false;
        undo_btnP1.GetComponent<Button>().interactable = false;
        winPanel.SetActive(false);
    }
    public void changePhaseHandler()
    {
        partie = GameObject.Find("Logic").GetComponent<LogicScript>().partie;
        PlayerMovementHandler playerMovementHandler = GameObject.Find("PlayerMovementHandler").GetComponent<PlayerMovementHandler>();
        endturn_btnP1.GetComponent<Button>().interactable = false;
        undo_btnP1.GetComponent<Button>().interactable = false;
        state++;
        Debug.Log("State: " + state);
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
            phaseBarr1.GetComponent<Image>().sprite = phaseBarP2;
            phaseBarr2.GetComponent<Image>().sprite = phaseBarP2;
            phaseBarIconPion.GetComponent<Image>().sprite = phaseBarIconPionP2;
            phaseBarIconWall.GetComponent<Image>().sprite = phaseBarIconWallP2;
            phaseBarIconValid.GetComponent<Image>().sprite = phaseBarIconValidP2;
            //BOT Pawn
            (bool, Sommet) pionDeplacement = partie.game.IAFacile.deplacerPion(partie.game, partie.game.Graphe);
            Point p = new Point(pionDeplacement.Item2.X, pionDeplacement.Item2.Y);
            MoveToPlayer(pionDeplacement.Item1, p);  
            yield return new WaitForSeconds(1);
            ChangeColor(1);
            //BOT Wall
            (bool, Sommet) murPlacement = partie.game.IAFacile.poserMur(partie.game);
            Point p2 = new Point(murPlacement.Item2.X, murPlacement.Item2.Y);
            PlaceWall(p2, murPlacement.Item1);
            yield return new WaitForSeconds(1);
            ChangeColor(2);
            yield return new WaitForSeconds(1);
            ChangeColor(0);
            announcer.Message(1, 1);
            PlayerPrefs.SetInt("currentPlayer", 1);
            phaseBarr1.GetComponent<Image>().sprite = phaseBarP1;
            phaseBarr2.GetComponent<Image>().sprite = phaseBarP1;
            phaseBarIconPion.GetComponent<Image>().sprite = phaseBarIconPionP1;
            phaseBarIconWall.GetComponent<Image>().sprite = phaseBarIconWallP1;
            phaseBarIconValid.GetComponent<Image>().sprite = phaseBarIconValidP1;
            isMyTurnBtnP1.SetActive(true);
            isMyTurnBtnP2.SetActive(false);
        }
    }
    public void ChangeColor(int state)
    {
        switch (state)
        {
            case 0:
                phaseBarr1.GetComponent<Image>().fillAmount = 0;
                phaseBarr2.GetComponent<Image>().fillAmount = 0;
                break;
            case 1:
                StartCoroutine(FillImage(phaseBarr1.GetComponent<Image>()));
                break;
            case 2:
                StartCoroutine(FillImage(phaseBarr2.GetComponent<Image>()));
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

    public void victore()
    {

    }

    public void MoveToPlayer(bool isUp, Point targetPosition)
    {
        GameObject pawnTarget = null;
        foreach (var item in GameObject.FindGameObjectsWithTag("Pions"))
        {
            if (item.GetComponent<PlayerPositionHandler>().playerID == PlayerID.Player2 && item.GetComponent<PlayerPositionHandler>().isUp == isUp)
            {
                pawnTarget = item;
            }
        }
        IHMLink partie = GameObject.Find("Logic").GetComponent<LogicScript>().partie;
        PlayerMovementHandler playerMouvementHandler = GameObject.Find("PlayerMovementHandler").GetComponent<PlayerMovementHandler>();
        Vector3 target = PlayerMovementHandler.GetCubePositionFromBoard(targetPosition);
        playerMouvementHandler.GetComponent<PlayerMovementHandler>().movePlayerHandler(pawnTarget, target);
        partie.updatePawnPosition(pawnTarget.GetComponent<PlayerPositionHandler>().initialPosition.X, pawnTarget.GetComponent<PlayerPositionHandler>().initialPosition.Y, targetPosition.X, targetPosition.Y);
        pawnTarget.GetComponent<PlayerPositionHandler>().initialPosition = targetPosition;
    }
    public void PlaceWall(Point targetPosition, bool isHorizontal)
    {
        IHMLink partie = GameObject.Find("Logic").GetComponent<LogicScript>().partie;
        string wallposition = "GameObject (" + (targetPosition.X * 10 + targetPosition.Y).ToString() + ")";
        GameObject wallPutter = GameObject.Find(wallposition);
        Quaternion rotation = Quaternion.identity;
        if (!isHorizontal)
        {
            rotation = Quaternion.Euler(0, 90, 0);
        }
        Instantiate(wallPrefab, wallPutter.transform.position, rotation);
        partie.placeWall(targetPosition.X, targetPosition.Y, isHorizontal);
    }

    public void victore(PlayerID playerID)
    {
        if (playerID == PlayerID.Player1)
        {
            string p1Name = PlayerPrefs.GetString("PlayerName1");
            winText.text = "Player " + p1Name + " a gagné";
        }
        else
        {
            string p2Name = PlayerPrefs.GetString("PlayerName2");
            winText.text = "Player " + p2Name + " a gagné";
        }
        winPanel.SetActive(true);
    }
}
