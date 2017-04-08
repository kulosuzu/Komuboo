using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ResultDetail : MonoBehaviour {

    public Text txt_startKomugi;
    public Text txt_lostKomugi;
    public Text txt_otodokeKomugi;

    


	// Use this for initialization
	void Start () {
        this.txt_startKomugi.text = Komuboo.GetSavedKomugiInit().ToString()+"kg";
        this.txt_otodokeKomugi.text = Komuboo.GetSavedKomugiCurrent().ToString() + "kg";
        this.txt_lostKomugi.text = (Komuboo.GetSavedKomugiInit() - Komuboo.GetSavedKomugiCurrent()).ToString() + "kg";
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void MoveToTitle()
    {
        SceneManager.LoadScene("Title"); 
    }
}
