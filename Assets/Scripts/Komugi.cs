using UnityEngine;
using System.Collections;

public class Komugi : MonoBehaviour {

    float initx;

    IEnumerator KomugiBehavior()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(this.gameObject);    
    }


    void Move()
    {
        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x + 0.5f, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
    }

	// Use this for initialization
	void Start () {
        this.initx = this.gameObject.transform.position.x;
        StartCoroutine(this.KomugiBehavior());
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Move();
	}
}
