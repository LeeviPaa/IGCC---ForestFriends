using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointCube : MonoBehaviour {

    private GameObject particles;

    public List<Material> matList = new List<Material>();
    private MeshRenderer M;
    private bool gate = false;

    void Start()
    {
        M = transform.GetComponent<MeshRenderer>();
        M.material = matList[1];
        bool childFound = false;
        foreach(Transform child in transform)
        {
            if(child.name == "ParticleSystems")
            {
                particles = child.gameObject;
                particles.SetActive(false);
                childFound = true;
            }
        }
        if (!childFound)
            Debug.LogError(transform.name + " Particle system not found!");
    }

	void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !gate)
        {
            gate = true;
            if (particles != null)
            {
                particles.SetActive(false);
                particles.SetActive(true);
            }
            if(M != null)
            {
                M.material = matList[0];
            }
        }
    }
}
