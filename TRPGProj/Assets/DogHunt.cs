using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogHunt : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.gameObject.GetComponent<NavMeshAgent>().destination = animator.gameObject.transform.position + (animator.gameObject.GetComponent<Dog>().Target.transform.position - animator.gameObject.transform.position) * 2.0f;
        float timeToCatch = Vector3.Distance(animator.gameObject.transform.position, animator.gameObject.GetComponent<Dog>().Target.transform.position) / animator.gameObject.GetComponent<NavMeshAgent>().speed;

        Vector3 projectedPos = animator.gameObject.GetComponent<Dog>().Target.transform.position + animator.gameObject.GetComponent<Dog>().Target.GetComponent<NavMeshAgent>().velocity * timeToCatch;

        animator.gameObject.GetComponent<NavMeshAgent>().destination = projectedPos;

        //animator.gameObject.GetComponent<NavMeshAgent>().destination = animator.gameObject.GetComponent<Dog>().Target.transform.position + animator.gameObject.GetComponent<Dog>().Target.GetComponent<NavMeshAgent>().velocity * 2.0f;
        animator.gameObject.GetComponent<Dog>().debugMarker.transform.position = animator.gameObject.GetComponent<NavMeshAgent>().destination;
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
