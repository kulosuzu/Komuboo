using UnityEngine;
using System.Collections;

public class Util
{

    /// <summary>
    /// gameObjectをInstantiateし、アッタッチされているcomponentを取得する
    /// </summary>
    /// <returns>The component.</returns>
    /// <param name="prefabPath">Prefab path.</param>
    /// <param name="parent">Parent.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public static T InstantiateComponent<T>(string prefabPath, Transform parent) where T : MonoBehaviour
    {
        GameObject prefabObj = Resources.Load<GameObject>(prefabPath);
        return InstantiateComponent<T>(prefabObj, parent);
    }

    /// <summary>
    /// gameObjectをInstantiateし、アッタッチされているcomponentを取得する
    /// </summary>
    /// <returns>The component.</returns>
    /// <param name="prefab">Prefab.</param>
    /// <param name="parent">Parent.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public static T InstantiateComponent<T>(GameObject prefabObj, Transform parent) where T : MonoBehaviour
    {
        if (prefabObj == null)
        {
            return null;
        }
        GameObject obj = GameObject.Instantiate<GameObject>(prefabObj);
        obj.transform.parent = parent;
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localScale = Vector3.one;
        obj.transform.name = prefabObj.name;
        T component = obj.GetComponent<T>();
        if (component == null)
        {
            Debug.LogError(string.Format("nothing component::{0}", typeof(T).Name));
            GameObject.Destroy(obj);
            return null;
        }
        return component;
    }


}
