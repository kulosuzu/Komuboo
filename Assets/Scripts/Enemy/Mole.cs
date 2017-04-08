using UnityEngine;
using System.Collections;

public class Mole : BaseEnemy {


    void Start()
    {
        StartActivate();
    }

    // Use this for initialization
    public void StartActivate()
    {
        base.Start();
        this.FULL_KOMUGI = 1;
        base.stoleValue = 3;
        base.moveSpeed_x = 0;
        base.canGoHome = false;
        if (base.isActive) StartCoroutine("MoveUpDown");
        else this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y - 1.0f, this.transform.localPosition.z);
    }

    // Update is called once per frame
    void Update()
    {
        base.FixedUpdateProcess();

    }

    IEnumerator MoveUpDown()
    {
        float speed = -0.025f;
        while (true)
        {
            if (this.transform.localPosition.y < -0.6f || this.transform.localPosition.y > 1.5f) speed *= -1;
            this.transform.localPosition += new Vector3(0, speed, 0);
            yield return null;
        }
    }


    
}
