using UnityEngine;
using System.Collections;

public class KomugiWindow : MonoBehaviour {


    private RectTransform thisRectTrans;

    [SerializeField]
    private Komuboo komuboo_cache;

    // 小麦窓の１辺を取っておく
    private float rectLength;

    // 初期位置を保存
    private float init_y;

	// Use this for initialization
	void Start () {
        this.thisRectTrans = this.GetComponent<RectTransform>();
        this.rectLength = this.thisRectTrans.sizeDelta.y;
        this.init_y = this.thisRectTrans.localPosition.y;
	}
	
    void FixedUpdate()
    {

        //this.thisRectTrans.sizeDelta = new Vector2(this.rectLength, /*this.komuboo_cache.GetKomugiCurrent()*/CalcPercentage());
        MoveKomugiInner();

    }


    float CalcPercentage()
    {
        float percentage = (float)this.komuboo_cache.GetKomugiCurrent() / this.komuboo_cache.GetKomugiInitial();
        return percentage * this.rectLength;
    }

    void MoveKomugiInner()
    {
        int lost = this.komuboo_cache.GetKomugiInitial() - this.komuboo_cache.GetKomugiCurrent();
        this.thisRectTrans.localPosition = new Vector3(this.thisRectTrans.localPosition.x, this.init_y - (lost*(this.rectLength/this.komuboo_cache.GetKomugiInitial())));
    }






}
