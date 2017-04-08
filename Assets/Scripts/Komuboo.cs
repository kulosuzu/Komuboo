using UnityEngine;
using System.Collections;

public class Komuboo : MonoBehaviour
{

    // デバッグ制御用
    [SerializeField]
    private DebugControll debugCtrl;


    private float walkSpeed; // 毎更新のxの値
    private float initSpeed = 7.5f;

    
    public bool grounded; // 接地チェック

    [System.NonSerialized]
    public bool groundedPrev = false;

    bool goalCheck; // ゴールフラグ
    float goalTime;


    // キャッシュ
    private Transform groundCheck_L;
    private Transform groundCheck_C;
    private Transform groundCheck_R;
    private Rigidbody2D komubooRigidBody;

    // コンボ用
    private GameObject groundCheck_OnRoadObject;
    private GameObject groundCheck_OnMoveObject;
    private GameObject groundCheck_OnEnemyObject;

    // 点滅用
    private float interval = 0.2f;   // 点滅周期
    private Renderer renderer;
    private int blinkTime; // この値が0~5 の間に点滅する

    /// <summary>
    /// BGM変更のため
    /// </summary>
    protected BGMController cache_bgmCtrl;

    /*** 
     * こむぶーのパラメータ色々  
     * ****/
    /// <summary> // </summary>

    /// <summary> こむぶーの体力 // </summary>
    [SerializeField]
    private int hp;

    /// <summary> 元々持っていた小麦の量 // </summary>
    [SerializeField]
    private int komugiInitial;

    /// <summary> 今持っている小麦の量 // </summary>
    [SerializeField]
    private int komugiCurrent;

    /// <summary> 【後調整】 攻撃で小麦が減る量// </summary>
    [SerializeField]
    private int DECREAS_ATTACK;

    public GameObject prefabKomugi;

    private bool isAttack;


    // 無敵無限用
    private float startInvinciblyTime;
    private float startInfinityTime;
    [SerializeField]
    private SpriteRenderer effectRenderer;

    // アニメーション
    // エフェクト用
    [SerializeField]
    private Animator effectAnimator;
   

    private Animator bodyAnimator;

    [SerializeField]
    private GameManager gameMng;

    /// <summary>
    /// コマンド制御用フラグ　ジャンプなどを防ぐため
    /// </summary>
    private bool commandPermission;
    public void ChangeCommandPermission(bool in_parameter)
    {
        this.commandPermission = in_parameter;
    }


    /// <summary>
    /// エレベーターなどの上に乗っている
    /// </summary>
    public bool onUpDownGimic;

    public bool isJumping;

    // SE
    public AudioSource jump_se;
    public AudioSource throw_se;
    public AudioSource sliding_se;
    public AudioSource bomb_se;
    public AudioSource get_se;


    // アニメーションハッシュ
    public readonly static int ANISTS_KomubooWalk = Animator.StringToHash("BaseLayer.Walk");
    public readonly static int ANISTS_KomubooJump = Animator.StringToHash("BaseLayer.Komuboo_Jump");
    public readonly static int ANISTS_KomubooDamage = Animator.StringToHash("BaseLayer.Komuboo_Damaged");
    public readonly static int ANISTS_KomubooSliding = Animator.StringToHash("BaseLayer.Komuboo_Sliding");
    public readonly static int ANISTS_KomubooPainWalk = Animator.StringToHash("BaseLayer.Komuboo_Pain_Walk");
    public readonly static int ANISTS_KomubooPainJump = Animator.StringToHash("BaseLayer.Komuboo_Pain_Jump");
    public readonly static int ANISTS_KomubooPainSliding = Animator.StringToHash("BaseLayer.Komuboo_Pain_Sliding");
    public readonly static int ANISTS_KomubooBomb = Animator.StringToHash("BaseLayer.Komuboo_Bomb");





    public float goalPosition;


    // 無敵
    // 可視化のためいまだけpublic
    public bool _invinciblyFlag = false;
    public bool InvinciblyFlag { get { return this._invinciblyFlag; } }

    // 無限
    public bool _infinityFlag = false;
    public bool InfinityFlag { get { return this._infinityFlag; } }


    

    // ボム発動のため
    [SerializeField]
    private EnemyController enemyCtrl;

    // UIボム
    [SerializeField]
    private GameObject[] bombIcons = new GameObject[3];
  

    // 他シーンに渡す用
    public static int save_komugiInit;
    public static int save_komugiCurrent;
    public static int save_komugiTarget; // 目標数
    public static int GetSavedKomugiInit()
    {
        return save_komugiInit;
    }
    public static int GetSavedKomugiCurrent()
    {
        return save_komugiCurrent;
    }
    public static int GetSavedKomugiTarget()
    {
        return save_komugiTarget;
    }


