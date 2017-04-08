using UnityEngine;
using System.Collections;

public class Wolf : BaseEnemy {


    public void StartActivate()
    {
        base.Start();
        this.FULL_KOMUGI = 3;
        base.moveSpeed_x = -2.0f;
        base.stoleValue = 8;
    }

	// Use this for initialization
	void Start () {
        StartActivate();
	}
	
	// Update is called once per frame
	void Update () {
        base.FixedUpdateProcess();

	}
}
