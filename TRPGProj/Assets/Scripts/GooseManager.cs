using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GooseManager : MonoBehaviour
{
    public int numberOfGeese;
    Vector3 swarmPosition;
    public Goose goosePrefab;
    List<Goose> gooseSwarm = new List<Goose>(); //Lol
    Vector3 _averagePos;
    public Vector3 AveragePos
    {
        get { return _averagePos; }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numberOfGeese; i++)
        {
            gooseSwarm.Add(Instantiate(goosePrefab, gameObject.transform));//, gameObject.transform));
            gooseSwarm[i].gameObject.SetActive(true);

            Vector3 point;

            RandomPoint(gameObject.transform.position, 0.3f, out point);

            gooseSwarm[i].gameObject.GetComponent<NavMeshAgent>().destination = point;
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        //calc average position of swarm
        Vector3 averagePos = Vector3.zero;

        foreach(Goose goose in gooseSwarm)
        {
            averagePos += goose.transform.position;
        }

        averagePos /= numberOfGeese;

        _averagePos = averagePos;
    }

    public Goose getClosestGoose(Vector3 pos)
    {
        Goose resultGoose = null;

        foreach(Goose goose in gooseSwarm)
        {
            if (resultGoose == null)
            {
                resultGoose = goose;
                continue;
            }

            if (Vector3.Distance(goose.transform.position, pos) < Vector3.Distance(resultGoose.transform.position, pos))
            {
                resultGoose = goose;
            }
                
        }

        return resultGoose;
    }

    //Source: Unity Technologies, 2020, Unity Docs "NavMesh.SamplePosition"
    //URL: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }
}
