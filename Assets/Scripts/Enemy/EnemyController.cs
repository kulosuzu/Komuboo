using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyController : ObjectManager {

    public class EnemyDataStructure
    {
        public string type; // 1:crow, 2:wolf, 3:bat, 4:mole
        public float x;
        public float y;
        public bool isGround; // 地上か
        public int isActive; // アクティブエネミーか
    }


    private List<EnemyDataStructure> list_enemyStructures;
    public List<GameObject> list_enemies;

    public TextAsset textData;

    /// <summary>
    /// 敵生成先地上
    /// </summary>
    [SerializeField]
    private Transform parentGround;

    /// <summary>
    /// 敵生成先地下
    /// </summary>
    [SerializeField]
    private Transform parentUnderGround;


    // 敵プレハブ
    public GameObject prefab_crow;
    public GameObject prefab_wolf;
    public GameObject prefab_bat;
    public GameObject prefab_mole;
    public GameObject prefab_mud;

    // 親オブジェクト
    public GameObject groundEnemies;
    public GameObject undergroundEnemies;

	// Use this for initialization
	void Start () {
        //this.list_enemyStructures = new List<EnemyDataStructure>();
        this.list_enemies = new List<GameObject>();

        int count = 0;

        // インスペクタ上から持ってくるパターン
        if (this.groundEnemies != null)
        {
            foreach (Transform child in this.groundEnemies.transform)
            {

                    this.list_enemies.Add(child.gameObject);

            }
            count = 0;
        }

        if (this.undergroundEnemies != null)
        {
            foreach (Transform child in this.undergroundEnemies.transform)
            {

                    this.list_enemies.Add(child.gameObject);

            }

        }

        // アクティブ化
        foreach (GameObject enemyObj in this.list_enemies)
        {
            if (enemyObj.GetComponent<Crow>() != null)
            {
                enemyObj.GetComponent<Crow>().StartActivate();
                enemyObj.name = "Crow";
            }
            if (enemyObj.GetComponent<Wolf>() != null)
            {
                enemyObj.GetComponent<Wolf>().StartActivate();
                enemyObj.name = "Wolf";
            }
            if (enemyObj.GetComponent<Bat>() != null)
            {
                enemyObj.GetComponent<Bat>().StartActivate();
                enemyObj.name = "Bat";
            }
            if (enemyObj.GetComponent<Mole>() != null)
            {
                enemyObj.GetComponent<Mole>().StartActivate();
                enemyObj.name = "Mole";
            }

        }



        //LoadTextData(this.textData);
        //foreach (var data in this.list_enemyStructures)
        //{
        //    GenerateEnemy(data);
        //}
        this.list_enemies.Sort((IComparer<GameObject>)new sort());

        GenerateMud();
	}





    private class sort : IComparer<GameObject>
    {
        int IComparer<GameObject>.Compare(GameObject in_objA, GameObject in_objB){
            return (int)(in_objA.transform.position.x - in_objB.transform.position.x);
        }
    }


    public void LoadTextData(TextAsset in_textData)
    {
        // まるごと取り込み
        string level_texts = in_textData.text;

        // １行ずつに分ける
        string[] lines = level_texts.Split('\n');

        foreach (string line in lines)
        {
            if (line == "") continue; // 空っぽなら飛ばす

            Debug.Log(line);

            // 単語ごとに区切る
            string[] words = line.Split();

            int n = 0;

            EnemyDataStructure enemyData = new EnemyDataStructure();

            foreach (string word in words)
            {
                if (word.StartsWith("#")) break; // 先頭が#なら抜ける
                if (word == "") continue; // 空っぽなら飛ばす

                switch (n)
                {
                    case 0:
                        enemyData.type = word;
                        break;
                    case 1:
                        enemyData.x = float.Parse(word);
                        break;
                    case 2:
                        enemyData.y = float.Parse(word);
                        break;
                    case 3:
                        enemyData.isGround = bool.Parse(word);
                        break;
                    case 4:
                        enemyData.isActive = int.Parse(word);
                        break;

                }
                n++;
            }

            if (n >= 5)
            {
                this.list_enemyStructures.Add(enemyData);
            }
            else
            {
                if (n == 0) ; // ０ならコメントなのでスルー
                else // こっちはエラー
                {
                    Debug.LogError("[EnemyDataStructure] Out of parameter.\n");
                }
            }
        }

    }


    public void GenerateEnemy(EnemyDataStructure in_enemyDataStructure)
    {
        GameObject enemyObj = null;
        switch (in_enemyDataStructure.type)
        {
            case "crow":
                enemyObj = Instantiate(this.prefab_crow);
                enemyObj.transform.SetParent(this.parentGround, true);
                break;

            case "wolf":
                enemyObj = Instantiate(this.prefab_wolf);
                enemyObj.transform.SetParent(this.parentGround, true);
                break;

            case "bat":
                enemyObj = Instantiate(this.prefab_bat);
                enemyObj.transform.SetParent(this.parentUnderGround, true);
                break;
            case "mole":
                enemyObj = Instantiate(this.prefab_mole);
                enemyObj.transform.SetParent(this.parentUnderGround, true);
                break;
        }

        if (enemyObj != null)
        {
            // 座標指定
            enemyObj.transform.localPosition = new Vector3(in_enemyDataStructure.x, in_enemyDataStructure.y, -1);

            if (in_enemyDataStructure.isActive > 0 ) enemyObj.GetComponent<BaseEnemy>().isActive = true;

            switch (in_enemyDataStructure.type)
            {
                case "crow":
                    enemyObj.GetComponent<Crow>().StartActivate();
                    enemyObj.name = "Crow";
                    break;
                case "wolf":
                    enemyObj.GetComponent<Wolf>().StartActivate();
                    enemyObj.name = "Wolf";
                    break;
                case "bat":
                    enemyObj.GetComponent<Bat>().StartActivate();
                    enemyObj.name = "Bat";
                    break;
                case "mole":
                    enemyObj.GetComponent<Mole>().StartActivate();
                    enemyObj.name = "Mole";
                    break;
            }

            this.list_enemies.Add(enemyObj);
        }
    }

    public void Bomb(Transform in_komubooTrans)
    {
        float targetRange_x = in_komubooTrans.position.x;
        float targetRange_y = in_komubooTrans.position.y;

        // エラー回避のためコピーする
        GameObject[] enemies = new GameObject[this.list_enemies.Count];
        this.list_enemies.CopyTo(enemies);

        foreach (GameObject enemy in enemies)
        {
            if (enemy != null && enemy.transform.position.x >= targetRange_x  && enemy.transform.position.x < targetRange_x + 15f
                    && enemy.transform.position.y >= targetRange_y -10f && enemy.transform.position.y < targetRange_y + 10f)
            {
                DestroyEnemy(enemy);
            }   
        }
    }



    public void DestroyEnemy(GameObject in_enemy)
    {
        if (in_enemy == null) return;

        // お腹いっぱいにさせて帰らせる
        if (in_enemy.name == "Mole")
        {
            DestroyFromList(in_enemy);
            Destroy(in_enemy.gameObject);
        }
        else
            in_enemy.GetComponent<BaseEnemy>().PrepareGoHome();


        //DestroyFromList(in_enemy);
    }


    /// <summary>
    /// BaseEnemyからも呼出されないといけないため
    /// </summary>
    /// <param name="in_enemy"></param>
    public void DestroyFromList(GameObject in_enemy)
    {
        this.list_enemies.Remove(in_enemy); // アドレスが違っても実体の中身が一緒であれば消える
    }

    //　土生成
    public void GenerateMud()
    {
        foreach (GameObject data in this.list_enemies)
        {
            if (data.gameObject.name == "Mole")
            {
                GameObject mudObj = Instantiate(this.prefab_mud);
                mudObj.transform.SetParent(this.parentUnderGround, true);
                mudObj.transform.localPosition = new Vector3(data.transform.localPosition.x, 0.55f, data.transform.localPosition.z);
            }
        }
    }


}
