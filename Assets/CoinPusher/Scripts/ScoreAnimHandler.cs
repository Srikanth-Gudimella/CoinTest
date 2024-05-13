using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CoinPusher
{
    public class ScoreAnimHandler : MonoBehaviour
    {
        public bool IsReady;
        public float CoinValue;

        public void StartAnim(float value)
        {
            Debug.LogError("--------- Start coin Anim");
            CoinValue = value;
            IsReady = false;
            iTween.Stop(gameObject);
            iTween.MoveTo(gameObject, iTween.Hash("x", 0, "y", 6, "time", 1f, "islocal", true, "easetype", iTween.EaseType.linear, "oncomplete", "AnimFinish", "oncompletetarget", this.gameObject));
        }
        void AnimFinish()
        {
            Debug.Log("-------- AnimFinish CoinValue="+ CoinValue);
            IsReady = true;
            gameObject.SetActive(false);
            Debug.Log("-------- AnimFinish TotalMoney="+ GameManager.Instance.TotalMoney);

            GameManager.Instance.TotalMoney += CoinValue * 2;
            UIController.Instance.SetTotalMoneyTxt();
            Debug.Log("-------- AnimFinish TotalMoney222="+ GameManager.Instance.TotalMoney);
            //Destroy(gameObject);
        }
    }
}
