using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GooseNormal : StateMachineBehaviour
{
    GameObject goose;
    NavMeshAgent gooseAgent;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (gooseAgent == null)
            gooseAgent = animator.gameObject.GetComponent<NavMeshAgent>();
        if (goose == null)
            goose = animator.gameObject;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 separation = Vector3.zero;
        Vector3 position = animator.gameObject.transform.position;

        //if (animator.gameObject.GetComponentInChildren<GooseSeparation>().Neighbours.Count > 0)
        //{
        //    string debugshit = "number of neighbours: " + animator.gameObject.GetComponentInChildren<GooseSeparation>().Neighbours.Count;
        //    Debug.LogWarning(debugshit);
        //}
        

        foreach (Goose neighbour in animator.gameObject.GetComponentInChildren<GooseSeparation>().Neighbours)
        {
            separation -= (neighbour.gameObject.transform.position - position);
        }

        animator.gameObject.GetComponent<NavMeshAgent>().destination = position + separation.normalized + 0.05f * (animator.gameObject.GetComponentInParent<GooseManager>().AveragePos - position).normalized;
        //animator.gameObject.GetComponentInParent<GooseManager>().AveragePos
        //if ()
        //gooseAgent.destination = goose.transform.position - 
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
