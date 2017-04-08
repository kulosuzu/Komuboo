using UnityEngine;
using System.Collections;

public class BaseGimmick : MonoBehaviour {

    /// <summary>
    /// このギミックが有効かどうか
    /// </summary>
    protected bool isActive;

    protected GameObject cache_komuboo;

    /// <summary>
    /// スクロール挙動変更のため
    /// </summary>
    protected GameObject cache_camera;


    /// <summary>
    /// BGM変更のため
    /// </summary>
    protected BGMController cache_bgmCtrl;


	// Use this for initialization
	protected void Start () {
        this.isActive = false;
        this.cache_bgmCtrl = GameObject.FindWithTag("BGMCtrl").GetComponent<BGMController>();
        this.cache_camera = GameObject.FindWithTag("MainCamera");
	}


    public void ChangeActivate(bool in_parameter)
    {
        this.isActive = in_parameter;
    }

    public GameObject GetCacheKomuboo()
    {
        return this.cache_komuboo;
    }

    public GameObject GetCacheCamera()
    {
        return this.cache_camera;
    }
}
