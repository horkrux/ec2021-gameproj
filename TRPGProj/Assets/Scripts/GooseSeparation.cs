using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooseSeparation : MonoBehaviour
{
    List<Goose> _neighbours = new List<Goose>();
    public ref List<Goose> Neighbours
    {
        get { return ref _neighbours; }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GooseSeparation"))
        {
            _neighbours.Add(other.gameObject.GetComponentInParent<Goose>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("GooseSeparation"))
        {
            _neighbours.Remove(other.gameObject.GetComponentInParent<Goose>());
        }
    }
}
