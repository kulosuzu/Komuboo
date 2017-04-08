using UnityEngine;
using System.Collections;

public class ResultBGM : MonoBehaviour {


    [SerializeField]
    private AudioSource audioSourceFanfare;
    [SerializeField]
    private AudioSource audioSouceLoop;

    private bool startLoop;

	// Use this for initialization
	void Start () {
        this.startLoop = false;
        // まずファンファーレを鳴らす
        this.audioSourceFanfare.Play();
	}
	
	// Update is called once per frame
	void Update () {
        if (!this.audioSourceFanfare.isPlaying && !this.startLoop)
        {
            this.startLoop = true;
            this.audioSouceLoop.Play();

        }
	}
}
