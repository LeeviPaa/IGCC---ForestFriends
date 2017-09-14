using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoomsdayTrigger : MonoBehaviour {

    private bool trigger = false;
    public float delayTimer = 5;
	void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !trigger)
        {
            trigger = true;
            StartCoroutine(DoomsdayTimer(delayTimer));
        }
    }
    IEnumerator DoomsdayTimer(float time)
    {
        //Do something

        yield return new WaitForSeconds(time);
        //Do something else
        SceneManager.LoadScene(1);
        
        yield return null;
    }
}
