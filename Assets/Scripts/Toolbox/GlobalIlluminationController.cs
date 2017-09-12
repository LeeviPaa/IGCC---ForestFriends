using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalIlluminationController : MonoBehaviour {

    public float GlobalIllumination = 1;
    private List<GlobalLightReciever> lightList = new List<GlobalLightReciever>();

	void Start () {
        
        foreach(GlobalLightReciever L in Resources.FindObjectsOfTypeAll<GlobalLightReciever>())
        {
            lightList.Add(L);
        }
	}

    float prevIlluminationValue = 0;
    void Update()
    {
        if(prevIlluminationValue != GlobalIllumination)
        {
            lightList.ForEach(delegate (GlobalLightReciever L)
            {
                L.IntensityModifier = GlobalIllumination;
            });
        }
        prevIlluminationValue = GlobalIllumination;
    }

}
