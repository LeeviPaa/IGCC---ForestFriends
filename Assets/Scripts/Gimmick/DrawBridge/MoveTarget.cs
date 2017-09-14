using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTarget : MonoBehaviour
{
    [SerializeField]
    Transform target = null;

    void Start()
    {
        var agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(target.localPosition);

        // OffMeshLinkに乗った際のアクション
        StartCoroutine(MoveNormalSpeed(agent));
    }

    IEnumerator MoveNormalSpeed(NavMeshAgent agent)
    {
        // OffMeshLinkによる移動を禁止
        agent.autoTraverseOffMeshLink = false;

        while (true)
        {
            // OffmeshLinkに乗るまで普通に移動
            yield return new WaitWhile(() => agent.isOnOffMeshLink == false);

            // OffMeshLinkに乗ったので、NavmeshAgentによる移動を止めて、
            // OffMeshLinkの終わりまでNavmeshAgent.speedと同じ速度で移動
            agent.Stop();
            yield return new WaitWhile(() =>
            {
                transform.localPosition = Vector3.MoveTowards(
                                            transform.localPosition,
                                            agent.currentOffMeshLinkData.endPos, agent.speed * Time.deltaTime);

                return Vector3.Distance(transform.localPosition, agent.currentOffMeshLinkData.endPos) > 0.1f;
            });

            // NavmeshAgentを到達した事にして、Navmeshを再開
            agent.CompleteOffMeshLink();
            agent.Resume();
        }
    }
}
