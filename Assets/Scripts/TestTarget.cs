using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestTarget : MonoBehaviour {
    [SerializeField]
    private NavMeshAgent agent;
    
	// Use this for initialization
	void Start () {
        StartCoroutine(SetRandamDestination());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    IEnumerator SetRandamDestination()
    {
        while(true)
        {
            agent.destination = new Vector3(Random.Range(-12, 12), 0, Random.Range(-12, 12));
            yield return new WaitForSeconds(3.0f);
        }
    }
}
