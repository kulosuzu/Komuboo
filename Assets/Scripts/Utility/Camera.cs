using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {


    // デバッグ制御用
    [SerializeField]
    private DebugControll debugCtrl;

    /// <summary>
    /// 移動制御用
    /// </summary>
    [SerializeField]
    private MoveController moveController;


    private float goalPosition = 305.0f;


    public enum CameraMode
    {
        NORMAL = 0,
        GIMMICK,
        TRAM,
    };

    /// <summary>
    /// 落とし穴とエレベーターに乗っている時のみカメラのｙをこむぶーのｙ値にするため
    /// </summary>
    public CameraMode onGimicScroll;


	// Use this for initialization
	void Start () {
        this.onGimicScroll = CameraMode.NORMAL;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        if (!this.debugCtrl.isDebugging && /*!this.moveController.GetJumpReady() &&*/ this.transform.position.x < this.goalPosition)
        {
            // カメラ移動
            if (this.onGimicScroll == CameraMode.NORMAL) // 横だけ移動
                this.transform.position = new Vector3(this.moveController.cache_komubooTrans.position.x + 5.0f, this.transform.position.y, this.transform.position.z);
            else if (this.onGimicScroll == CameraMode.GIMMICK) // 縦も移動
            {
                if (this.moveController.cache_komubooTrans.position.y < -3.25)
                    this.transform.position = new Vector3(this.moveController.cache_komubooTrans.position.x + 5.0f, this.moveController.cache_komubooTrans.position.y + 3.2f, this.transform.position.z);
                else this.onGimicScroll = CameraMode.NORMAL; // 上に出過ぎだら縦移動なしにする（トロッコ上がったとこ）
            }
            else if (this.onGimicScroll == CameraMode.TRAM)
                this.transform.position = new Vector3(this.moveController.cache_tramTrans.position.x + 5.0f, this.moveController.cache_tramTrans.position.y + 3.2f, this.transform.position.z);
        }
    }

    public void SetViaKomuboo()
    {
        this.transform.position = new Vector3(this.moveController.cache_komubooTrans.position.x + 5.0f, this.moveController.cache_komubooTrans.position.y + 3.2f, this.transform.position.z);
    }


}
