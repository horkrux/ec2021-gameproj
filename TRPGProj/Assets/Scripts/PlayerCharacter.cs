using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
    private ChrStatModule Stats;
    private Inventory _inventory;
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
        get { return _moving; }
        set { _moving = value; }
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
                _selectedTargetId = -1;
                _moving = false;
                _rotating = false;
                _target = null;
            }
            
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Loot"))
        {
            _selectedTargetId = -1;
            _moving = false;
            _rotating = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Loot"))
        {
            if (_target == other.gameObject)
            {
                other.gameObject.GetComponent<Loot>().pickUp(Inventory);
                _selectedTargetId = -1;
                _moving = false;
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
                _selectedTargetId = -1;
                _moving = false;
                _rotating = false;
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
