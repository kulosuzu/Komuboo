using UnityEngine;
using System.Collections;

public class Tram : MonoBehaviour {
    [SerializeField]
    private GameObject cach_komuboo;

    [SerializeField]
    private GameObject cach_upHole;
    public Sprite broken;

    // スクロールのため
    public GameManager gameMng;

    // トロッコとこむぶーを切り離すため
    public Transform stageTrans;

    private BGMController bgmCtrl;

    void Awake()
    {
        this.bgmCtrl = GameObject.Find("BGMController").GetComponent<BGMController>();
    }

    public void EndTram()
    {
        this.gameMng.ChangeScrollState(true);
        cach_komuboo.transform.SetParent(this.stageTrans, true);
        cach_komuboo.GetComponent<Komuboo>().StartWalking();
        cach_komuboo.transform.localRotation = new Quaternion();
        cach_komuboo.GetComponent<Komuboo>().onUpDownGimic = false;
    }

    public void BreakIta()
    {
        this.cach_upHole.GetComponent<SpriteRenderer>().sprite = this.broken;
        this.bgmCtrl.ChangeBGM("normal");
    }
}
