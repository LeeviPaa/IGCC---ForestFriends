using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject Food;

    [SerializeField]
    private float spawnLevel;
    [SerializeField]
    private float spawnDuration;
    // Use this for initialization
    void Start()
    {
        Observable.Interval(System.TimeSpan.FromSeconds(spawnDuration))
                .Subscribe(_ => SpawnFood());
    }

    // Update is called once per frame
    void Update()
    {

    }
    void SpawnFood()
    {
        var go = Instantiate(Food, new Vector3(transform.position.x + Random.Range(-spawnLevel, spawnLevel), transform.position.y, transform.position.z + Random.Range(-spawnLevel, spawnLevel)), Quaternion.identity);
        go.name = "Food";
    }
}
