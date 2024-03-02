using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoinPusher
{
    public class CoinGenerateHandler : MonoBehaviour
    {
        public GameObject CoinPrefab;
        // Start is called before the first frame update
        void Start()
        {
            InvokeRepeating(nameof(GenerateCoin), 1, 3);

           //GenerateCoin();
        }
        void GenerateCoin()
        {
            //Vector3 SpawnPosition = new Vector3(Random.Range(-1.3f, 1.3f), 4.5f, -0.1f);
            //Instantiate(CoinPrefab, SpawnPosition, Quaternion.identity, PushBoxHandler.Instance.PlayArea);
            StartCoroutine(SpawnCoins());
        }
        IEnumerator SpawnCoins()
        {
            int RandomSpawnCount = Random.Range(4, 7);
            float maxgap = 5;
            float eachgap = maxgap / RandomSpawnCount;
            float startPos = 0;// - RandomSpawnCount * 0.5f;
            Debug.Log("------ StartPos=" + startPos + ":::RandomeSpawnCount=" + RandomSpawnCount);
            startPos = 0-RandomSpawnCount * 0.5f * eachgap+eachgap*0.5f;
            for (int i=0;i< RandomSpawnCount; i++)
            {
                yield return new WaitForSeconds(i * 0.2f);
                //Vector3 SpawnPosition = new Vector3(Random.Range(-1.3f, 1.3f), 4.5f, -0.1f);
                //Vector3 SpawnPosition = new Vector3((-1.4f+i*1f), 4.5f, -0.1f);
                //this is left to right distance, -2 to 2
                Vector3 SpawnPosition = new Vector3((startPos+(i*eachgap) + (Random.Range(-0.5f,0.6f))), 4.5f, -0.1f);
                Instantiate(CoinPrefab, SpawnPosition, Quaternion.identity, PushBoxHandler.Instance.PlayArea);
            }
        }
        // Update is called once per frame
        void Update()
        {

        }
    }
}
