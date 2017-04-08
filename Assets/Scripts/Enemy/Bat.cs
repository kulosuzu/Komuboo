using UnityEngine;
using System.Collections;

public class Bat : BaseEnemy {

    private float initHeight; // 高さのレンジ

    public float hoveringValue; // 動きのレンジ

    public void StartActivate()
    {
        base.Start();
        this.FULL_KOMUGI = 2;
        base.moveSpeed_x = 0f;
        base.stoleValue = 5;
        this.initHeight = this.transform.localPosition.y;
        StartCoroutine("FlyUpDown");
    }

    // Use this for initialization
    void Start()
    {
        StartActivate();
    }

    // Update is called once per frame
    void Update()
    {
        base.FixedUpdateProcess();

    }

    IEnumerator FlyUpDown()
    {
        float speed = -0.1f;
        while (true)
        {
            if (this.transform.localPosition.y > this.initHeight || this.transform.localPosition.y < this.initHeight-this.hoveringValue) speed *= -1; 
            this.transform.localPosition += new Vector3(0, speed, 0);
            yield return null;
        }
    }
}
