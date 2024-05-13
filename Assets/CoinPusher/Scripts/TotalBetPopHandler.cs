using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CoinPusher
{
    public class TotalBetPopHandler : MonoBehaviour
    {
        public static TotalBetPopHandler Instance;
        public GameObject MainObj;
        public float[] BetMoney;
        private void Awake()
        {
            Instance = this;
            MainObj.SetActive(false);
        }
        private void Start()
        {
            GameManager.Instance.CurrentBetMoney = BetMoney[0];
            UIController.Instance.BetMoneyTxt.text = GameManager.Instance.CurrentBetMoney.ToString("F2");
        }
        public void Open()
        {
            MainObj.SetActive(true);
        }
        public void Close()
        {
            MainObj.SetActive(false);
        }
        public void BetBtnClick(int Index)
        {
            GameManager.Instance.CurrentBetMoney = BetMoney[Index];
            UIController.Instance.BetMoneyTxt.text = GameManager.Instance.CurrentBetMoney.ToString("F2");
        }
    }
}
