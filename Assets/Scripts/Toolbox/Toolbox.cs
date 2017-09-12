using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toolbox : Singleton<Toolbox>
{
    protected Toolbox() { } // guarantee this will be always a singleton only - can't use the constructor!
    private EventManager E;

    //some global functions
    private PoivotPoint P;
    public PoivotPoint Pivot
    {
        set
        {
            P = value;
        }
        get
        {
            return P;
        }
    }

    void Awake()
    {
        //E = RegisterComponent<EventManager>();
        // Your initialization code here
        MasujimaRyohei.SceneBase.UseManager(typeof(MasujimaRyohei.AudioManager));
    }

    static public T RegisterComponent<T>() where T : Component
    {
        return Instance.GetOrAddComponent<T>();
    }
}

[System.Serializable]
public class Language
{
    public string current;
    public string lastLang;
}
