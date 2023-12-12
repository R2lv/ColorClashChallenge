using UnityEngine;


public class Singleton<T> : MonoBehaviour where T : Component
{
    #region PUBLIC_VARS
    public static T Instance
    {
        get
        {
            return _instance;
        }
    }
    #endregion

    #region PRIVATE_VARS
    private static T _instance;
    #endregion

    #region UNITY_CALLBACKS
    public virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

}