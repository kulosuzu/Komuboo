using UnityEngine;
using System.Collections;

public class TrokStart : MonoBehaviour {

    public GameObject cache_Komuboo;
    public GameObject cache_Trok;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void OnTriggerEnter2D(Collider2D in_col)
    {
        if (in_col.gameObject.tag == "Komuboo")
        {
            this.cache_Komuboo.GetComponent<Animator>().SetTrigger("Komuboo_Tram");
            this.cache_Trok.GetComponent<Animator>().SetTrigger("Up2Ground");
        }

    }
}
