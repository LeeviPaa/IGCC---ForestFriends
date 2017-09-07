using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class FoodSpawner : MonoBehaviour {
    [SerializeField]
    private GameObject Food;
	// Use this for initialization
	void Start () {
        Observable.Interval(System.TimeSpan.FromSeconds(1))
                .Subscribe(_=>SpawnFood());	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void SpawnFood()
    {
      var go =  Instantiate(Food, new Vector3(Random.Range(-12, 12), 10, Random.Range(-12, 12)), Quaternion.identity);
        go.name = "Food";
    }
}
