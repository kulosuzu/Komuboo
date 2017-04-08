using UnityEngine;
using System.Collections;

public class MoveController : MonoBehaviour {

    // JumpReady用
    [SerializeField]
    private Komuboo cache_komubooComponent;

    /// <summary>こむぶーの位置 </summary>
    public Transform cache_komubooTrans;


    /// <summary>トロッコの位置 </summary>
    public Transform cache_tramTrans;

}
