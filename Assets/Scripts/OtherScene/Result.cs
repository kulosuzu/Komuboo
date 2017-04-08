using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Result : ObjectManager {

    // ヒエラルキー上オブジェクトをアタッチ
    [SerializeField]
    private Slider komugi_meter;
    [SerializeField]
    private Text txt_komugiValue;
    public Text txt_subtitle;


    // お届けした量。先に保持しておく
    private int valueOtodoke;

    private bool animationEnd;


    // 遷移の代わりにオブジェクトを生成
    private GameObject detailModal; // 生成先
    public GameObject clearPrefab;
    public GameObject failPrefab;
    public GameObject slidingPrefab;

    public Sprite clearBackSprite;
    public Sprite failBackSprite;


    // 遷移状況管理
    private int state;


    // デジタル数字の色
    private Color red = new Color(255, 0, 0);   // 未達成
    private Color green = new Color(0, 255, 0); //目標クリア

    // こむぶーが上にハマったあとメーターを動かすため
    [SerializeField]
    private Animator komuboo_anim;

    // アニメーションハッシュ
    public readonly static int ANISTS_KomubooResultMove = Animator.StringToHash("Base Layer.ResultAnim");
    public readonly static int ANISTS_KomubooResultStay = Animator.StringToHash("Base Layer.ResultStay");

    // コルーチンを１回だけ呼び出す用
    private bool isStarted;

    

	// Use this for initialization
	void Start () {
        this.state = 0;
        this.isStarted = false;

        this.valueOtodoke = Komuboo.GetSavedKomugiCurrent();
        this.animationEnd = false;


        this.komugi_meter.maxValue = Komuboo.GetSavedKomugiInit();
        this.komugi_meter.value = 0;
        this.txt_komugiValue.color = this.red;

        SaveResult();
	}


    void SaveResult()
    {
        int result = Komuboo.GetSavedKomugiCurrent();

        int first_value = PlayerPrefs.GetInt("First_Value", 0);
        int second_value = PlayerPrefs.GetInt("Second_Value", 0);
        int third_value = PlayerPrefs.GetInt("Third_Value", 0);
        int forth_value = PlayerPrefs.GetInt("Forth_Value", 0);
        int fifth_value = PlayerPrefs.GetInt("Fifth_Value", 0);

        // 1位の時
        if(result > first_value){
            PlayerPrefs.SetInt("First_Value",result);
            PlayerPrefs.SetInt("Second_Value", first_value);
            PlayerPrefs.SetInt("Third_Value", second_value);
            PlayerPrefs.SetInt("Forth_Value", third_value);
            PlayerPrefs.SetInt("Fifth_Value", forth_value);
        }
        // 2位の時
        if (result < first_value && result > second_value)
        {
            PlayerPrefs.SetInt("First_Value", first_value);
            PlayerPrefs.SetInt("Second_Value", result);
            PlayerPrefs.SetInt("Third_Value", second_value);
            PlayerPrefs.SetInt("Forth_Value", third_value);
            PlayerPrefs.SetInt("Fifth_Value", forth_value);
        }
        // 3位の時
        if (result < second_value && result > third_value)
        {
            PlayerPrefs.SetInt("First_Value", first_value);
            PlayerPrefs.SetInt("Second_Value", second_value);
            PlayerPrefs.SetInt("Third_Value", result);
            PlayerPrefs.SetInt("Forth_Value", third_value);
            PlayerPrefs.SetInt("Fifth_Value", forth_value);
        }
        // 4位の時
        if (result < third_value && result > forth_value)
        {
            PlayerPrefs.SetInt("First_Value", first_value);
            PlayerPrefs.SetInt("Second_Value", second_value);
            PlayerPrefs.SetInt("Third_Value", third_value);
            PlayerPrefs.SetInt("Forth_Value", result);
            PlayerPrefs.SetInt("Fifth_Value", forth_value);
        }
        // 5位の時
        if (result < forth_value && result > fifth_value)
        {
            PlayerPrefs.SetInt("First_Value", first_value);
            PlayerPrefs.SetInt("Second_Value", second_value);
            PlayerPrefs.SetInt("Third_Value", third_value);
            PlayerPrefs.SetInt("Forth_Value", forth_value);
            PlayerPrefs.SetInt("Fifth_Value", result);
        }    
    }
	
	// Update is called once per frame
	void Update () {

        if (this.komuboo_anim.GetCurrentAnimatorStateInfo(0).fullPathHash == ANISTS_KomubooResultStay)
        {
            if (!this.isStarted)
            {
                StartCoroutine("IncreaseValue");
                this.isStarted = true;
            }
            // 数字がパタパタ変わる
            this.txt_komugiValue.text = ((int)this.komugi_meter.value).ToString();

            // 目標を超えたらデジタル数字の色を緑に変える
            if (this.txt_komugiValue.color == this.red && this.komugi_meter.value >= Komuboo.GetSavedKomugiTarget())
                this.txt_komugiValue.color = this.green;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            MoveToDetail();
        }
    }

    public void MoveToDetail()
    {
        if (!this.animationEnd) return;

        if (state == 0)
        {
            this.txt_subtitle.gameObject.SetActive(false);

            // 詳細リザルトオブジェクト生成
            if (this.valueOtodoke >= Komuboo.GetSavedKomugiTarget())
                this.detailModal = base.InstantiateObjects(this.clearPrefab, this.transform);
            else this.detailModal = base.InstantiateObjects(this.failPrefab, this.transform);

            // 値を代
            InitDetailModal();

            // 遷移済みにする
            state = 1;

            return;
        }
        else if (state == 1)
        {
            if (CheckTreasure())
            {
                Destroy(this.detailModal.gameObject);
                this.detailModal = base.InstantiateObjects(this.slidingPrefab, this.transform);
                // 失敗成功にあわせて背景変更
                if (this.valueOtodoke >= Komuboo.GetSavedKomugiTarget())
                    this.detailModal.GetComponent<Image>().sprite = this.clearBackSprite;
                else
                    this.detailModal.GetComponent<Image>().sprite = this.failBackSprite;

                PlayerPrefs.SetInt("Skill_Sliding", 1); // スライディング習得
                this.state = 2;
                return;
            }
            else
            {
                // 宝箱表示なければタイトルへ
                SceneManager.LoadScene("Title");
            }
        }
        else SceneManager.LoadScene("Title");
    }

    /// <summary>
    /// メーターの値を上げていく
    /// </summary>
    /// <returns></returns>
    IEnumerator IncreaseValue()
    {
        float speed = 0.05f;
        while (this.komugi_meter.value < this.valueOtodoke)
        {
            this.komugi_meter.value++;
            speed -= 0.05f;
            yield return new WaitForSeconds(speed);      
        }

        // 遷移可能にする
        this.animationEnd = true;
        yield return null;        
    }


    private void InitDetailModal()
    {
        Text txt_startKomugi = GameObject.Find("Shuppatsu_Value").GetComponent<Text>();
        Text txt_lostKomugi = GameObject.Find("Nakushita_Value").GetComponent<Text>();
        Text txt_otodokeKomugi = GameObject.Find("Otodoke_Value").GetComponent<Text>();
        txt_startKomugi.text = Komuboo.GetSavedKomugiInit().ToString() + "kg";
        txt_otodokeKomugi.text = Komuboo.GetSavedKomugiCurrent().ToString() + "kg";
        txt_lostKomugi.text = (Komuboo.GetSavedKomugiInit() - Komuboo.GetSavedKomugiCurrent()).ToString() + "kg";
    }

    private bool CheckTreasure()
    {
        if (PlayerPrefs.GetInt("Skill_Sliding", 0) == 1) return false; // 既にスライディング使える

        if (PlayerPrefs.GetInt("Sliding_UnLock", 0) == 0) return false; // 宝箱取ってない

        return true;
    }


}