    void Awake()
    {
        this.isAttack = false;
        this.onUpDownGimic = false;
        this.isJumping = false;
        this.commandPermission = true;

        this.startInfinityTime = Time.time;
        this.startInvinciblyTime = Time.time;

        this.renderer = GameObject.Find("KomubooSprite").GetComponent<Renderer>();
        this.cache_bgmCtrl = GameObject.FindWithTag("BGMCtrl").GetComponent<BGMController>();
        groundCheck_L = transform.Find("GroundCheck_L");
        groundCheck_C = transform.Find("GroundCheck_C");
        groundCheck_R = transform.Find("GroundCheck_R");
    }

    void InitKomuboo(int in_initValue)
    {
        this.komugiInitial = in_initValue;
        this.komugiCurrent = this.komugiInitial;

        this.walkSpeed = this.initSpeed;

        this.DECREAS_ATTACK = 1;

        // スキル設定 スライディング取得してない場合は宝箱GET情報を初期化
        if (PlayerPrefs.GetInt("Skill_Sliding", 0) == 0)       
            PlayerPrefs.SetInt("Sliding_UnLock", 0);
        else
            PlayerPrefs.SetInt("Sliding_UnLock", 1);

        // キャッシュ取得
        this.komubooRigidBody = this.GetComponent<Rigidbody2D>();
        this.bodyAnimator = this.GetComponent<Animator>();

        // ボムUI設定
        int currentNum = PlayerPrefs.GetInt("BombNum",0);
        if (currentNum > 3) currentNum = 3;
        if (this.bombIcons[0] != null)
        {
            for (int i = 0; i < currentNum; i++ )
                bombIcons[i].SetActive(true);
        }

    }

    // Use this for initialization
    void Start()
    {
        this.grounded = true;
        this.goalCheck = false;

        this.blinkTime = 10; // 最初は点滅させない

        StartCoroutine("Blink");
        // TODO:
        // とりあえず200 で。　あとからシーン切替時の小麦の量に変えます
        InitKomuboo(100);
    }

    public void ForInitTitle()
    {
        this.initSpeed = 1.0f;
        this.walkSpeed = this.initSpeed;
    }

    void Update()
    {
        if (this.grounded)
        {

            // ジャンプ
            if (Input.GetKeyDown(KeyCode.Space)
                && this.commandPermission
                && (this.bodyAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash == ANISTS_KomubooWalk
                    || this.bodyAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash == ANISTS_KomubooPainWalk)
                && this.transform.position.x < this.goalPosition // ゴール付近はジャンプできない
                && !this.onUpDownGimic // ギミックに乗っているときはジャンプできない
                /*&& !this.isJumping*/ // ジャンプ中は無効
                )  
            {

                this.bodyAnimator.SetTrigger("Jump");
                this.isJumping = true;
                //this.speed = 0;
            }

        }

        //  攻撃
        if (Input.GetKeyDown(KeyCode.Z) && this.transform.position.x < 300)
        {
            this.throw_se.Play();
            Attack();
        }

        // スライディング
        if (PlayerPrefs.GetInt("Skill_Sliding", 0) == 1 && Input.GetKeyDown(KeyCode.X)
            && this.commandPermission
            && (this.bodyAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash == ANISTS_KomubooWalk
                || this.bodyAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash == ANISTS_KomubooPainWalk) // 歩いてる時しか使えない
            && !this.onUpDownGimic // ギミックに乗っているときはできない
            )
        {
            this.bodyAnimator.SetTrigger("Sliding");
            Sliding();
        }

        // ボム発動
        if (PlayerPrefs.GetInt("BombNum", 0) > 0 && Input.GetKeyDown(KeyCode.C)
           && this.bodyAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash != ANISTS_KomubooBomb
            /*&& this.commandPermission*/)
        {
            this.bomb_se.Play();
            this.bodyAnimator.SetTrigger("Bomb");
            int currentNum = PlayerPrefs.GetInt("BombNum");
            PlayerPrefs.SetInt("BombNum", --currentNum);
            if (this.bombIcons != null && currentNum < 3)
            {
                bombIcons[currentNum].SetActive(false);
            }
            this.enemyCtrl.Bomb(this.transform);
        }
    }

