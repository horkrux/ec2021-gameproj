using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private int _currentHealth;
    private int _maxHealth;
    private int _currentMana;
    private int _maxMana;

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

    // Start is called before the first frame update
    void Start()
    {
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
}
