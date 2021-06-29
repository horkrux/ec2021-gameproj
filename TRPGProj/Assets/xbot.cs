using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class xbot : MonoBehaviour
{
    TestTarget _attackTarget;

    public TestTarget AttackTarget
    {
        get { return _attackTarget; }
        set { _attackTarget = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage()
    {
        _attackTarget.Damage();
        gameObject.GetComponent<Animator>().SetBool("IsAttack", false);
    }
}
