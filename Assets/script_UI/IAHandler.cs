using UnityEngine;
using static UnityEngine.ParticleSystem;

public class IAHandler : MonoBehaviour
{
    private GameObject playerMouvementHandler;
    private GameObject phaseHandler;
    public Partie partie;
    public GameObject wallPrefab;
    private void Start()
    {
        playerMouvementHandler = GameObject.Find("PlayerMouvementHandler");
        phaseHandler = GameObject.Find("PhaseHandler");
        partie = GameObject.Find("Logic").GetComponent<LogicScript>().partie;
    }

    public void MoveToPlayer(GameObject pawnTarget, Point targetPosition)
    {
        Vector3 target = PlayerMovementHandler.GetCubePositionFromBoard(targetPosition);
        playerMouvementHandler.GetComponent<PlayerMovementHandler>().movePlayerHandler(pawnTarget, target);
        phaseHandler.GetComponent<PhaseHandler>().changePhaseHandler();
    }
    //Create 2 gameobjects phasehandler
    public void PlaceWall(Point targetPosition, bool isHorizontal)
    {
        string wallposition = "GameObject (" + (targetPosition.X * 10 + targetPosition.Y).ToString() + ")";
        GameObject wallPutter = GameObject.Find(wallposition);
        Quaternion rotation = Quaternion.identity;
        if (!isHorizontal)
        {
            rotation = Quaternion.Euler(0, 90, 0);
        }
        Instantiate(wallPrefab, wallPutter.transform.position, rotation);
        partie.placeWall(targetPosition.X, targetPosition.Y, isHorizontal);
        phaseHandler.GetComponent<PhaseHandler>().changePhaseHandler();
    }
}
