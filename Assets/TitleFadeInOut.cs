using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleFadeInOut : MonoBehaviour
{

    public float alpha;
    public float fadespeed;
    private float red, green, blue;

    public float displayTime;
    private bool isDis;

    // Use this for initialization
    void Start ()
    {
        red = GetComponent<Image>().color.r;
        green = GetComponent<Image>().color.g;
        blue = GetComponent<Image>().color.b;
    }
	
	// Update is called once per frame
	void Update ()
    {
        GetComponent<Image>().color = new Color(red, green, blue, alpha);

        if(alpha < 1 && !isDis)
        {
            alpha += fadespeed;

            if(alpha > 1)
            {
                isDis = true;
            }
        }

        if(alpha > 0 && isDis)
        {
            displayTime -= Time.deltaTime;

            if(displayTime < 0)
            {
                alpha -= Time.deltaTime;
            }
        }
    }
}