    // フレームの書き換え
    void FixedUpdate()
    {
        
        if (!this.debugCtrl.isDebugging )
        {
            // 移動計算&こむぶー移動
            this.komubooRigidBody.velocity = new Vector2(walkSpeed, this.komubooRigidBody.velocity.y);
        }


        // 地面チェック
        this.groundedPrev = grounded;
        this.grounded = false;

        this.groundCheck_OnRoadObject = null;
        this.groundCheck_OnMoveObject = null;
        this.groundCheck_OnEnemyObject = null;

        // まずは当たっているもの全部取得
        Collider2D[][] groundCheckCollider = new Collider2D[3][];
        groundCheckCollider[0] = Physics2D.OverlapPointAll(groundCheck_L.position);
        groundCheckCollider[1] = Physics2D.OverlapPointAll(groundCheck_C.position);
        groundCheckCollider[2] = Physics2D.OverlapPointAll(groundCheck_R.position);

        // ばらばらにして、当たっているオブジェクトを識別しつつ格納
        foreach (Collider2D[] groundCheckList in groundCheckCollider)
        {
            foreach (Collider2D groundCheck in groundCheckList)
            {
                if (groundCheck != null)
                {
                    if (!groundCheck.isTrigger)
                    {                     
                        if (groundCheck.tag == "Road")
                        {
                            groundCheck_OnRoadObject = groundCheck.gameObject;
                            this.grounded = true;
                        }
                        else if (groundCheck.tag == "MoveObject")
                        {
                            groundCheck_OnMoveObject = groundCheck.gameObject;
                        }
                        else if (groundCheck.tag == "Enemy")
                        {
                            groundCheck_OnEnemyObject = groundCheck.gameObject;
                        }
                    }
                }
            }
        }

        // 着地した瞬間にAnimationをWalkにする
        if (!this.groundedPrev && this.grounded && this.isJumping)
        {
            this.isJumping = false;

            if (this.bodyAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash == ANISTS_KomubooWalk
                || this.bodyAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash == ANISTS_KomubooPainWalk
                || this.bodyAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash == ANISTS_KomubooBomb) return;
                
            this.bodyAnimator.SetTrigger("Walk");  
        }


    }

    public void Attack()
    {
        this.isAttack = true;
        // こむぶーの斜め右上が発射点
        Vector3 position = new Vector3(this.gameObject.transform.position.x + 2.0f, this.gameObject.transform.position.y, this.gameObject.transform.position.z);

        // インスタンシエートして小麦生成
        // 前方に飛ばす　or
        // 敵にホーミングして飛ばす

        GameObject gobj = (GameObject)Instantiate(this.prefabKomugi, position, new Quaternion());

        if (!this._infinityFlag)
        {
            KomugiDecrease(DECREAS_ATTACK);
        }
    }


    public void Sliding()
    {
        this.sliding_se.Play();
    }


    public void EndSliding()
    {
        this.GetComponent<Animator>().SetTrigger("Walk");
    }


    /// <summary>
    /// 小麦を減らす
    /// </summary>
    /// <param name="in_value">int: 減らしたい量</param>
    public void KomugiDecrease(int in_decreaseValue)
    {
        if (this.komugiCurrent > 0)
            this.komugiCurrent -= in_decreaseValue;
        else this.komugiCurrent = 0;

        this.GetComponent<Animator>().SetInteger("Komugi", this.komugiCurrent);
    }

    /// <summary>
    /// こむぶーをジャンプさせる
    /// </summary>
    private void AnimationJump()
    {
        this.jump_se.Play();
        this.walkSpeed = this.initSpeed;
        this.komubooRigidBody.velocity = Vector2.up * CalcJumpPower();
    }



    
    /// <summary>
    /// 衝突判定
    /// </summary>
    /// <param name="in_col"></param>
    public void OnTriggerEnter2D(Collider2D in_col)
    {
        if (in_col.gameObject.tag == "Enemy" && !this._invinciblyFlag)
        {
            this.blinkTime = 0;
            StartCoroutine("Blink");
            KomugiDecrease(in_col.gameObject.GetComponent<BaseEnemy>().stoleValue);
        }
        else if (in_col.gameObject.tag == "Item") // アイテムGETの時
        {
            this.get_se.Play();
            if (in_col.name == "KomugiMuteki")
            {
                if (!this._invinciblyFlag)
                {
                    SetInvincibly();
                    StartCoroutine("InvinciblyPower");
                }
            }
            else if (in_col.name == "KomugiMugen")
            {
                if (!this._infinityFlag)
                {
                    SetInfinity();
                    StartCoroutine("ImagineKomugiPower");
                }
            }
            else if (in_col.name == "Treasure")
            {
                // まずはロック解除
                //PlayerPrefs.SetInt("Skill_Sliding", 1);
                PlayerPrefs.SetInt("Sliding_UnLock", 1);
            }
            else if (in_col.name == "Bomb")
            {
                int currentNum = PlayerPrefs.GetInt("BombNum", 0);
                // 持てるのは３個まで
                if(currentNum < 3)
                    PlayerPrefs.SetInt("BombNum", ++currentNum);
                else
                    PlayerPrefs.SetInt("BombNum", 3);
                // UI更新
                if (this.bombIcons != null && currentNum > 0)
                {
                    bombIcons[currentNum-1].SetActive(true);
                }          

            }
            Destroy(in_col.gameObject);
        }
        else if (in_col.gameObject.tag == "Gimic")
        {
            if (in_col.name == "StartTrigger")
            {
                this.walkSpeed = this.initSpeed;
            }
            if (in_col.name == "OnlyWalkArea")
            {
                this.walkSpeed = this.initSpeed;
            }
        }
    }

