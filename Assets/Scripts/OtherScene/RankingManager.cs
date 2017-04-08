using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour {

    public Text first_text;
    public Text second_text;
    public Text third_text;
    public Text forth_text;
    public Text fifth_text;

	// Use this for initialization
	void Start () {
        int first_value = PlayerPrefs.GetInt("First_Value", 0);
        int second_value = PlayerPrefs.GetInt("Second_Value", 0);
        int third_value = PlayerPrefs.GetInt("Third_Value", 0);
        int forth_value = PlayerPrefs.GetInt("Forth_Value", 0);
        int fifth_value = PlayerPrefs.GetInt("Fifth_Value", 0);

        this.first_text.text = first_value.ToString() + "kg";
        this.second_text.text = second_value.ToString() + "kg";
        this.third_text.text = third_value.ToString() + "kg";
        this.forth_text.text = forth_value.ToString() + "kg";
        this.fifth_text.text = fifth_value.ToString() + "kg";
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ResetRanking()
    {
        PlayerPrefs.SetInt("First_Value", 0);
        PlayerPrefs.SetInt("Second_Value", 0);
        PlayerPrefs.SetInt("Third_Value", 0);
        PlayerPrefs.SetInt("Forth_Value", 0);
        PlayerPrefs.SetInt("Fifth_Value", 0);

        Start();
    }
}
