using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StageManager : MonoBehaviour {

    /// <summary>
    /// 管理するステージ
    /// </summary>
    [SerializeField]
    private BaseStage currentStage;

    /// <summary>
    /// キャッシュでこむぶー取得
    /// </summary>
    [SerializeField]
    GameObject komuboo;

    /// <summary>
    /// こむぶーのTransformコンポーネント
    /// </summary>
    //[SerializeField]
    public Transform transKomuboo;


    /// <summary>
    /// ステージを生成するために拝見のプレハブをアタッチ
    /// </summary>
    public GameObject stagePrefab;

    private bool stageInsFlag;


    // Instanciateのための親
    public Transform stageTrans;


    public GameObject oasobi;

	// Use this for initialization
	void Start () {
      
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            Application.Quit();
        }
	    
	}






}
