using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using ItEx = MasujimaRyohei.iTweenExtensions;

public class Creature : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private GameObject target;

    [SerializeField]
    private GameObject sprite;
    [SerializeField]
    private Animator animator;

    public bool ShowSerchRange;
    [SerializeField]
    private GameObject serchRange;

    [SerializeField]
    private GameObject eatingEffect;

    [SerializeField]
    private GameObject exclamationEffect;

    [SerializeField]
    private GameObject excrement;

    [SerializeField]
    private float sensorDistance;

    private bool isJumpLink;

    private float gravity = 0;
    private float normalizedTime = 0;


    [SerializeField]
    private float durationForSetDestination = 3.0f;
    [SerializeField]
    private float durationForAttack = 3.0f;
    [SerializeField]
    private float durationForDamage = 3.0f;
    [SerializeField]
    private float durationForEat = 1.4f;

    [SerializeField]
    private int eatCount = 0;

    [SerializeField]
    private float wonderLevel = 3;

    EventManager eventManager;

    enum EState
    {
        WAIT,
        WONDER,
        CHASE,
        ATTACK,
        DAMAGE,
        EAT,
        EXCRETE,
        NONE
    }

    private EState state = EState.NONE;
    // Use this for initialization
    void Start()
    {
        // Enabled rigidbody.
        rb.isKinematic = true;

        if (animator == null)
            Debug.LogError("Animator not found!");

        agent.autoTraverseOffMeshLink = false;


        // First state.
        Wondering();


        // For serchable range animation.
        if (ShowSerchRange)
            iTween.ValueTo(gameObject,
                iTween.Hash(ItEx.from, 0, ItEx.to, 0.3f, ItEx.time, 3,
                ItEx.loopType, "pingpong", ItEx.onupdate, "UpdateSensorAnimation"));
    }
    void UpdateSensorAnimation(float opacity)
    {
        if (!serchRange)
        {
            Debug.LogError("Serch range not found!!");
            return;
        }

        // For position and local scale.
        serchRange.transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
        serchRange.transform.localScale = new Vector3(sensorDistance * 2, 0.01f, sensorDistance * 2);

        // For opacity.
        var c = serchRange.GetComponent<MeshRenderer>().material.color;
        c = new Color(c.r, c.g, c.b, opacity);
        serchRange.GetComponent<MeshRenderer>().material.color = c;

    }
    // Update is called once per frame
    void Update()
    {
        #region States
        switch (state)
        {
            case EState.WAIT:
                Waiting();

                break;
            case EState.WONDER:
                Wondering();
                break;
            case EState.CHASE:
                Chasing(target);
                break;
            case EState.ATTACK:
                Attacking();
                break;
            case EState.EAT:
                Eating();
                break;
            case EState.EXCRETE:
                Excreting();
                break;
            default:
                break;
        }
        #endregion

        // There state don't allow to transition to other states.
        if (state == EState.ATTACK ||
            state == EState.EAT ||
            state == EState.EXCRETE ||
            state == EState.WAIT ||
            state == EState.DAMAGE)
        {
            print("returrrrrrrrrrrn");
            return;
        }

        // LOL.
        if (eatCount >= 20)
            Excreting();

        // Serch nearest food.
        if (!target)
            target = SerchTag(gameObject, "Food");
        else
        {
            if (Vector3.Distance(transform.position, target.transform.position) < sensorDistance)
            {
                if (Vector3.Distance(transform.position, target.transform.position) < 2)
                    ContactTarget(target);
                else
                    Chasing(target);
            }
            else
            {
                // Target lost.
                target = null;
                Wondering();
            }
        }


        // For the flipping and animation, of sprite.
        if (agent.velocity.magnitude > 0)
        {
            print("moving");
            animator.SetBool("isWalk", true);

            if (agent.velocity.x > 0)
            {
                eatingEffect.transform.localPosition = new Vector3(1, 0.5f, 0);
                sprite.GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (agent.velocity.x < 0)
            {
                eatingEffect.transform.localPosition = new Vector3(-1, 0.5f, 0);
                sprite.GetComponent<SpriteRenderer>().flipX = false;
            }
        }
        else
        {
            print("freezing");
            animator.SetBool("isWalk", false);

        }



    }

    /// <summary>
    /// Serch nearest object with tag.
    /// </summary>
    /// <param name="nowObj"></param>
    /// <param name="tagName"></param>
    /// <returns></returns>
    GameObject SerchTag(GameObject nowObj, string tagName)
    {
        float tempDistance = 0;
        float nearestDistanse = 0;
        GameObject targetObject = null;

        foreach (GameObject obs in GameObject.FindGameObjectsWithTag(tagName))
        {
            tempDistance = Vector3.Distance(obs.transform.position, nowObj.transform.position);

            if (nearestDistanse == 0 || nearestDistanse > tempDistance)
            {
                nearestDistanse = tempDistance;
                targetObject = obs;
            }

        }
        return targetObject;
    }

    /// <summary>
    /// Excute when contact this object and target object.
    /// </summary>
    /// <param name="target"></param>
    private void ContactTarget(GameObject target)
    {
        switch (target.tag)
        {
            case "Food":
                Eating();
                break;
            case "Player":
                if (state == EState.ATTACK || state == EState.DAMAGE || state == EState.EAT || state == EState.EXCRETE)
                    break;
                Barking();
                break;
            case "Enemy":
                Attacking();
                break;
            default:
                break;
        }
    }

    void Eating()
    {
        if (state == EState.EAT)
            return;

        state = EState.EAT;

        StopAllCoroutines();
        StartCoroutine(EatCoroutine());

    }
    IEnumerator EatCoroutine()
    {
        print("Start eating!!");

        animator.SetBool("isEat", true);

        // Make smaller target food.
        iTween.ScaleTo(target, iTween.Hash("x", 0, "y", 0, "z", 0, "time", 10.0f));
        // Freeze position of target food.
        target.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        // Play se.
        MasujimaRyohei.AudioManager.PlaySE("LongEating");
        // Activate eating effect.
        eatingEffect.SetActive(true);
        // Wait for this creture to finish eating.
        yield return new WaitForSeconds(durationForEat);
        // Inactivate eating effect.
        eatingEffect.SetActive(false);

        animator.SetBool("isEat", false);

        // Increment number of eating.
        eatCount++;

        // Destroy target food.
        Destroy(target);

        // Next state is wonder.
        Wondering();
    }

    void Attacking()
    {
        if (state == EState.ATTACK)
            return;
        state = EState.ATTACK;

        StopAllCoroutines();
        StartCoroutine(AttackCoroutine());
    }
    IEnumerator AttackCoroutine()
    {
        print("Start attacking!!");

        yield return new WaitForSeconds(durationForAttack);

        print("Finished");

        //Destroy(target);
        Wondering();
    }

    void Dameging()
    {
        if (state == EState.DAMAGE)
            return;
        state = EState.DAMAGE;

        StopAllCoroutines();
        StartCoroutine(DamageCoroutine());
    }

    IEnumerator DamageCoroutine()
    {
        print("Received any damage!!");

        yield return new WaitForSeconds(durationForDamage);

        print("Revival!");
    }
    void Wondering()
    {
        if (state == EState.WONDER)
            return;
        state = EState.WONDER;

        StopAllCoroutines();
        StartCoroutine(WonderCoroutine());



    }
    IEnumerator WonderCoroutine()
    {
        while (true)
        {
            //print("Set new destination");
            agent.destination = new Vector3(transform.position.x + Random.Range(-wonderLevel, wonderLevel), 0, transform.position.z + Random.Range(-wonderLevel, wonderLevel));
            yield return new WaitForSeconds(durationForSetDestination);
        }
    }
    void Waiting()
    {
        if (state == EState.WAIT)
            return;
        state = EState.WAIT;

        animator.SetBool("isEat", false);
        animator.SetBool("isWalk", false);
        animator.SetBool("isJump", false);

        print("Waiting...");

    }

    void Barking()
    {
        print("Barking");
        //MasujimaRyohei.AudioManager.PlaySE("Barking");
    }
    void Chasing(GameObject target)
    {
        agent.destination = target.transform.position;
        OffMeshLinkData data;
        Vector3 startPosition;
        Vector3 endPosition;
        if (agent.isOnOffMeshLink)
        {
            data = agent.currentOffMeshLinkData;
            startPosition = data.startPos;
            endPosition = data.endPos;
            if (!isJumpLink)
            {
                animator.SetBool("isJump", true);
                isJumpLink = true;
            }

            gravity += -Physics.gravity.y * Time.deltaTime;
            normalizedTime += Time.deltaTime;
            normalizedTime = Mathf.Clamp(normalizedTime, 0, 1);
            var jumpHeight = 2.5f;
            var offset = new Vector3(0, (1 - normalizedTime) * jumpHeight, 0);
            transform.position = Vector3.Lerp(transform.position, endPosition, gravity * Time.deltaTime) + offset;

            if (transform.position == endPosition)
            {
                agent.CompleteOffMeshLink();
                normalizedTime = 0;
                gravity = 0;
                isJumpLink = false;
                animator.SetBool("isJump", false);
            }
        }


        if (state == EState.CHASE)
            return;
        state = EState.CHASE;

        StopAllCoroutines();
        StartCoroutine(ChaseCoroutine());
    }

    IEnumerator ChaseCoroutine()
    {
        var go = Instantiate(exclamationEffect, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
        Destroy(go);
    }
    void Excreting()
    {
        if (state == EState.EXCRETE)
            return;
        state = EState.EXCRETE;

        StopAllCoroutines();
        StartCoroutine(ExcreteCoroutine());
    }

    IEnumerator ExcreteCoroutine()
    {
        print("Start excreting!!");

        yield return new WaitForSeconds(3.0f);

        MasujimaRyohei.AudioManager.PlaySE("Excreting");
        Instantiate(excrement, transform.position, Quaternion.identity);
        eatCount = 0;
        print("Finished");
        state = EState.WONDER;
    }

    private void OnEnable()
    {
        eventManager = Toolbox.RegisterComponent<EventManager>();
        eventManager.GiveDogDestination += RecievePosition;
    }
    private void OnDisable()
    {
        eventManager.GiveDogDestination -= RecievePosition;
    }
    void RecievePosition(Vector3 pos)
    {
        Debug.LogWarning("Position recieved : " + pos);
        agent.SetDestination(pos);
    }
    public void switchAgentAndRigidbody()
    {
        agent.enabled = !agent.enabled;
        rb.useGravity = !rb.useGravity;
        rb.isKinematic = !rb.isKinematic;
    }
}

