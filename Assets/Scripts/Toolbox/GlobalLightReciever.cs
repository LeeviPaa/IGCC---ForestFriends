using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalLightReciever : MonoBehaviour {
    List<Light> lightList = new List<Light>();
    private List<float> InitIntensity = new List<float>();
    private float intensityModifier;

    void Awake()
    {
        foreach(Light L in GetComponentsInChildren<Light>())
        {
            lightList.Add(L);
            InitIntensity.Add(L.intensity);
        }
    }

    public float IntensityModifier
    {
        set
        {
            if (lightList.Count > 0)
            {
                intensityModifier = value;
                for (int i = 0; i < lightList.Count; i++)
                {
                    lightList[i].intensity = InitIntensity[i] * intensityModifier;
                }
            }
        }
    }
	
}
