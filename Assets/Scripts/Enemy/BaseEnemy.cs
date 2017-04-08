using UnityEngine;
using System.Collections;


public class BaseEnemy : MonoBehaviour
{

    /// <summary> 食べた小麦 </summary>
    protected int ateValue;

    /// <summary> 満腹な値 </summary>
    protected int FULL_KOMUGI;

    /// <summary> こむぶー衝突時に食べる小麦</summary>
    public int stoleValue { protected set; get; }


    /// <summary>Update関数用　引き返しフラグ</summary>
    protected bool isGoHome;
    public bool canGoHome; // モグラが左に動くのはマズイため

    /// <summary>引き返し始めたときの位置　消す判断材料として保持 </summary>
    protected Vector3 startGoBackPosition;

    // キャッシュ
    private Rigidbody2D baseRigidBody;

    /// <summary>移動制御用</summary>
    protected MoveController moveController;

    protected float moveSpeed_x;
    protected float moveSpeed_y;

    // Destroy特殊処理のため
    private EnemyController enemyCtrl;


    /// <summary>
    /// 動きありかナシか
    /// </summary>
    public bool isActive;

    // Use this for initialization
    protected void Start()
    {
        InitValue();
        this.moveController = GameObject.Find("MoveController").GetComponent<MoveController>();
        this.enemyCtrl = GameObject.Find("EnemyController").GetComponent<EnemyController>();
    }

    public void InitValue()
    {
        this.ateValue = 0;
        this.moveSpeed_x = -1.0f;
        this.isGoHome = false;
        this.canGoHome = true;

        // キャッシュ取得
        this.baseRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public void FixedUpdateProcess()
    {
        if (this.isGoHome && this.gameObject != null)
        {
            GoBackHome();

        }

        // 移動
        this.baseRigidBody.velocity = new Vector2(moveSpeed_x, this.baseRigidBody.velocity.y);
    }


    public void OnCollisionEnter2D(Collision2D in_col)
    {
        // こむぶーはこっちで反応する
        Debug.Log("こむぶーにあたった！");



    }


    public void OnTriggerEnter2D(Collider2D in_col)
    {
        Debug.Log(in_col.gameObject.name);
        if (in_col.gameObject.tag == "Komugi")
        {
            
            this.ateValue++;

            // 食べた小麦は消す
            if (this.ateValue <= this.FULL_KOMUGI) Destroy(in_col.gameObject);

            // 満腹になったらフラグ立て＆移動処理変更
            if (this.ateValue >= this.FULL_KOMUGI && this.isGoHome != true)
            {
                Debug.Log("お腹いっぱいじゃー");
                PrepareGoHome();
            }
        }
    }


    public void PrepareGoHome()
    {
        this.isGoHome = true;
        this.startGoBackPosition = this.transform.position;
        this.moveSpeed_x = 20.0f;
        this.ateValue = this.FULL_KOMUGI;

        // 逆側向いてく
        this.transform.localScale = new Vector3(-1, this.transform.localScale.y);
    }

    /// <summary>
    /// 満腹なのでお家に帰りまーす
    /// </summary>
    private void GoBackHome()
    {
        //this.transform.position = new Vector3(this.transform.position.x+0.35f, this.transform.position.y, this.transform.position.z);
        if (this.canGoHome) // モグラ以外は画面外に移動し終わってからデストロイする
        {
            if (this.startGoBackPosition.x + 50.0f < this.transform.position.x)
            {
                this.enemyCtrl.DestroyFromList(this.gameObject);
                Destroy(this.gameObject);
            }
        }
        else
        {
            this.enemyCtrl.DestroyFromList(this.gameObject); // モグラは即効でデストロイ
            Destroy(this.gameObject);
        }
    }

}
