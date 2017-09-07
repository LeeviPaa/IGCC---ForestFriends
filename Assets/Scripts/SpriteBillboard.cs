using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBillboard : MonoBehaviour {

	void Update () {
        transform.forward = Camera.main.transform.forward;
	}
}
