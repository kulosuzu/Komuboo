using UnityEngine;
using System.Collections;

public class BaseStage : MonoBehaviour
{


    /// <summary>
    /// ステージの全長
    /// </summary>
    protected float stageLength;

    public float GetStageLength()
    {
        return this.stageLength;
    }

    // Use this for initialization
    protected void Start()
    {


    }

    // Update is called once per frame
    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    protected void FixedUpdate()
    {

    }
}
