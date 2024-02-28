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
            float startPos = 0 - RandomSpawnCount * 0.5f;
            Debug.Log("------ StartPos="+startPos+":::RandomeSpawnCount="+RandomSpawnCount);
            for (int i=0;i< RandomSpawnCount; i++)
            {
                yield return new WaitForSeconds(i * 0.2f);
                //Vector3 SpawnPosition = new Vector3(Random.Range(-1.3f, 1.3f), 4.5f, -0.1f);
                //Vector3 SpawnPosition = new Vector3((-1.4f+i*1f), 4.5f, -0.1f);
                //this is left to right distance, -2 to 2
                Vector3 SpawnPosition = new Vector3((startPos+(i*(4/ RandomSpawnCount)) + (Random.Range(-0.5f,0.5f))), 4.5f, -0.1f);
                Instantiate(CoinPrefab, SpawnPosition, Quaternion.identity, PushBoxHandler.Instance.PlayArea);
            }
        }
        // Update is called once per frame
        void Update()
        {

        }
    }
}
