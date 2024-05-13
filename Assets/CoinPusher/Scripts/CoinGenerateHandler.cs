using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoinPusher
{
    public class CoinGenerateHandler : MonoBehaviour
    {
        public static CoinGenerateHandler Instance;
        public GameObject CoinPrefab;
        public GameObject LotusPrefab;
        public GameObject DutaraPrefab,AxePrefab;

        public float GenerateSpeed = 3;
        public enum PieceType
        {
            Coin,Lotus,Dutara,Axe
        }
        public PieceType NextPieceType;
        private void Awake()
        {
            Instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {

           //GenerateCoin();
        }
        public void ActivateCoinSpawning()
        {
            InvokeRepeating(nameof(GenerateCoin), 1, GenerateSpeed);
        }
        public void DisableCoinSpawning()
        {
            CancelInvoke(nameof(GenerateCoin));
        }
        void GenerateCoin()
        {
            //Vector3 SpawnPosition = new Vector3(Random.Range(-1.3f, 1.3f), 4.5f, -0.1f);
            //Instantiate(CoinPrefab, SpawnPosition, Quaternion.identity, PushBoxHandler.Instance.PlayArea);
            StartCoroutine(SpawnCoins());
        }

        GameObject SpawnObj;
        IEnumerator SpawnCoins()
        {
            //if(GameManager.Instance.TotalMoney < GameManager.Instance.CurrentBetMoney)
            //{
            //    Debug.LogError("------- Dont have sufficient money");
            //    yield break;
            //}
            int RandomSpawnCount = Random.Range(4, 7);
            float maxgap = 5;
            float eachgap = maxgap / RandomSpawnCount;
            float startPos = 0;// - RandomSpawnCount * 0.5f;
            //Debug.Log("------ StartPos=" + startPos + ":::RandomeSpawnCount=" + RandomSpawnCount);
            startPos = 0-RandomSpawnCount * 0.5f * eachgap+eachgap*0.5f;
            for (int i=0;i< RandomSpawnCount; i++)
            {
                yield return new WaitForSeconds(i * 0.2f);
                if (GameManager.Instance.TotalMoney < GameManager.Instance.CurrentBetMoney)
                {
                    Debug.LogError("------- Dont have sufficient money");
                    break;
                }
                GameManager.Instance.TotalMoney -= GameManager.Instance.CurrentBetMoney;
                UIController.Instance.SetTotalMoneyTxt();
                //Vector3 SpawnPosition = new Vector3(Random.Range(-1.3f, 1.3f), 4.5f, -0.1f);
                //Vector3 SpawnPosition = new Vector3((-1.4f+i*1f), 4.5f, -0.1f);
                //this is left to right distance, -2 to 2
                Vector3 SpawnPosition = new Vector3((startPos+(i*eachgap) + (Random.Range(-0.5f,0.6f))), 4.5f, -0.1f);
                switch(NextPieceType)
                {
                    case PieceType.Coin:
                        SpawnObj=Instantiate(CoinPrefab, SpawnPosition, Quaternion.identity, GameManager.Instance.CoinsParent);
                        break;
                    case PieceType.Lotus:
                        SpawnObj= Instantiate(LotusPrefab, SpawnPosition, Quaternion.identity, GameManager.Instance.CoinsParent);
                        break;
                    case PieceType.Dutara:
                        SpawnObj=Instantiate(DutaraPrefab, SpawnPosition, Quaternion.identity, GameManager.Instance.CoinsParent);
                        break;
                    case PieceType.Axe:
                        SpawnObj=Instantiate(AxePrefab, SpawnPosition, Quaternion.identity, GameManager.Instance.CoinsParent);
                        break;
                }
                SpawnObj.GetComponent<CoinHandler>().CoinValue = GameManager.Instance.CurrentBetMoney;
                NextPieceType = PieceType.Coin;
            }
        }
        // Update is called once per frame
        void Update()
        {

        }
    }
}
