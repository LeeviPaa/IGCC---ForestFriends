using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exclamation : MonoBehaviour
{
    [SerializeField]
    private float durationDestroy = 0.5f;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(DulayDestroy());
    }
    IEnumerator DulayDestroy()
    {
        yield return new WaitForSeconds(durationDestroy);
        Destroy(gameObject);
    }
}
