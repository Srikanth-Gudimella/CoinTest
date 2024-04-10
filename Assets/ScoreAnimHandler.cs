using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreAnimHandler : MonoBehaviour
{
    public bool IsReady;
    
    public void StartAnim()
    {
        Debug.LogError("--------- Start coin Anim");
        IsReady = false;
        iTween.Stop(gameObject);
        iTween.MoveTo(gameObject, iTween.Hash("x", 0, "y", 6, "time", 1f, "islocal", true, "easetype", iTween.EaseType.linear, "oncomplete", "AnimFinish", "oncompletetarget", this.gameObject));

    }
    void AnimFinish()
    {
        Debug.Log("-------- AnimFinish");
        IsReady = true;
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }
}
