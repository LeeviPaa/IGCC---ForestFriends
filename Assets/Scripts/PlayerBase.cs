using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour {
    private Camera mainCamera;
    private GameObject myCollider;
    private GameObject myMesh;
    private Vector3 MovementInputVector = Vector3.zero;
    private RaycastHit hit;
    private Animator LowerSpriteAnimator;
    private SpriteRenderer LowerSpriteRender;
    private Animator UpperSpriteAnimator;
    private SpriteRenderer UpperSpriteRender;
    private CapsuleCollider CharacterCollider;

    //Animation
    private bool Moving = false;

    //The object that the player is looking at (Transform.LookAt requires a transform as a target)
    public Transform lookTarget;

    public LayerMask WalkLayer;
    public LayerMask CollisionLayer;
    public GameObject UpperSprite;
    public GameObject LowerSprite;
    public float WalkingSpeed = 5;
    public float RunningSpeed = 10;
    public float stepHeight = 0.1f;
    private float MovementSpeed = 5;
	// Use this for initialization
	void Start () {
        MovementSpeed = WalkingSpeed;
        //check child gameobjects for the mesh object
		foreach(Transform child in transform)
        {
            if(child.name == "Mesh")
            {
                Debug.Log("Mesh found");
                myMesh = child.gameObject;   
            }
        }
        if (LowerSprite.GetComponent<Animator>() && UpperSprite.GetComponent<Animator>())
        {
            LowerSpriteAnimator = LowerSprite.GetComponent<Animator>();
            UpperSpriteAnimator = UpperSprite.GetComponent<Animator>();
            Debug.Log("Animator found!");
        }
        else
        {
            Debug.LogError("Animator not found!");
        }

        if (LowerSprite.GetComponent<SpriteRenderer>() && UpperSprite.GetComponent<SpriteRenderer>())
        {
            LowerSpriteRender = LowerSprite.GetComponent<SpriteRenderer>();
            UpperSpriteRender = UpperSprite.GetComponent<SpriteRenderer>();
            Debug.Log("Sprite renderer found");
        }
        else
        {
            Debug.LogError("Sprite renderer not found");
        }

        CharacterCollider = transform.GetComponent<CapsuleCollider>();
        //get reference to the main camera
        mainCamera = Camera.main;

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

        //Running
        if (Input.GetButton("Fire3"))
        {
            MovementSpeed = RunningSpeed;
        }
        else
            MovementSpeed = WalkingSpeed;

        //Movement
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            //Animation variables
            Moving = true;
            LowerSpriteAnimator.SetBool("Walking", true);
            UpperSpriteAnimator.SetBool("Walking", true);

            //Flip the sprite according to move direction
            if (Input.GetAxis("Horizontal") < 0)
            {
                LowerSpriteRender.flipX = false;
                UpperSpriteRender.flipX = false;
            }
            else if(Input.GetAxis("Horizontal") > 0)
            {
                LowerSpriteRender.flipX = true;
                UpperSpriteRender.flipX = true;
            }

            MovementInputVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * MovementSpeed * Time.deltaTime;
            MovementInputVector = transform.TransformDirection(MovementInputVector);
            //Cliff detection
            Ray moveRay = new Ray(new Vector3(
                transform.position.x, 
                transform.position.y - (CharacterCollider.height / 2)+stepHeight,
                transform.position.z) + MovementInputVector, 
                -transform.up);

            //move if there is ground below where we are moving
            if (Physics.SphereCast(moveRay,0.3f, out hit, stepHeight * 2, WalkLayer, QueryTriggerInteraction.Ignore))
            {
                transform.position = new Vector3(
                    transform.position.x,
                    hit.point.y+(CharacterCollider.height/2),
                    transform.position.z);


                moveRay = new Ray(transform.position, MovementInputVector*2);
                Debug.DrawRay(transform.position, MovementInputVector, Color.red, 0.25f);

                Vector3 capsulePoint1 = MovementInputVector + transform.position + CharacterCollider.center + (Vector3.up * CharacterCollider.height / 2);
                Vector3 capsulePoint2 = MovementInputVector + transform.position + CharacterCollider.center - (Vector3.up * CharacterCollider.height / 6);
                //!Physics.CapsuleCast(capsulePoint1, capsulePoint2, CharacterCollider.radius*1.1f, transform.TransformDirection( MovementInputVector), MovementInputVector.magnitude, CollisionLayer, QueryTriggerInteraction.Ignore)

                if (!Physics.CheckCapsule(capsulePoint1, capsulePoint2, CharacterCollider.radius * 0.95f, CollisionLayer, QueryTriggerInteraction.Ignore))
                {
                    transform.Translate(MovementInputVector, Space.World);
                }
                else
                {
                    if (hit.collider.gameObject.tag == "Obstacle")
                    {
                        //Vector3 hitMoveVector = Vector3.MoveTowards(transform.position, hit.point, MovementSpeed);
                        ////transform.position = hitMoveVector;

                        ////Wall sliding
                        //hitMoveVector = hit.point + (MovementInputVector - hit.point) - hit.normal * Vector3.Dot((MovementInputVector - hit.point), hit.normal);
                        //transform.Translate(hitMoveVector);
                    }
                }
            }
        }
        else
        {
            //Animation variables
            Moving = false;
            LowerSpriteAnimator.SetBool("Walking", false);
            UpperSpriteAnimator.SetBool("Walking", false);
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

        lookTarget.localPosition = (new Vector3(screenPointNormalized.x, 0, screenPointNormalized.y)/25);
        myMesh.transform.LookAt(lookTarget);
    }
}
