namespace MasujimaRyohei
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AI;

    public class MyGameAgentActor : MyGameBaseActor
    {
        protected NavMeshAgent agent;

        // Use this for initialization
        protected new void Start()
        {
            base.Start();

            if (GetComponent<NavMeshAgent>())
                agent = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}