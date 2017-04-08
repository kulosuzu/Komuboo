using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class How2Play : MonoBehaviour {

    // 画像挿入
    public Sprite[] page = new Sprite[10];

    // 画像描画場所
    [SerializeField]
    private Image drawPlace;

    private int pageIndex;


	// Use this for initialization
	void Start () {
        this.pageIndex = 0;
        UpdateDraw();
	}
	
	// Update is called once per frame
	void Update () {
	  
	}

    private void UpdateDraw()
    {
        this.drawPlace.sprite = this.page[this.pageIndex];
    }


    public void GoBack(){
        if (this.pageIndex == 0)
        {
            SceneManager.LoadScene("Title"); 
            return;
        }
        this.pageIndex--;
        UpdateDraw();
    }

    public void GoNext()
    {
        if (this.pageIndex == 11)
        {
            SceneManager.LoadScene("Title"); 
            return;
        }
        this.pageIndex++;
        UpdateDraw();
    }
}
