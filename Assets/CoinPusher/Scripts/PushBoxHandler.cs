using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoinPusher
{
    public class PushBoxHandler : MonoBehaviour
    {
        public static PushBoxHandler Instance;
        public BoxCollider2D PushBoxPushCollider;
        public List<GameObject> AttachedCoinsList = new List<GameObject>();
        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            Invoke(nameof(MoveDown), 0f);
        }
        public float MoveTime = 1.5f;
        public iTween.EaseType easetype;
        public Transform PlayArea;
        void MoveDown()
        {
            foreach (GameObject obj in AttachedCoinsList)
            {
                obj.transform.SetParent(PlayArea);
                obj.GetComponent<CoinHandler>()._CoinState = GameManager.coinState.ReadyToCollide;
            }
            AttachedCoinsList.Clear();
            PushBoxPushCollider.isTrigger = false;

            iTween.Stop(gameObject);
            iTween.MoveTo(gameObject,iTween.Hash("y", 3.3f, "time", MoveTime, "islocal", true, "easetype", easetype));
            Invoke(nameof(MoveUp), MoveTime+0.1f);
        }
        void MoveUp()
        {
            PushBoxPushCollider.isTrigger = true;

            iTween.Stop(gameObject);
            iTween.MoveTo(gameObject,iTween.Hash("y", 5.5f, "time", MoveTime, "islocal", true, "easetype", easetype));
            Invoke(nameof(MoveDown), MoveTime + 0.1f);
        }
       
    }
}
