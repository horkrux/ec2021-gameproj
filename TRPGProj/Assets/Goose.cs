using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goose : MonoBehaviour
{
    public PlayerCharacter player;
    List<Goose> neighbours = new List<Goose>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Scare(bool val)
    {
        gameObject.GetComponent<Animator>().SetBool("IsFleeing", val);
    }
}
