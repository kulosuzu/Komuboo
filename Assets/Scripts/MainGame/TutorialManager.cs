using UnityEngine;
using System.Collections;

public class TutorialManager : ObjectManager {

    // ウィンドウ表示用
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private GameObject tutorialUI;
    private GameObject windowObj = null;

    private KeyCode startKey; // 一時停止から抜け出すキー


    // テキスト読み込み用
    [SerializeField] private TextAsset textFile;
    private string allText;
    private string[] messages;
    private int messageIndex = 0;

    // Use this for initialization
    void Start()
    {
        LoadTexts();
    }

    private void LoadTexts()
    {
        this.allText = this.textFile.text;
        this.messages = this.allText.Split('@');
    }

    public void Update()
    {
        if (Time.timeScale == 0)
        {
            if (Input.GetKeyDown(this.startKey) && this.windowObj != null)
            {
                Time.timeScale = 1;
                Destroy(this.windowObj.gameObject);
                this.messageIndex++;
            }
        }

    }


    public void CreateWindow(KeyCode key)
    {
        this.startKey = key;
        this.windowObj = InstantiateObjects(this.prefab, tutorialUI.transform);
        this.windowObj.GetComponent<MessageWindow>().Init(GetTutorialMessage());
    }

    public string GetTutorialMessage()
    {

        return this.messages[this.messageIndex];
    }

}
