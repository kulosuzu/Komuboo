using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TitleManager : ObjectManager {

    [SerializeField]
    private Komuboo cache_komuboo;


    public GameObject buttonsBefore;  
    public GameObject buttonsAfter;
    public GameObject buttonsLeft;
    public GameObject buttonsRight;
    public GameObject buttonHint;
    public GameObject buttonHint2;

    private GameObject hintPrefab;
    private GameObject hintdetailObj;
    private GameObject hint2Prefab;
    private GameObject hint2detailObj;



    // Use this for initialization
    void Start () {
        this.cache_komuboo.ForInitTitle();
        GoToBeforeButtons();
        this.hintPrefab = Resources.Load("Prefabs/Title/HintObj") as GameObject;
        this.hint2Prefab = Resources.Load("Prefabs/Title/Hint2Obj") as GameObject;
    }

    public void GoToBeforeButtons()
    {
        this.buttonsBefore.SetActive(true);
        this.buttonsAfter.SetActive(false);
        this.buttonsLeft.SetActive(false);
        this.buttonsRight.SetActive(false);
       
    }
	
    // はじめる押した後
    public void GoToAfterButtons()
    {
        this.buttonsBefore.SetActive(false);
        this.buttonsAfter.SetActive(true);
        this.buttonsLeft.SetActive(true);
        this.buttonsRight.SetActive(true);
    }

    public void OnPushHintButton()
    {
        if (this.buttonHint.transform.childCount == 0)
        {
            this.hintdetailObj = InstantiateObjects(this.hintPrefab, this.buttonHint.gameObject.transform);
        }
        else
            Destroy(this.hintdetailObj.gameObject);
    }

    public void OnPushHint2Button()
    {
        if (this.buttonHint2.transform.childCount == 0)
        {
            this.hint2detailObj = InstantiateObjects(this.hint2Prefab, this.buttonHint2.gameObject.transform);
        }
        else
            Destroy(this.hint2detailObj.gameObject);
    }


}
