using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    public float alpha;
    public float fadespeed;
    private float red, green, blue;

    public bool fadeIn;
    public bool fadeOut;

    public float displayTime;
    private bool isDis;


    // Use this for initialization
    void Start()
    {
        red = GetComponent<Image>().color.r;
        green = GetComponent<Image>().color.g;
        blue = GetComponent<Image>().color.b;
        isDis = true;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Image>().color = new Color(red, green, blue, alpha);

        displayTime -= Time.deltaTime;

        if (displayTime < 0)
            isDis = false;

        if (fadeIn && !isDis)
            FadeIn();

        if (fadeOut && !isDis)
            FadeOut();
    }

    public void FadeIn()
    {
        if (alpha < 1)
        {
            alpha += fadespeed;

            if (alpha > 1)
            {
                fadeIn = false;
            }
        }
    }

    public void FadeOut()
    {

        if (alpha > 0)
        {
            alpha -= fadespeed;

            if (alpha < 0)
            {
                fadeOut = false;
            }
        }
    }
}

