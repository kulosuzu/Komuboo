using UnityEngine;
using System.Collections;

public class ChangeTramAnim : MonoBehaviour {

    public Animator animTram;


    public void OnTriggerEnter2D(Collider2D in_col)
    {
        if (in_col.gameObject.tag == "Komuboo")
        {
            // アニメーションを次のトロッコ用のものに変更
            this.animTram.SetInteger("Stage", 3);
        }
    }
}
