using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject ScoreAnimObj;
    public List<ScoreAnimHandler> ScoreAnimHandlerList = new List<ScoreAnimHandler>();
    public Transform CoinsParent;
    private void Awake()
    {
        Instance = this;
    }
    public enum coinState
    {
        Falling,
        FallingFinish,
        AttachedToParent,
        DetachFromParent,
        ReadyToCollide,
        Collected
    }
    public enum pushBoxState
    {
        MovingDown,
        Movingup
    }

}
