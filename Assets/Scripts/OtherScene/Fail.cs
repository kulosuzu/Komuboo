using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Fail : MonoBehaviour {

    // お届けした量。先に保持しておく
    private int valueOtodoke;

    private int valueShuppatsu;


    [SerializeField]
    private Text shuppatsu;
    [SerializeField]
    private Text lost;
    [SerializeField]
    private Text otodoke;


	// Use this for initialization
	void Start () {
        this.valueShuppatsu = Komuboo.GetSavedKomugiInit();
        this.valueOtodoke = Komuboo.GetSavedKomugiCurrent();
        this.shuppatsu.text = this.valueShuppatsu.ToString()+"kg";
        this.lost.text = this.valueShuppatsu.ToString()+"kg";
        this.otodoke.text = "0kg";
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("Title");  
        }
    }
}
