using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoinPusher
{ 
    public class CoinHandler : MonoBehaviour
    {
        public float MoveTime = 1.5f;
        public iTween.EaseType easetype;
        public CircleCollider2D thisCollider;
        public bool IsReadyWithCollisions;
        public GameManager.coinState _CoinState;

        private void Awake()
        {
            _CoinState = GameManager.coinState.Falling;
            IsReadyWithCollisions = false;
            thisCollider = GetComponent<CircleCollider2D>();
            //Invoke(nameof(MoveDown), 0f);
            StartCoroutine(MoveDown());
        }

        private void Start()
        {
           

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
                    //gameObject.transform.SetParent(hit.collider.gameObject.transform);
                    gameObject.transform.SetParent(PushBoxHandler.Instance.transform);
                   // gameObject.transform.SetParent(PushBoxHandler.Instance.PushBoxParentArea);

                    PushBoxHandler.Instance.AttachedCoinsList.Add(gameObject);
                    //gameObject.transform.position = new Vector3(0, 2.5f, 0);
                    _CoinState = GameManager.coinState.AttachedToParent;
                   // this.gameObject.transform.localPosition = new Vector3(0, 2000.5f, 0);
                    // You can do something here, like changing color, etc.


                }
            }
        }

        IEnumerator MoveDown()
        {
            yield return new WaitForSeconds(0);
            iTween.Stop(gameObject);
            iTween.MoveTo(gameObject, iTween.Hash("y", 0.5f, "time", MoveTime, "islocal", true, "easetype", easetype));
            yield return new WaitForSeconds(MoveTime+0.05f);
            thisCollider.isTrigger = false;
            _CoinState = GameManager.coinState.FallingFinish;

            //IsReadyWithCollisions = true;

            //Invoke(nameof(MoveUp), MoveTime + 0.1f);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
                   
            //if (collision.CompareTag("Hills") && _CoinState == GameManager.coinState.AttachedToParent)
            //{
            //    Debug.Log("----- coin triggered to hills");
            //    _CoinState = GameManager.coinState.DetachFromParent;
            //    PushBoxHandler.Instance.AttachedCoinsList.Remove(this.gameObject);
            //    transform.SetParent(PushBoxHandler.Instance.PlayArea);
            //    _CoinState = GameManager.coinState.ReadyToCollide;
            //    //show coin animation as fall from top of box to down small rotate anim
            //}
            //if (IsReadyWithCollisions && collision.CompareTag("PushBoxParent"))
            //{
            //    Debug.Log("---------- Attaching coin to parent");
            //    gameObject.transform.SetParent(collision.gameObject.transform);
            //    PushBoxHandler.Instance.AttachedCoinsList.Add(gameObject);
            //}
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            //if (IsReadyWithCollisions && collision.CompareTag("PushBoxParent"))
            //{
            //    Debug.Log("---------- Attaching coin to parent");
            //    gameObject.transform.SetParent(collision.gameObject.transform);
            //}
        }
        IEnumerator MoveDownToBoard()
        {
            yield return new WaitForSeconds(0);
            iTween.Stop(gameObject);
            iTween.MoveTo(gameObject, iTween.Hash("y", 0.4f, "time", MoveTime, "islocal", true, "easetype", easetype));
            //yield return new WaitForSeconds(MoveTime + 0.05f);
            //thisCollider.isTrigger = false;
            //_CoinState = GameManager.coinState.FallingFinish;

            //IsReadyWithCollisions = true;

            //Invoke(nameof(MoveUp), MoveTime + 0.1f);
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Hills") && _CoinState == GameManager.coinState.AttachedToParent)
            {
                Debug.Log("----- coin triggered to hills");
                _CoinState = GameManager.coinState.DetachFromParent;
                PushBoxHandler.Instance.AttachedCoinsList.Remove(this.gameObject);
                transform.SetParent(PushBoxHandler.Instance.PlayArea);
                _CoinState = GameManager.coinState.ReadyToCollide;
                StartCoroutine(MoveDownToBoard());
                //show coin animation as fall from top of box to down small rotate anim
            }
            if (PushBoxHandler.Instance.PushBoxState==GameManager.pushBoxState.MovingDown && _CoinState!=GameManager.coinState.ReadyToCollide)
            {
                //show coin animation
                _CoinState = GameManager.coinState.ReadyToCollide;
                //move littlebit down
            }
        }
    }
}
