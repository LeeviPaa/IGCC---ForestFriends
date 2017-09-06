using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour {
    private Camera mainCamera;
    private GameObject myCollider;
    private GameObject myMesh;
    private Vector3 MovementInputVector = Vector3.zero;
    private RaycastHit hit;

    //The object that the player is looking at (Transform.LookAt requires a transform as a target)
    public Transform lookTarget;
    public float MovementSpeed = 5;
	// Use this for initialization
	void Start () {

        //check child gameobjects for the mesh object
		foreach(Transform child in transform)
        {
            if(child.name == "Mesh")
            {
                Debug.Log("Mesh found");
                myMesh = child.gameObject;   
            }
        }

        //get reference to the main camera
        Camera[] CamArr = new Camera[5];
        int Count = Camera.GetAllCameras(CamArr);
        if (Count > 0)
            mainCamera = CamArr[0];

    }
	
	// Update is called once per frame
	void Update () {
        rotation();
        movement();
        
	}

    /// <summary>
    /// This method is called every frame to move the character
    /// </summary>
    void movement()
    {
        //Movement
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            MovementInputVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * MovementSpeed * Time.deltaTime;
            Ray moveRay = new Ray(transform.position, MovementInputVector);
            if (!Physics.SphereCast(moveRay, 0.5f, out hit, MovementInputVector.magnitude))
            {
                transform.Translate(MovementInputVector);
            }
            else
            {
                if (hit.collider.gameObject.tag == "Obstacle")
                {
                    Vector3 hitMoveVector = Vector3.MoveTowards(transform.position, hit.point, MovementSpeed);
                    transform.position = hitMoveVector;

                    //Wall sliding
                    //hitMoveVector = hit.point + (MovementInputVector - hit.point) - hit.normal * Vector3.Dot((MovementInputVector - hit.point), hit.normal);
                    transform.Translate(hitMoveVector);
                }
            }
        }
    }

    /// <summary>
    /// this method is called every frame to rotate the player character
    /// </summary>
    void rotation()
    {
        //Rotation
        Vector3 resolution = new Vector2(mainCamera.pixelWidth, mainCamera.pixelHeight);
        resolution /= 2;
        Vector2 screenPointNormalized = Input.mousePosition - resolution;

        lookTarget.position = (transform.position + new Vector3(screenPointNormalized.x, transform.position.y, screenPointNormalized.y)/25);
        myMesh.transform.LookAt(lookTarget);
    }
}
