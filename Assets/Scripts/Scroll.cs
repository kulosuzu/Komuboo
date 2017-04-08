using UnityEngine;
using System.Collections;

public class Scroll : MonoBehaviour {

    // スクロールするスピード
    public float speed;

    public Renderer rend;


    // Readyがはじまった時間
    private float startReadyTime;

    // デバッグ制御用
    [SerializeField]
    private DebugControll debugCtrl;

    /// <summary>
    /// 移動制御用
    /// </summary>
    [SerializeField]
    private MoveController moveController;

    private float goalPosition = 305.0f;



    // ストップテスト 
    public bool isMoving;
    private ScrollTimer scrollTimer;


    /// <summary>
    /// テクスチャ(3D)かスプライト(2D)か選択
    /// </summary>
    public bool isUseTexture;


	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
        this.scrollTimer = GameObject.Find("ScrollTimer").GetComponent<ScrollTimer>();

        this.isMoving = true;
	}
	
    void FixedUpdate()
    {
        if (!this.debugCtrl.isDebugging && this.transform.position.x < this.goalPosition)
        {
            if (this.isUseTexture)
            {
                float x = Mathf.Repeat(this.scrollTimer.time * speed, 1);
                // Yの値がずれていくオフセットを作成
                Vector2 offset = new Vector2(x, 0);

                if (this.isMoving)
                {
                    // マテリアルにオフセットを設定する
                    rend.material.SetTextureOffset("_MainTex", offset);
                }
            }

            // 背景移動
            this.transform.position = new Vector3(this.moveController.cache_komubooTrans.position.x + 5.0f, this.transform.position.y, this.transform.position.z);
        }
    }

}
