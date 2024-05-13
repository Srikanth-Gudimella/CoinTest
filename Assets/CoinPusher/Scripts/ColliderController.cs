using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderController : MonoBehaviour
{
    public enum ColliderType
    {
        Hills,LeftBorder,RightBorder,DownBorder
    }
    public ColliderType CurrentColliderType;
}
