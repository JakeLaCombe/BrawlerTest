using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabsManager : MonoBehaviour
{
    public static PrefabsManager instance;
    public WolfEnemy enemy;
    public WolfHowl wolfHowl;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
}
