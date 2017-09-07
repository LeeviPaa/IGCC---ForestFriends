namespace MasujimaRyohei
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Manager<T> : SingletonMonoBehaviour<Manager<T>> where T : SingletonMonoBehaviour<Manager<T>> 
    {
    }
}