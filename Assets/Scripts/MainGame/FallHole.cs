using UnityEngine;
using System.Collections;

public class FallHole : BaseGimmick {



    /// <summary>
    /// この板
    /// </summary>
    [SerializeField]
    private BoxCollider2D fallCol;


    private bool downFlag;

    public Sprite damagedSprite;

    // 板が割れる重さ
    public int needValue;

  
	// Use this for initialization
	new void Start () {
        base.Start();
        this.downFlag = false;
	}
	

    public void OnTriggerEnter2D(Collider2D in_col)
    {
        AnimatorStateInfo astateInfo = in_col.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
        bool result = astateInfo.IsName("Base.Komuboo_Sliding");

        if (in_col.tag == "Komuboo" && !result)
        {
            if (in_col.gameObject.GetComponent<Komuboo>().GetKomugiCurrent() > needValue)
            {
                // ジャンプしながら落ちたときは歩くに強制変更
                if (in_col.GetComponent<Komuboo>().isJumping)
                {
                    in_col.gameObject.GetComponent<Animator>().SetTrigger("Walk");
                    in_col.GetComponent<Komuboo>().isJumping = false;
                }

                // 板われる
                this.GetComponent<SpriteRenderer>().sprite = this.damagedSprite;
                this.fallCol.isTrigger = true;

                // 地下BGMに変更
                if (!in_col.gameObject.GetComponent<Komuboo>().InfinityFlag && !in_col.gameObject.GetComponent<Komuboo>().InvinciblyFlag)
                    this.cache_bgmCtrl.ChangeBGM("underground");

                // こむぶーの動きを止める
                this.cache_komuboo = in_col.gameObject;
                in_col.gameObject.GetComponent<Komuboo>().onUpDownGimic = true;
                in_col.gameObject.GetComponent<Komuboo>().StopWalking();
                this.isActive = true;
                this.downFlag = true;
                // 縦スクロール可能にする
                this.cache_camera.GetComponent<Camera>().onGimicScroll = Camera.CameraMode.GIMMICK;

            }
        }
    }
}
