using UnityEngine;
using System.Collections;

public class BGMController : MonoBehaviour {

    public AudioClip bgm_normal;
    public AudioClip bgm_underground;
    public AudioClip bgm_invincible;
    public AudioClip bgm_infinity;


	// Use this for initialization
	void Start () {
        this.GetComponent<AudioSource>().clip = this.bgm_normal;
        this.GetComponent<AudioSource>().Play();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ChangeBGM(string in_bgmName)
    {
        if (in_bgmName == "normal")
        {
            this.GetComponent<AudioSource>().clip = this.bgm_normal;
            this.GetComponent<AudioSource>().Play();
        }
        else if (in_bgmName == "underground")
        {
            this.GetComponent<AudioSource>().clip = this.bgm_underground;
            this.GetComponent<AudioSource>().Play();
        }
        else if (in_bgmName == "invincible")
        {
            this.GetComponent<AudioSource>().clip = this.bgm_invincible;
            this.GetComponent<AudioSource>().Play();
        }
        else if (in_bgmName == "infinity")
        {
            this.GetComponent<AudioSource>().clip = this.bgm_infinity;
            this.GetComponent<AudioSource>().Play();
        }
    }

}
