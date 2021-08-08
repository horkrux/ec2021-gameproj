using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class xbot : MonoBehaviour
{
    Character _attackTarget;

    public Character AttackTarget
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
        _attackTarget.Damage(gameObject.GetComponentInParent<Character>());
        gameObject.GetComponent<Animator>().SetBool("IsAttack", false);
    }

    public void DisableIsHit()
    {
        GetComponentInParent<Animator>().SetBool("IsHit", false);
    }

    public void AttackFinished()
    {
        GetComponentInParent<Character>().AttackFinished();
    }

    public void DeathAnimFinished()
    {

    }
}
