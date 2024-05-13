using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CoinPusher
{
    public class UIController : MonoBehaviour
    {
        public bool IsMenuClicked = false;
        public bool IsSoundsEnabled = false;
        public bool IsAutoPlayActivated = false;

        public static UIController Instance;
        public GameObject Sounds,SoundsOn, SoundsOff, Exit;
        public GameObject AutoPlayActive, AutoPlayDeActive;
        public Text TotalMoneyTxt,BetMoneyTxt;
        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            IsMenuClicked = false;
            ShowMenuIcons();
            ShowSoundsIcons();
            IsAutoPlayActivated = false;
            CheckAutoPlay();
            SetTotalMoneyTxt();
        }

        public void MenuBtnClick()
        {
            Debug.Log("MenuBtnClick=");

            IsMenuClicked = !IsMenuClicked;
            ShowMenuIcons();
        }
        void ShowMenuIcons()
        {
            Debug.Log("ShowMenuIcons IsMenuClicked="+IsMenuClicked);
            if (IsMenuClicked)
            {
                Sounds.SetActive(true);
                Exit.SetActive(true);
            }
            else
            {
                Sounds.SetActive(false);
                Exit.SetActive(false);
            }
        }
        public void SoundBtnClick()
        {
            IsSoundsEnabled = !IsSoundsEnabled;
            ShowSoundsIcons();
        }
        void ShowSoundsIcons()
        {
            if(IsSoundsEnabled)
            {
                SoundsOn.SetActive(true);
                SoundsOff.SetActive(false);
            }
            else
            {
                SoundsOn.SetActive(false);
                SoundsOff.SetActive(true);
            }
        }
        public void ExitBtnClick()
        {
            Debug.Log("----- Exit Btn Click");
        }
        public void AutoPlayBtnClick()
        {
            IsAutoPlayActivated = !IsAutoPlayActivated;
            CheckAutoPlay();
        }
        void CheckAutoPlay()
        {
            AutoPlayActive.SetActive(false);
            AutoPlayDeActive.SetActive(false);

            if (IsAutoPlayActivated)
            {
                CoinGenerateHandler.Instance.ActivateCoinSpawning();
                AutoPlayDeActive.SetActive(true);
            }
            else
            {
                CoinGenerateHandler.Instance.DisableCoinSpawning();
                AutoPlayActive.SetActive(true);
            }
        }
        public void TotalBetClick()
        {
            TotalBetPopHandler.Instance.Open();
        }
        public void SetTotalMoneyTxt()
        {
            TotalMoneyTxt.text = GameManager.Instance.TotalMoney.ToString("F2");
        }
    }
}
