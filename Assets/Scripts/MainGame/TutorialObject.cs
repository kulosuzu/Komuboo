using UnityEngine;
using System.Collections;

public class TutorialObject : ObjectManager {

    [SerializeField] private TutorialManager tutMngIns;
    [SerializeField]
    private KeyCode key;

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Komuboo")
        {
            this.tutMngIns.CreateWindow(this.key);
            Time.timeScale = 0;
        }

    }

}
