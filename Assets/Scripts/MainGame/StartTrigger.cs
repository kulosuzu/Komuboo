using UnityEngine;
using System.Collections;

/// <summary>
/// 落とし穴から落ちたこむぶーが動くためのトリガーオブジェクト
/// </summary>
public class StartTrigger : BaseGimmick {

    [SerializeField]
    private GameManager gameMng;

    public void OnTriggerEnter2D(Collider2D in_col)
    {
        if (in_col.tag == "Komuboo")
        {
            in_col.GetComponent<Komuboo>().StartWalking();
            in_col.GetComponent<Komuboo>().onUpDownGimic = false;    
            this.cache_camera.GetComponent<Camera>().onGimicScroll = Camera.CameraMode.NORMAL;
            this.gameMng.ChangeScrollState(true);
            ChangeActivate(false);
        }
    }
}
