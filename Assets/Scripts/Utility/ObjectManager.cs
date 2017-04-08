using UnityEngine;
using System.Collections;

public class ObjectManager : MonoBehaviour {


    /// <summary>
    /// オブジェクトをシーン上に設定する
    /// </summary>
    /// <param name="in_prefab">シーンに設定するプレハブ</param>
    /// <param name="in_parent">シーンに設定する際の親となるオブジェクト</param>
    public GameObject InstantiateObjects(GameObject in_prefab, Transform in_parent)
    {

        GameObject ins = Instantiate(in_prefab) as GameObject;
        SetParentObject(in_parent, ins.transform);

        return ins;
    }

    /// <summary>
    /// オブジェクトに親子関係を設定する
    /// </summary>
    /// <param name="in_parent">親となるオブジェクト</param>
    /// <param name="in_target">子となるオブジェクト</param>
    public void SetParentObject(Transform in_parent, Transform in_target)
    {

        Vector3 sizeOriginal = in_target.localScale;
        Vector3 posOriginal = in_target.localPosition;

        in_target.parent = in_parent;
        in_target.localScale = sizeOriginal;
        in_target.localPosition = posOriginal;
    }
}
