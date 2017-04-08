using UnityEngine;
using System.Collections;

public class OnlyScroll : MonoBehaviour {

    // スクロールするスピード
    public float speed = 0.1f;

    public Renderer rend;

    // Use this for initialization
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // 時間によってYの値が0から1に変化していく。1になったら0に戻り、繰り返す。
        float x = Mathf.Repeat(Time.time * speed, 1);

        // Yの値がずれていくオフセットを作成
        Vector2 offset = new Vector2(x, 0);

        // マテリアルにオフセットを設定する
        rend.material.SetTextureOffset("_MainTex", offset);
    }
}
