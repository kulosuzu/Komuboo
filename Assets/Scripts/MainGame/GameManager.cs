using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : ObjectManager {

    [SerializeField]
    private GameObject cache_komuboo;

    [SerializeField]
    private BaseStage cache_stage;

    private float stageLength;

    private int generateState;

    public GameObject railPrefab;

    [SerializeField]
    private Transform stageTrans;



    public string difficalty; // Easy, Normal, Hard
    [SerializeField]
    private Animator tramAnimator;


    // 背景ストップのため
    public GameObject ground_backgroundObjs;
    public GameObject underground_backgroundObjs;
    public ScrollTimer scrollTimer;

    
	// Use this for initialization
	new void Start () {
        this.stageLength = this.cache_stage.GetStageLength();
        this.generateState = 0;

        if (this.tramAnimator != null)
        {
            switch (this.difficalty)
            {
                case "Easy":
                    tramAnimator.SetInteger("Stage", 1);
                    break;
                case "Normal":
                    tramAnimator.SetInteger("Stage", 2);
                    break;
                case "Hard":
                    tramAnimator.SetInteger("Stage", 3);
                    break;
                default:
                    tramAnimator.SetInteger("Stage", 0);
                    break;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {

        CheckInstanciateObjects();

        // ゴール
        if (this.cache_komuboo.transform.position.y < -5 && this.cache_komuboo.transform.position.x > 300)
        {
            this.cache_komuboo.GetComponent<Komuboo>().SaveInformation();
            SceneManager.LoadScene("Result");
        }

        // デバッグ用　地下で落ちた時
        if (this.cache_komuboo.transform.position.y < -30)
        {
            this.cache_komuboo.GetComponent<Komuboo>().SaveInformation();
            SceneManager.LoadScene("Fail"); 
        }

        // ギミックに乗っている間はスクロールを止める
        if (this.cache_komuboo.GetComponent<Komuboo>().onUpDownGimic)
        {
            ChangeScrollState(false);
        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
	}


    // 死亡ゲーム終了
    public void KomubooDeath()
    {
        this.cache_komuboo.GetComponent<Komuboo>().SaveInformation();
        SceneManager.LoadScene("Fail");
    }

    void CheckInstanciateObjects()
    {
        float komuboo_x = this.cache_komuboo.transform.position.x;

        if (this.generateState == 0)
        {
            if (!(komuboo_x >= this.stageLength * 1 / 3 && komuboo_x < this.stageLength * 2 / 3)) return;
            else
            {
                this.generateState = 1;
                GameObject instanciate = InstantiateObjects(this.railPrefab, this.stageTrans);
                instanciate.transform.position = new Vector3(komuboo_x + 30, instanciate.transform.position.y, -0.55f);
            }
        }

    }

    /// <summary>
    /// スクロールを止めたり再開したりする
    /// </summary>
    /// <param name="in_parameter"></param>
    public void ChangeScrollState(bool in_parameter)
    {
        this.scrollTimer.isMoving = in_parameter;

        Transform children = this.ground_backgroundObjs.transform.GetComponentInChildren<Transform>();
        foreach (Transform child in children)
        {
            Scroll scrollComp = null;
            scrollComp = child.gameObject.GetComponent<Scroll>();
            if (scrollComp != null)
            {
                scrollComp.isMoving = in_parameter;
            }
        }

        children = this.underground_backgroundObjs.transform.GetComponentInChildren<Transform>();
        foreach (Transform child in children)
        {
            Scroll scrollComp = null;
            scrollComp = child.gameObject.GetComponent<Scroll>();
            if (scrollComp != null)
            {
                scrollComp.isMoving = in_parameter;
            }
        }       
    }


}
