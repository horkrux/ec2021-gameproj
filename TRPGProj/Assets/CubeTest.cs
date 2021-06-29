using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisableIsDamaged()
    {
        gameObject.GetComponent<Animator>().SetBool("IsDamaged", false);
    }
}
