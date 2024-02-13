using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoinPusher
{ 
    public class CoinHandler : MonoBehaviour
    {
        public float MoveTime = 1.5f;
        public iTween.EaseType easetype;
        public BoxCollider2D thisCollider;
        public bool IsReadyWithCollisions;
        public GameManager.coinState _CoinState;
        

        private void Start()
        {
            _CoinState = GameManager.coinState.Falling;
            IsReadyWithCollisions = false;
            thisCollider = GetComponent<BoxCollider2D>();
            //Invoke(nameof(MoveDown), 0f);
            StartCoroutine(MoveDown());

        }
        public LayerMask layerMask; // The layer(s) to check against
        public float raycastDistance = 1f; // How far to cast the ray

        private void Update()
        {
            if (_CoinState == GameManager.coinState.FallingFinish)
            {
                // Cast a ray downwards from the object's position
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, raycastDistance, layerMask);
                Debug.DrawLine(transform.position, transform.position + Vector3.down * raycastDistance, Color.red);

                // Check if the ray hits anything
                if (hit.collider != null)
                {
                    Debug.Log("-------- Object is above another collider!");
                    gameObject.transform.SetParent(hit.collider.gameObject.transform);
                    PushBoxHandler.Instance.AttachedCoinsList.Add(gameObject);
                    _CoinState = GameManager.coinState.AttachedToParent;
                    // You can do something here, like changing color, etc.
                }
            }
        }

        IEnumerator MoveDown()
        {
            yield return new WaitForSeconds(0);
            iTween.Stop(gameObject);
            iTween.MoveTo(gameObject, iTween.Hash("y", 0.5f, "time", MoveTime, "islocal", true, "easetype", easetype));
            yield return new WaitForSeconds(MoveTime);
            _CoinState = GameManager.coinState.FallingFinish;

            //IsReadyWithCollisions = true;

            //Invoke(nameof(MoveUp), MoveTime + 0.1f);
        }
        //private void OnTriggerEnter2D(Collider2D collision)
        //{
        //    if (IsReadyWithCollisions && collision.CompareTag("PushBoxParent"))
        //    {
        //        Debug.Log("---------- Attaching coin to parent");
        //        gameObject.transform.SetParent(collision.gameObject.transform);
        //        PushBoxHandler.Instance.AttachedCoinsList.Add(gameObject);
        //    }
        //}
        //private void OnTriggerStay2D(Collider2D collision)
        //{
        //    if (IsReadyWithCollisions && collision.CompareTag("PushBoxParent"))
        //    {
        //        Debug.Log("---------- Attaching coin to parent");
        //        gameObject.transform.SetParent(collision.gameObject.transform);
        //    }
        //}
    }
}
