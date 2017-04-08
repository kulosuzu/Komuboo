using UnityEngine;
using System.Collections;

public class TramStart : BaseGimmick {

    public GameObject cache_tram;

    // こむぶーのおしりを支えるコライダ
    public BoxCollider2D tramColBack;

    // 山コライダ
    public BoxCollider2D railColBack;


    // 出口コライダ
    public BoxCollider2D UpHallCol;


    public GameManager gameMng;


    public void OnTriggerEnter2D(Collider2D in_col)
    {
        if (in_col.gameObject.tag == "Komuboo")
        {
            base.ChangeActivate(true);

            // ジャンプしていたら強制的にWalkheへ遷移
            if (in_col.GetComponent<Komuboo>().isJumping)
            {
                in_col.gameObject.GetComponent<Animator>().SetTrigger("Walk");
                in_col.GetComponent<Komuboo>().isJumping = false;
                in_col.transform.position = this.cache_tram.transform.position;
            }

            // レールを超えられないようにする
            this.railColBack.isTrigger = false;
            // 出口を抜けられるようにする
            this.UpHallCol.isTrigger = true;

            // カメラ周りの制御
            in_col.GetComponent<Komuboo>().onUpDownGimic = true;
            gameMng.ChangeScrollState(false);
            base.cache_camera.GetComponent<Camera>().onGimicScroll = Camera.CameraMode.GIMMICK;
            this.tramColBack.isTrigger = false;
            
            // こむぶー調整
            in_col.GetComponent<Komuboo>().StopWalking();
            in_col.transform.SetParent(this.cache_tram.transform, true);
            Vector3 current = in_col.transform.position;
            in_col.transform.position = new Vector3(current.x, current.y, current.z);
            in_col.GetComponent<Komuboo>().onUpDownGimic = true;
         
            this.cache_tram.GetComponent<Animator>().SetTrigger("Start");
        }
    }

}
