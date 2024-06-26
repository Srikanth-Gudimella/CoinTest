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
        public GameManager.pushBoxState PushBoxState;
        public Transform PushBoxParentArea;
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
            //foreach (GameObject obj in AttachedCoinsList)
            //{
            //    obj.transform.SetParent(PlayArea);
            //    obj.GetComponent<CoinHandler>()._CoinState = GameManager.coinState.ReadyToCollide;
            //}
            //AttachedCoinsList.Clear();
            PushBoxState = GameManager.pushBoxState.MovingDown;
            PushBoxPushCollider.isTrigger = false;
            RemoveAllAttachedCoins();
            iTween.Stop(gameObject);
            iTween.MoveTo(gameObject,iTween.Hash("y", 3.3f, "time", MoveTime, "islocal", true, "easetype", easetype));
            Invoke(nameof(MoveUp), MoveTime+0.1f);
        }
       
        void MoveUp()
        {
            PushBoxState = GameManager.pushBoxState.Movingup;

            PushBoxPushCollider.isTrigger = true;

            iTween.Stop(gameObject);
            iTween.MoveTo(gameObject,iTween.Hash("y", 5.5f, "time", MoveTime, "islocal", true, "easetype", easetype));
            Invoke(nameof(MoveDown), MoveTime + 0.1f);
        }
        void RemoveAllAttachedCoins()
        {
            for(int i= AttachedCoinsList.Count-1; i>=0;i--)
            {
                CoinHandler _coinHandler = AttachedCoinsList[i].GetComponent<CoinHandler>();
                AttachedCoinsList.Remove(AttachedCoinsList[i]);
                _coinHandler.transform.SetParent(GameManager.Instance.CoinsParent);
                _coinHandler._CoinState = GameManager.coinState.ReadyToCollide;
            }
        }

    }
}
