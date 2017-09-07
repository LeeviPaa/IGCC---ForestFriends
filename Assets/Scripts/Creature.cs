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
    private float sensorDistance;

    [SerializeField]
    private float durationForSetDestination = 3.0f;
    [SerializeField]
    private float durationForAttack = 3.0f;

    enum EState
    {
        WAIT,
        WONDER,
        CHASE,
        ATTACK,
        EAT,
        EXCRETE,
        NONE
    }

    private EState state = EState.NONE;
    private IEnumerator coroutine;
    // Use this for initialization
    void Start()
    {
        Waiting();
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

        if (state == EState.ATTACK || state == EState.EAT || state == EState.EXCRETE || state == EState.WAIT)
            return;

        if (!target)
            target = serchTag(gameObject, "Character");
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
        switch (target.name)
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

        yield return new WaitForSeconds(3);

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
    void Wondering()
    {
        if (state == EState.WONDER)
            return ;
        state = EState.WONDER;

        StopAllCoroutines();
        StartCoroutine(WonderCoroutine());
     


    }
    IEnumerator WonderCoroutine()
    {
        while (true)
        {
            print("Set new destination");
            agent.destination = new Vector3(Random.Range(-12, 12), 0, Random.Range(-12, 12));
            yield return new WaitForSeconds(durationForSetDestination);
        } }
    void Waiting()
    {
        if (state == EState.WAIT)
            return;
        state = EState.WAIT;

        print("Waiting...");
        
    }
    void Chasing(GameObject target)
    {
        //if (state == EState.CHASE)
        //    return;
        //state = EState.CHASE;
        agent.destination = target.transform.position;
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

        print("Finished");
        state = EState.WONDER;
    }
}
