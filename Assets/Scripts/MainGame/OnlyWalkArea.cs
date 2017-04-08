using UnityEngine;
using System.Collections;

public class OnlyWalkArea : MonoBehaviour {

    public void OnTriggerEnter2D(Collider2D in_col)
    {

        if (in_col.tag == "Komuboo")
        {
            in_col.GetComponent<Komuboo>().ChangeCommandPermission(false);
        }
    }

    public void OnTriggerExit2D(Collider2D in_col)
    {

        if (in_col.tag == "Komuboo")
        {
            in_col.GetComponent<Komuboo>().ChangeCommandPermission(true);
        }
    }
}