    /// <summary>
    /// 小麦が少ないほど高くジャンプする
    /// </summary>
    /// <returns></returns>
    float CalcJumpPower()
    {
        float soten = 7.5f; // 最低でも7.5の力でジャンプする
        float percent = (float)GetKomugiCurrent() / GetKomugiInitial();
        float value = (1 - percent)*5 + soten;
        return value;
    }

    public int GetKomugiCurrent()
    {
        return this.komugiCurrent;
    }

    public int GetKomugiInitial()
    {
        return this.komugiInitial;
    }

    // 点滅コルーチン
    IEnumerator Blink()
    {
        while (this.blinkTime < 6)
        {
            this.blinkTime++;
            renderer.enabled = !renderer.enabled;
            yield return new WaitForSeconds(interval);
        }
        renderer.enabled = true;
        yield return null;
    }

    /// <summary>
    /// 終了時必ず呼び出す　
    /// 他シーンに引き渡すデータをセーブする
    /// </summary>
    public void SaveInformation(){
        save_komugiInit = this.komugiInitial;
        save_komugiCurrent = this.komugiCurrent;
        save_komugiTarget = 50; // とりあえず今は５０にしておく
    }


    public void SetInvincibly()
    {
        this._invinciblyFlag = true;
        this.startInvinciblyTime = Time.time;
        this._infinityFlag = false;
        this.effectAnimator.SetTrigger("Star");
        this.cache_bgmCtrl.ChangeBGM("invincible");
    }

    public void SetInfinity()
    {
        this._infinityFlag = true;
        this.startInfinityTime = Time.time;
        this._invinciblyFlag = false;
        this.effectAnimator.SetTrigger("Mugen");
        this.cache_bgmCtrl.ChangeBGM("infinity");
    }

    /// <summary>
    /// 無敵アイテムゲット時処理
    /// 5秒間無敵になる
    /// </summary>
    IEnumerator InvinciblyPower()
    {
        while (this._invinciblyFlag)
        {
            float localtime = Time.time;

            // 終了間際の点滅
            if (this.startInvinciblyTime + 5.2f < localtime)
            {
                this.effectRenderer.enabled = !this.effectRenderer.enabled;
            }
            if (this.startInvinciblyTime + 6.2f < localtime)
            {
                this.effectRenderer.enabled = true;
               this._invinciblyFlag = false; // 終了
            }
            yield return new WaitForSeconds(0.1f);
        }

        string stageState = "normal";
        if (this.transform.position.y < -4) stageState = "underground";
        if (!this._infinityFlag) // 無限のBGM流れてるときは変更しない
        {
            this.cache_bgmCtrl.ChangeBGM(stageState);
            this.effectAnimator.SetTrigger("None");
        }

        yield return null;
    }

    /// <summary>
    /// 無限アイテムゲット時処理
    /// うてうてー
    /// </summary>
    IEnumerator ImagineKomugiPower()
    {
        while (this._infinityFlag)
        {
            float localtime = Time.time;

            // 終了間際の点滅
            if (this.startInfinityTime + 5.2f < localtime)
            {
                this.effectRenderer.enabled = !this.effectRenderer.enabled;
            }
            if (this.startInfinityTime + 6.2f < localtime)
            {
                this.effectRenderer.enabled = true;
               this._infinityFlag = false;
            }
            yield return new WaitForSeconds(0.1f);
        }

        string stageState = "normal";
        if (this.transform.position.y < -4) stageState = "underground";
        if (!this.InvinciblyFlag) // 無敵のBGM流れてるときは変更しない
        {
            this.cache_bgmCtrl.ChangeBGM(stageState);
            this.effectAnimator.SetTrigger("None");
        }

        yield return null;
    }

    public void StopWalking()
    {
        this.walkSpeed = 0;
    }

    public void StartWalking()
    {
        this.walkSpeed = this.initSpeed;
    }

    public void StartDeath()
    {
        this.walkSpeed = 0;
        this.gameMng.ChangeScrollState(false);
    }


    public void EndByDeath()
    {
        this.gameMng.KomubooDeath();
    }




}