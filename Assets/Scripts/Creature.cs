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
    private GameObject target;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

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

    [SerializeField]
    private float durationForSetDestination = 3.0f;
    [SerializeField]
    private float durationForAttack = 3.0f;
    [SerializeField]
    private float durationForDamage = 3.0f;

    [SerializeField]
    private int eatCount = 0;

    [SerializeField]
    private float wonderLevel = 3;


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
        Wondering();


        if(ShowSerchRange)
        iTween.ValueTo(gameObject,
            iTween.Hash(ItEx.from, 0, ItEx.to, 0.3f,ItEx.time,3,
            ItEx.loopType, "pingpong", ItEx.onupdate, "UpdateSensorAnimation"));
    }
    void UpdateSensorAnimation(float opacity)
    {
        if (serchRange)
            serchRange.transform.position = new Vector3(transform.position.x, transform.position.y-1, transform.position.z);
        print(opacity);
        serchRange.transform.localScale = new Vector3(sensorDistance * 2, 0.01f, sensorDistance * 2);
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
                print("WAIT");
                Waiting();
                break;
            case EState.WONDER:
                print("WONDER");
                Wondering();
                break;
            case EState.CHASE:
                print("CHASE");
                Chasing(target);
                break;
            case EState.ATTACK:
                print("ATTACK");
                Attacking();
                break;
            case EState.EAT:
                print("EAT");
                Eating();
                break;
            case EState.EXCRETE:
                print("EXCRETE");
                Excreting();
                break;
            default:
                break;
        }
        #endregion

        if (state == EState.ATTACK ||
            state == EState.EAT ||
            state == EState.EXCRETE ||
            state == EState.WAIT ||
            state == EState.DAMAGE)
            return;

        if (eatCount >= 20)
            Excreting();

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

        if (agent.velocity.x > 0)
        {
            eatingEffect.transform.localPosition = new Vector3(1, 0.5f, 0);
            spriteRenderer.flipX = true;
        }
        else if (agent.velocity.x < 0)
        {
            eatingEffect.transform.localPosition = new Vector3(-1, 0.5f, 0);
            spriteRenderer.flipX = false;
        }
       

    }
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
    private void ContactTarget(GameObject target)
    {
        print(target.tag);
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

        iTween.ScaleTo(target, iTween.Hash("x", 0, "y", 0, "z", 0, "time", 10.0f));
        target.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        MasujimaRyohei.AudioManager.PlaySE("LongEating");
        eatingEffect.SetActive(true);
        yield return new WaitForSeconds(1.4f);
        eatingEffect.SetActive(false);
        eatCount++;
        print(eatCount);

        print("Finished");

        Destroy(target);
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
            print("Set new destination");
            agent.destination = new Vector3(transform.position.x + Random.Range(-wonderLevel, wonderLevel), 0, transform.position.z + Random.Range(-wonderLevel, wonderLevel));
            yield return new WaitForSeconds(durationForSetDestination);
        }
    }
    void Waiting()
    {
        if (state == EState.WAIT)
            return;
        state = EState.WAIT;

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

    private void OnGUI()
    {

    }
}

