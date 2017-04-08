using UnityEngine;
using System.Collections;

public class Elevator : BaseGimmick {



    private bool upFlag;

    [SerializeField]
    private GameManager gameMng;
 

	// Use this for initialization
	new void Start () {
        base.Start();
        this.upFlag = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (this.upFlag && this.transform.position.y < -4.5)
        {
            this.transform.position += new Vector3(0, 0.1f, 0);
        }

        // 再度こむぶーなどを動かす
        if (this.isActive && this.cache_komuboo != null && this.upFlag == true)
        {
            // 上がるときは上スクロールをしない（背景がズレるため）
            this.cache_camera.GetComponent<Camera>().onGimicScroll = Camera.CameraMode.NORMAL;

            if (this.transform.position.y >= -6)
                this.gameMng.ChangeScrollState(true);
            if (this.transform.position.y >= -4.5)
            {
                if(!this.cache_komuboo.GetComponent<Komuboo>().InvinciblyFlag)
                    this.cache_bgmCtrl.ChangeBGM("normal");
                this.cache_komuboo.GetComponent<Komuboo>().StartWalking();
                this.cache_komuboo.GetComponent<Komuboo>().onUpDownGimic = false;
                this.cache_camera.GetComponent<Camera>().SetViaKomuboo();
                this.isActive = false;

            }
        }
	}

    public void OnTriggerEnter2D(Collider2D in_col)
    {
        if (in_col.tag == "Komuboo" && this.isActive == true)
        {
            this.cache_komuboo = in_col.gameObject;
            in_col.gameObject.GetComponent<Komuboo>().StopWalking();
            in_col.GetComponent<Komuboo>().onUpDownGimic = true;
            this.upFlag = true;
            this.cache_camera.GetComponent<Camera>().onGimicScroll = Camera.CameraMode.GIMMICK;


            if (in_col.GetComponent<Komuboo>().isJumping)
            {
                in_col.gameObject.GetComponent<Animator>().SetTrigger("Walk");
                in_col.GetComponent<Komuboo>().isJumping = false;
            }
        }
    }

    
}
