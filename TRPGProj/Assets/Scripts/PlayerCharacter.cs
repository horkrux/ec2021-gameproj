using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerCharacter : Character
{
    private Vector3 oldPosition = Vector3.zero;
    private xbot playerAnimController;
    private Animator animator;
    private ChrStatModule Stats;
    private Inventory _inventory;
    private NavMeshAgent agent;
    //private PlayerController Controller;
    private int _selectedTargetId = -1;
    private bool _moving = false;
    private bool _rotating = false;
    private GameObject _target = null;
    public int SelectedTargetId
    {
        get { return _selectedTargetId; }
        set { _selectedTargetId = value; }
    }
    public bool Moving
    {
        get { return animator.GetBool("IsMoving"); }
        set { animator.SetBool("IsMoving", value); }
        //get { return _moving; }
        //set { _moving = value; }
    }

    public bool Rotating
    {
        get { return _rotating; }
        set { _rotating = value; }
    }

    public GameObject Target
    {
        get { return _target; }
        set { _target = value; }
    }

    public Inventory Inventory
    {
        get { return _inventory; }
    }

    // Start is called before the first frame update
    void Start()
    {
        Stats = gameObject.AddComponent<ChrStatModule>();
        _inventory = gameObject.AddComponent<Inventory>();
        animator = gameObject.GetComponentInChildren<Animator>();
        playerAnimController = gameObject.GetComponentInChildren<xbot>();
        agent = gameObject.GetComponent<NavMeshAgent>();

        //hardcode some values

        Stats.Strength = 1;
        Stats.Dexterity = 1;
        Stats.Balance = 1;
        Stats.Intelligence = 1;

        InitHpMana(100, 100);
        DecHp(50);
        DecMana(50);
        //Controller = gameObject.transform.parent.gameObject.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Loot"))
        {
            if (_target == collision.gameObject)
            {
                agent.ResetPath();
                _selectedTargetId = -1;
                Moving = false;
                _rotating = false;
                _target = null;
            }
            
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Loot"))
        {
            agent.ResetPath();
            _selectedTargetId = -1;
            Moving = false;
            _rotating = false;
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Loot"))
        {
            if (_target == other.gameObject)
            {
                animator.SetBool("IsPickup", true);
                other.gameObject.GetComponent<Loot>().pickUp(Inventory);
                agent.ResetPath();
                _selectedTargetId = -1;
                Moving = false;
                _rotating = false;
                _target = null;
            }
            
        }
        else if (other.gameObject.CompareTag("Talk"))
        {
            //TODO: this is called a million times holy hell
            if (_target == other.gameObject)
            {
                _target = null;
                other.gameObject.GetComponent<Talk>().Init();
                agent.ResetPath();
                _selectedTargetId = -1;
                Moving = false;
                _rotating = false;
            }
        }
        else if (other.gameObject.CompareTag("Attack"))
        {
            //TODO: this is called a million times holy hell
            if (_target == other.gameObject)
            {
                playerAnimController.AttackTarget = _target.GetComponent<TestTarget>();
                agent.ResetPath();
                _target = null;
                animator.SetBool("IsAttack", true);
                _selectedTargetId = -1;
                Moving = false;
                _rotating = false;
            }
        }
        else if (other.gameObject.CompareTag("GooseDanger"))
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
        if (other.gameObject.CompareTag("Loot"))
        {
            if (_target == other.gameObject)
            {
                agent.ResetPath();
                animator.SetBool("IsPickup", true);
                other.gameObject.GetComponent<Loot>().pickUp(Inventory);
                _selectedTargetId = -1;
                Moving = false;
                _rotating = false;
                _target = null;
            }

        }
        else if (other.gameObject.CompareTag("Talk"))
        {
            //TODO: this is called a million times holy hell
            if (_target == other.gameObject)
            {
                agent.ResetPath();
                _target = null;
                other.gameObject.GetComponent<Talk>().Init();
                _selectedTargetId = -1;
                Moving = false;
                _rotating = false;
            }
        }
        else if (other.gameObject.CompareTag("Attack"))
        {
            //TODO: this is called a million times holy hell
            if (_target == other.gameObject)
            {
                agent.ResetPath();
                playerAnimController.AttackTarget = _target.GetComponent<TestTarget>();
                _target = null;
                animator.SetBool("IsAttack", true);
                _selectedTargetId = -1;
                Moving = false;
                _rotating = false;
            }
        }
        else if (other.gameObject.CompareTag("GooseDanger"))
        {
            other.gameObject.GetComponentInParent<Goose>().Scare(true);
        }

        if (other.gameObject.CompareTag("RandomEncounter"))
        {
            if (oldPosition != gameObject.transform.position)
            {
                oldPosition = gameObject.transform.position;
                float rand = Random.Range(0.0f, 1 / other.gameObject.GetComponent<RandomEncounterZone>().encounterChance);

                if (rand <= 1.0f)
                {
                    gameObject.GetComponentInParent<PlayerController>().combatMan.IsCombat = true;
                    other.gameObject.GetComponent<RandomEncounterZone>().SpawnUnits();
                    other.gameObject.GetComponent<Collider>().enabled = false;
                }

            }
        }
    }

    public int GetStrength()
    {
        return Stats.Strength;
    }

    public int GetDexterity()
    {
        return Stats.Dexterity;
    }

    public int GetIntelligence()
    {
        return Stats.Intelligence;
    }

    public int GetBalance()
    {
        return Stats.Balance;
    }
}
