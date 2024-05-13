using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoinPusher
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public GameObject ScoreAnimObj;
        public List<ScoreAnimHandler> ScoreAnimHandlerList = new List<ScoreAnimHandler>();
        public Transform CoinsParent;
        public float TotalMoney = 100.0f;
        public float CurrentBetMoney;

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
}
