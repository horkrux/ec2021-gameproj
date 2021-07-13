using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    public GameObject debugMarker;
    public GooseManager gooseMan;
    Goose _target;
    public Goose Target
    {
        get { return _target; }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        _target = gooseMan.getClosestGoose(gameObject.transform.position);

        if (Input.GetButtonDown("Dog Wander"))
        {
            gameObject.GetComponent<Animator>().SetBool("IsSeeking", false);
            gameObject.GetComponent<Animator>().SetBool("IsHunting", false);
        } 
        else if (Input.GetButtonDown("Dog Seek"))
        {
            gameObject.GetComponent<Animator>().SetBool("IsHunting", false);
            gameObject.GetComponent<Animator>().SetBool("IsSeeking", true);
        } 
        else if (Input.GetButtonDown("Dog Hunt"))
        {
            gameObject.GetComponent<Animator>().SetBool("IsHunting", true);
            gameObject.GetComponent<Animator>().SetBool("IsSeeking", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GooseDanger"))
        {
            other.gameObject.GetComponentInParent<Goose>().Scare(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("GooseDanger"))
        {
            other.gameObject.GetComponentInParent<Goose>().Scare(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("GooseDanger"))
        {
            other.gameObject.GetComponentInParent<Goose>().Scare(true);
        }
    }
}
