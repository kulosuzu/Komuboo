using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MessageWindow : MonoBehaviour {

    [SerializeField] private Text message;

    public void Init(string message)
    {
        this.message.text = message;
    }

	
	
}
