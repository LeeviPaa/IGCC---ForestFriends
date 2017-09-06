using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour {
    internal bool collided = false;

    public bool PCollided
    {
        get
        {
            return collided;
        }
    }

	void OnCollisionStay(Collision other)
    {
        collided = true;
        Debug.Log("Collided");
    }
    void OnCollisionExit(Collision other)
    {
        collided = false;
    }
}
