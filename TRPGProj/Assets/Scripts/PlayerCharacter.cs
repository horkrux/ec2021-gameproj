using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerCharacter : Character
{
    private bool _rotatingCombatTarget = false;
    private Vector3 oldPosition = Vector3.zero;
    private xbot playerAnimController;
    
    
    private Inventory _inventory;
    
    //private PlayerController Controller;
    private float _movementRange = 10.0f;
    private float _attackRange = 2.0f;
    private int _selectedTargetId = -1;
    private bool _moving = false;
    private bool _rotating = false;
    private GameObject _target = null;
    private GameObject _camFollowTarget;
    private bool _reCenterCam = false;
    public int SelectedTargetId
    {
        get { return _selectedTargetId; }
        set { _selectedTargetId = value; }
    }
    public bool Moving
    {
        get { return ChrAnimator.GetBool("IsMoving"); }
        set { ChrAnimator.SetBool("IsMoving", value); }
        //get { return _moving; }
        //set { _moving = value; }
    }

    public bool Rotating
    {
        get { return _rotating; }
        set { _rotating = value; }
    }

    public bool RotatingCombatTarget
    {
        get { return _rotatingCombatTarget; }
        set { _rotatingCombatTarget = value; }
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

    public float MovementRange
    {
        get { return _movementRange; }
        set { _movementRange = value; }
    }

    public float AttackRange
    {
        get { return _attackRange; }
        set { _attackRange = value; }
    }

    public GameObject CamFollowTarget
    {
        get { return _camFollowTarget; }
        set { _camFollowTarget = value; }
    }

    public bool ReCenterCam
    {
        get { return _reCenterCam; }
        set { _reCenterCam = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        IsPlayer = true;
        _stats = gameObject.AddComponent<ChrStatModule>();
        _animator = gameObject.GetComponentInChildren<Animator>();
        _inventory = gameObject.AddComponent<Inventory>();
        obstacle = gameObject.GetComponent<NavMeshObstacle>();
        //animator = gameObject.GetComponentInChildren<Animator>();
        playerAnimController = gameObject.GetComponentInChildren<xbot>();
        agent = gameObject.GetComponent<NavMeshAgent>();

        //hardcode some values

        Stats.Strength = 8;
        Stats.Dexterity = 10;
        Stats.Balance = 10;
        Stats.Intelligence = 10;

        InitHpMana(100, 100);
        //DecHp(50);
        //DecMana(50);
        //Controller = gameObject.transform.parent.gameObject.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsAlive && enabled)
        {
            enabled = false;
        }
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
                ChrAnimator.SetBool("IsPickup", true);
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
            if (_target == other.gameObject)
            {
                //playerAnimController.AttackTarget = _target.GetComponent<TestTarget>();
                agent.ResetPath();
                _target = null;
                ChrAnimator.SetBool("IsAttack", true);
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
                ChrAnimator.SetBool("IsPickup", true);
                other.gameObject.GetComponent<Loot>().pickUp(Inventory);
                _selectedTargetId = -1;
                Moving = false;
                _rotating = false;
                _target = null;
            }

        }
        else if (other.gameObject.CompareTag("Talk"))
        {
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
            if (_target == other.gameObject)
            {
                agent.ResetPath();
                //playerAnimController.AttackTarget = _target.GetComponent<TestTarget>();
                _target = null;
                ChrAnimator.SetBool("IsAttack", true);
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
                    other.gameObject.GetComponent<RandomEncounterZone>().SpawnUnits();
                    combatMan.StartCombat(other.gameObject.GetComponent<RandomEncounterZone>());
                    //gameObject.GetComponentInParent<PlayerController>().combatMan.IsCombat = true;
                    other.gameObject.GetComponent<Collider>().enabled = false;
                }

            }
        }
    }

    public void Attack(EnemyCharacter enemy)
    {
        playerAnimController.AttackTarget = enemy;
        ChrAnimator.SetBool("IsAttack", true);
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
