using Spine.Unity;
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
        public Animator CoinFallAnimator;
        public SkeletonAnimation coinSkeleton;
        public bool IsSplPiece=false;

        private void Awake()
        {
            if (IsSplPiece)
            {
                _CoinState = GameManager.coinState.ReadyToCollide;
                IsReadyWithCollisions = true;
                thisCollider = GetComponent<CircleCollider2D>();
                thisCollider.isTrigger = false;
            }
            else
            {
                _CoinState = GameManager.coinState.Falling;
                IsReadyWithCollisions = false;
                thisCollider = GetComponent<CircleCollider2D>();
                //Invoke(nameof(MoveDown), 0f);
                StartCoroutine(MoveDown());
            }
        }

        private void Start()
        {

            //coinSkeleton.loop = true;

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
            //coinSkeleton.loop = false;
            yield return new WaitForSeconds(MoveTime+0.05f);
            _CoinState = GameManager.coinState.FallingFinish;
            thisCollider.isTrigger = false;

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
                transform.SetParent(GameManager.Instance.CoinsParent);
                _CoinState = GameManager.coinState.ReadyToCollide;
                StartCoroutine(MoveDownToBoard());
                //show coin animation as fall from top of box to down small rotate anim
            }
            else if (collision.gameObject.CompareTag("Border"))
            {
                Debug.Log("----- coin triggered to Border");
                //Time.timeScale = 0;
                thisCollider.isTrigger = true;
               

                _CoinState = GameManager.coinState.Collected;

                if (PushBoxHandler.Instance.AttachedCoinsList.Contains(this.gameObject))
                {
                    PushBoxHandler.Instance.AttachedCoinsList.Remove(this.gameObject);
                    transform.SetParent(GameManager.Instance.CoinsParent);
                }

               
                StartCoroutine(ShowCoinFalling("FallSide",0));
                //show coin animation as fall from top of box to down small rotate anim
            }
            else if (collision.gameObject.CompareTag("RightBorder"))
            {
                Debug.Log("----- coin triggered to Right Border");
                //Time.timeScale = 0;
                thisCollider.isTrigger = true;


                _CoinState = GameManager.coinState.Collected;

                if (PushBoxHandler.Instance.AttachedCoinsList.Contains(this.gameObject))
                {
                    PushBoxHandler.Instance.AttachedCoinsList.Remove(this.gameObject);
                    transform.SetParent(GameManager.Instance.CoinsParent);
                }


                StartCoroutine(ShowCoinFalling("FallSide",1));
                //show coin animation as fall from top of box to down small rotate anim
            }
            else if (collision.gameObject.CompareTag("DownBorder"))
            {
                Debug.Log("----- coin triggered to Down Border");
                //Time.timeScale = 0;
                thisCollider.isTrigger = true;


                _CoinState = GameManager.coinState.Collected;

                if (PushBoxHandler.Instance.AttachedCoinsList.Contains(this.gameObject))
                {
                    PushBoxHandler.Instance.AttachedCoinsList.Remove(this.gameObject);
                    transform.SetParent(GameManager.Instance.CoinsParent);
                }


                StartCoroutine(ShowCoinFalling("FallDown",2));
                //show coin animation as fall from top of box to down small rotate anim
            }
            if (PushBoxHandler.Instance.PushBoxState==GameManager.pushBoxState.MovingDown && _CoinState!=GameManager.coinState.ReadyToCollide)
            {
                //show coin animation
                _CoinState = GameManager.coinState.ReadyToCollide;
                //move littlebit down
            }

        }
        IEnumerator ShowCoinFalling(string animTriggerValue,int BorderType)
        {
            yield return new WaitForSeconds(0.1f);
            switch(BorderType)
            {
                case 0://Left
                    iTween.Stop(gameObject);
                    iTween.MoveTo(gameObject, iTween.Hash("x", gameObject.transform.position.x-0.5f, "y", gameObject.transform.position.y - 0.5f, "time", 1f, "islocal", true, "easetype", easetype));
                    break;
                case 1://Right
                    iTween.Stop(gameObject);
                    iTween.MoveTo(gameObject, iTween.Hash("x", gameObject.transform.position.x + 0.6f, "y", gameObject.transform.position.y - 0.5f, "time", 1f, "islocal", true, "easetype", easetype));
                    break;
                case 2://Down
                    iTween.Stop(gameObject);
                    iTween.MoveTo(gameObject, iTween.Hash("y", gameObject.transform.position.y - 1.4f, "time", 1f, "islocal", true, "easetype", easetype));
                    break;
            }

            //ShowFalling Animation
            //iTween.ScaleTo(this.gameObject, iTween.Hash("y", 0f, "time", 0.7f, "islocal", true, "easetype", iTween.EaseType.linear));

            //iTween.ScaleTo(this.gameObject, iTween.Hash("x", 0.2f, "time", 0.3f, "islocal", true, "easetype", iTween.EaseType.linear));
            //yield return new WaitForSeconds(0.3f);
            //iTween.ScaleTo(this.gameObject, iTween.Hash("x", 0.2f, "time", 0.3f, "islocal", true, "easetype", iTween.EaseType.linear));
            CoinFallAnimator.enabled = true;
            CoinFallAnimator.SetTrigger(animTriggerValue);
            yield return new WaitForSeconds(1.2f);

            //Vector3 SpawnPosition = new Vector3((startPos + (i * eachgap) + (Random.Range(-0.5f, 0.6f))), 4.5f, -0.1f);
            if (BorderType == 2)
                SpawnScoreAnim();
            Destroy(gameObject);

            //Instantiate score anim effect
        }

        void SpawnScoreAnim()
        {
            bool IsAviableScoreAnimObj=false;
            for(int i=0;i<GameManager.Instance.ScoreAnimHandlerList.Count;i++)
            {
                if(GameManager.Instance.ScoreAnimHandlerList[i].IsReady)
                {
                    GameManager.Instance.ScoreAnimHandlerList[i].transform.position = transform.position;
                    GameManager.Instance.ScoreAnimHandlerList[i].transform.SetParent(PushBoxHandler.Instance.PlayArea);
                    GameManager.Instance.ScoreAnimHandlerList[i].gameObject.SetActive(true);
                    GameManager.Instance.ScoreAnimHandlerList[i].StartAnim();
                    IsAviableScoreAnimObj = true;
                    break;
                }
            }
            if(!IsAviableScoreAnimObj)
            {
                GameObject obj= Instantiate(GameManager.Instance.ScoreAnimObj, transform.position, Quaternion.identity, PushBoxHandler.Instance.PlayArea);
                ScoreAnimHandler _ScoreAnimHandler = obj.GetComponent<ScoreAnimHandler>();
                GameManager.Instance.ScoreAnimHandlerList.Add(_ScoreAnimHandler);
                //_ScoreAnimHandler.IsReady = false;
                _ScoreAnimHandler.StartAnim();
                Debug.LogError("--------- Instantiating coin score anim");

            }
        }
    }
}
