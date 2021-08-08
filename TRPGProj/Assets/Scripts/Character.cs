using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    public CombatXP combatXP;
    public CombatManager combatMan;
    public GameOverUI gameOver;
    protected Animator _animator;
    protected ChrStatModule _stats;
    protected NavMeshAgent agent;
    protected NavMeshObstacle obstacle;
    private int _currentHealth;
    private int _maxHealth;
    private int _currentMana;
    private int _maxMana;
    private bool _isAlive = true;
    protected bool IsPlayer;
    //private int _hitChance; //this is only for testing and shouldn't exist

    public int CurrentHealth
    {
        get { return _currentHealth; }
    }

    public int MaxHealth
    {
        get { return _maxHealth; }
    }

    public int CurrentMana
    {
        get { return _currentMana; }
    }

    public int MaxMana
    {
        get { return _maxMana; }
    }

    public ChrStatModule Stats
    {
        get { return _stats; }
    }

    public Animator ChrAnimator
    {
        get { return _animator; }
    }

    public bool IsAlive
    {
        get { return _isAlive; }
        set { _isAlive = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        //_stats = gameObject.AddComponent<ChrStatModule>();
        //_animator = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    protected void InitHpMana(int health, int mana)
    {
        _maxHealth = health;
        _currentHealth = health;

        _maxMana = mana;
        _currentMana = mana;
    }

    public void AddHp(int hp)
    {
        _currentHealth += hp;

        if (_currentHealth > _maxHealth)
            _currentHealth = _maxHealth;
    }

    public void AddMana(int mana)
    {
        _currentMana += mana;

        if (_currentMana > _maxMana)
            _currentMana = _maxMana;
    }

    public void DecHp(int hp)
    {
        _currentHealth -= hp;

        if (_currentHealth < 0)
            _currentHealth = 0;
    }

    public void DecMana(int mana)
    {
        _currentMana -= mana;

        if (_currentMana < 0)
            _currentMana = 0;
    }

    public void Damage(Character attacker)
    {
        float baseChance = 1.0f;
        float hitChance = baseChance - _stats.Dexterity * 0.01f; //lol

        float rand = Random.Range(0.0f, 1 / hitChance);
        //rand = 2.0f;
        if (rand <= 1.0f)
        {
            int damage = Mathf.Max(1, 2 * attacker.Stats.Strength);
            //damage = 100;
            DecHp(damage);

            if (_currentHealth == 0)
            {
                _animator.SetBool("IsKilled", true);
                _isAlive = false;

                if (IsPlayer)
                    gameOver.gameObject.SetActive(true);

                if (combatMan.IsLastEnemyKilled())
                {
                    combatMan.InitEndCombat();
                }
            }
            else
            {
                _animator.SetBool("IsHit", true);
            }
        } 
        else
        {
            combatXP.mode = CombatXP.CombatXPMode.Miss;
            combatXP.position = attacker.gameObject.transform.position;
            combatXP.gameObject.SetActive(true);
        }
            
    }

    public void AttackFinished()
    {
        if (IsPlayer)
        {
            combatMan.enemyCounter++;
            combatMan.NextEnemyAction();
        } 
        else
        {

        }
        
    }
}
