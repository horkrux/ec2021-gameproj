using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;

// Too lazy to make EnemyController, so just dump it all in here

public class EnemyCharacter : Character
{
    bool firstTime = true;
    float _attackRange = 2.0f;
    float _movementRange = 10.0f;
    public int AttackDmg;
    public int Defense;
    //bool _isMoving = false;
    bool _isAttacking = false;
    bool _isTurning = false;
    bool _isCombatTurning = false;
    public EnemyOptions enemyOptions;
    //public CombatManager combatMan;
    public EnemyInfo enemyInfo;
    //private Animator animator;
    private GameObject _target = null;
    public Vector3 movePoint = Vector3.zero;
    NavMeshAgent enemyAgent;
    Rigidbody enemyRb;
    private xbot enemyAnimController;
    private Vector3 _startingPosAtTurn = Vector3.zero;
    public int randomStrengthValue;
    public int randomDexValue;

    public GameObject Target
    {
        get { return _target; }
        set { _target = value; }
    }

    public bool IsMoving
    {
        get { return _animator.GetBool("IsMoving"); }
        set { _animator.SetBool("IsMoving", value); }
    }

    public bool IsTurning
    {
        get { return _isTurning; }
        set { _isTurning = value; }
    }

    public bool IsCombatTurning
    {
        get { return _isCombatTurning; }
        set { _isCombatTurning = value; }
    }

    public bool IsAttacking
    {
        get { return _isAttacking; }
        set { _isAttacking = value; }
    }

    public Vector3 StartingPosAtTurn
    {
        get { return _startingPosAtTurn; }
        set { _startingPosAtTurn = value; }
    }

    public float AttackRange
    {
        get { return _attackRange; }
    }

    // Start is called before the first frame update
    void Start()
    {
        IsPlayer = false;
        _stats = gameObject.AddComponent<ChrStatModule>();
        _animator = gameObject.GetComponentInChildren<Animator>();
        enemyAgent = gameObject.GetComponent<NavMeshAgent>();
        enemyRb = gameObject.GetComponent<Rigidbody>();
        obstacle = gameObject.GetComponent<NavMeshObstacle>();
        enemyAnimController = gameObject.GetComponentInChildren<xbot>();

        InitHpMana(20, 0);
        _stats.Strength = randomStrengthValue;
        _stats.Dexterity = randomDexValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsAlive && enabled)
        {
            combatXP.mode = CombatXP.CombatXPMode.XP;
            combatXP.position = gameObject.transform.position;
            combatXP.gameObject.SetActive(true);
            obstacle.enabled = false;
            enemyAgent.enabled = false;
            gameObject.GetComponent<Collider>().enabled = false;
            enabled = false;
        }
    }

    private void FixedUpdate()
    {
        if (IsMoving)
        {
            obstacle.enabled = false;
            enemyAgent.enabled = true;
            Vector3 targetDir = movePoint - enemyRb.position;

            if (_isTurning)
            {
                Vector3 newDirection = Vector3.RotateTowards(enemyRb.transform.forward, targetDir, 0.1f, 0.0f);
                newDirection.y = 0;
                enemyRb.MoveRotation(Quaternion.LookRotation(newDirection));

                //PlayerChr.Rotating = false;
            }

            float distanceToTarget = Vector2.Distance(new Vector2(_target.transform.position.x, _target.transform.position.z), new Vector2(gameObject.transform.position.x, gameObject.transform.position.z));
            float distanceToStartingPos = Vector2.Distance(new Vector2(_startingPosAtTurn.x, _startingPosAtTurn.z), new Vector2(gameObject.transform.position.x, gameObject.transform.position.z));

            if (distanceToStartingPos >= _movementRange || distanceToTarget <= _attackRange || (!enemyAgent.pathPending && enemyAgent.remainingDistance <= 0.3f))//Vector3.Distance(playerRb.position, movePoint) < 0.001f)
            {
                enemyAgent.ResetPath();
                _isTurning = false;
                IsMoving = false;
                enemyAgent.enabled = false;
                obstacle.enabled = true;
                _isCombatTurning = true;

                if (distanceToTarget <= _attackRange)
                {
                    //Exec attack
                    combatMan.ExecAttackForEnemy(this);

                } 
                else
                {
                    combatMan.enemyCounter++;
                    combatMan.NextEnemyAction();
                }
            }
        }
        else if (_isCombatTurning)
        {
            Vector3 targetDir = _target.transform.position - enemyRb.position;

            Vector3 newDirection = Vector3.RotateTowards(enemyRb.transform.forward, targetDir, 0.5f, 0.0f);
            newDirection.y = 0;
            enemyRb.MoveRotation(Quaternion.LookRotation(newDirection));

            if (enemyRb.rotation == Quaternion.LookRotation(newDirection))
            {
                _isCombatTurning = false;
                _target = null;

                if (IsAttacking)
                {
                    _isAttacking = false;
                    combatMan.ExecAttackForEnemy(this);
                }
            }
        }
    }

    private void OnMouseDown()
    {
        //if (IsAlive && combatMan.EnemyInRange(gameObject) && !combatMan.HasPerformedAction)
        //{
        //    enemyOptions.Enemy = this;
        //    enemyOptions.gameObject.SetActive(true);
        //    combatMan.TurnPlayerCharacter(this);
        //}
    }

    private void OnMouseOver()
    {
        
        //enemyInfo.GetComponentsInChildren<TextMeshProUGUI>()[0].SetText("Attack: " + AttackDmg);
        //enemyInfo.GetComponentsInChildren<TextMeshProUGUI>()[1].SetText("Defense: " + Defense);
        //enemyInfo.gameObject.SetActive(true);
        //Debug.LogWarning("OVER");
    }

    private void OnMouseExit()
    {
        //enemyInfo.gameObject.SetActive(false);
        //Debug.LogWarning("EXIT");
    }

    public void Attack(PlayerCharacter enemy)
    {
        enemyAnimController.AttackTarget = enemy;
        ChrAnimator.SetBool("IsAttack", true);
    }
}
