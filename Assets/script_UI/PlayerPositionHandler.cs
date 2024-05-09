using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerID
{
    Player1, Player2
}

public class PlayerPositionHandler : MonoBehaviour
{
    public Point initialPosition;
    public PlayerID playerID;
}
