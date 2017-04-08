using UnityEngine;
using System.Collections;

public class House : MonoBehaviour {

    /// <summary>
    /// 移動制御用
    /// </summary>
    [SerializeField]
    private MoveController moveController;

    // 家が走らないようにするため
    private float moveValue;
    private float prevKomubooX;
    private float currentKomubooX;
    private float prevKomubooY;
    private float currentKomubooY;

    private Rigidbody thisRigid;

    [SerializeField]
    private GameObject cache_komuboo;

    private float goalPosition = 305.0f;

	// Use this for initialization
	void Start () {
        this.thisRigid = this.GetComponent<Rigidbody>();
        this.moveController = GameObject.Find("MoveController").GetComponent<MoveController>();
        this.cache_komuboo = GameObject.Find("Komuboo");
        StartCoroutine("DestroyMe");
        this.currentKomubooX = this.cache_komuboo.transform.position.x;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        // 家が走らないようにするため
        prevKomubooX = currentKomubooX;
        currentKomubooX = this.cache_komuboo.transform.position.x;

        if (this.currentKomubooX > this.prevKomubooX && this.currentKomubooX < this.goalPosition-6)
        {
            this.thisRigid.velocity = new Vector3(8.4f, this.thisRigid.velocity.y, this.thisRigid.velocity.z);

        }
        else this.thisRigid.velocity = new Vector3(0f, this.thisRigid.velocity.y, this.thisRigid.velocity.z);
    }

    IEnumerator DestroyMe()
    {
        while (this.transform.position.x > this.cache_komuboo.transform.position.x - 5) yield return null;

        Destroy(this.gameObject);
    }
}
