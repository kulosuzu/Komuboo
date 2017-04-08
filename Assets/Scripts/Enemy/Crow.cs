using UnityEngine;
using System.Collections;

/// <summary>
/// カラススクリプト
/// </summary>
public class Crow : BaseEnemy {

    private GameObject komuboo_cache;

    private bool isAttack;

    //キャッシュ
    private Rigidbody2D crowRigidBody;

    public void StartActivate()
    {
        base.Start();
        this.FULL_KOMUGI = 1;
        this.isAttack = false;
        base.stoleValue = 4;

        this.komuboo_cache = GameObject.Find("Komuboo");


        base.moveSpeed_x = -0.5f;
        //Fly2Komuboo();

        // キャッシュ取得
        this.crowRigidBody = GetComponent<Rigidbody2D>();
    }

	// Use this for initialization
	void Start () {
        StartActivate();
	}

    void Update()
    {
        if (this.komuboo_cache.transform.position.x + 15.5f >= this.transform.position.x)
        {
            this.isAttack = true;
        } 

    }
	
	// Update is called once per frame
	void FixedUpdate () {

        base.FixedUpdateProcess();

        // こむぶーを狙うタイプの挙動
        if (base.isActive)
        {
            // 上空にいる場合はこむぶーに向かって飛ぶ
            if (this.isAttack && this.transform.localPosition.y >= -3.5)
            {
                this.crowRigidBody.velocity = new Vector2(this.crowRigidBody.velocity.x, -10.0f);

                Vector2 vec = (this.transform.position - this.komuboo_cache.transform.position).normalized;

                this.transform.rotation = Quaternion.FromToRotation(Vector3.right, vec);

            }
            else
            {
                this.crowRigidBody.velocity = new Vector2(this.crowRigidBody.velocity.x, 0);
            }
        }
	}

    /// <summary>
    /// こむぶーへ突撃
    /// </summary>
    public void Fly2Komuboo()
    {
        this.isAttack = true;
    }

}
