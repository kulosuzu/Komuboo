using UnityEngine;
using System.Collections;

public class ScrollTimer : MonoBehaviour {

    public float time;

    public bool isMoving;

	// Use this for initialization
	void Start () {
        this.time = 0;
        this.isMoving = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (this.isMoving)
        {
            this.time += 0.01f;
            if (this.time > 60) this.time = 0;
        }
	}
}
