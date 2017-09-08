using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Creature : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;

    [SerializeField]
    private GameObject target;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

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
        state = EState.NONE;
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

        if (state == EState.ATTACK  || 
            state == EState.EAT     || 
            state == EState.EXCRETE || 
            state == EState.WAIT    || 
            state == EState.DAMAGE)
            return;

        if (eatCount >= 10)
            Excreting();

        if (!target)
            target = serchTag(gameObject, "Food");
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
    GameObject serchTag(GameObject nowObj, string tagName)
    {
        float tmpDis = 0;
        float nearDis = 0;
        GameObject targetObj = null;

        foreach (GameObject obs in GameObject.FindGameObjectsWithTag(tagName))
        {
            tmpDis = Vector3.Distance(obs.transform.position, nowObj.transform.position);

            if (nearDis == 0 || nearDis > tmpDis)
            {
                nearDis = tmpDis;
                targetObj = obs;
            }

        }
        return targetObj;
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
}

