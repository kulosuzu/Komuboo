using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonEvents : MonoBehaviour {

    public void MoveScene(string in_sceneName)
    {

        SceneManager.LoadScene(in_sceneName); 
    }

    public void ResetSkill()
    {
        PlayerPrefs.SetInt("Skill_Sliding", 0);
        PlayerPrefs.SetInt("BombNum", 0);
    }

    public void AddSliding()
    {
        PlayerPrefs.SetInt("Skill_Sliding", 1);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
	}
}