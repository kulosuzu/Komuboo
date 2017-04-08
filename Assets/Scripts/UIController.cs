using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    /// <summary>
    /// インスペクター上からのキャッシュ
    /// </summary>
    [SerializeField]
    private Transform komuboo;

    /// <summary>
    /// UIスライダーをキャッシュ
    /// </summary>
    [SerializeField]
    private Slider directionSlider;

    [SerializeField]
    private Slider directionSlider2;



    /// <summary>
    /// 距離を取得するため
    /// </summary>
    [SerializeField]
    private BaseStage stageObj;


	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        this.directionSlider.value = CalcLength2Goal();
        //this.directionSlider2.value = CalcLength2Goal();

	}

    float CalcLength2Goal()
    {
        // スライダーは割合でとっているため
        return 1 - ((this.stageObj.GetStageLength() - this.komuboo.transform.position.x) / this.stageObj.GetStageLength());
    }
}
