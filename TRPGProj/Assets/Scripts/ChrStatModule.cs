using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChrStatModule : MonoBehaviour
{
    private int _strength = 0;
    private int _dexterity = 0;
    private int _intelligence = 0;
    private int _balance = 0;

    public int Strength
    {
        get { return _strength; }
        set { _strength = value; }
    }

    public int Dexterity
    {
        get { return _dexterity; }
        set { _dexterity = value; }
    }

    public int Intelligence
    {
        get { return _intelligence; }
        set { _intelligence = value; }
    }

    public int Balance
    {
        get { return _balance; }
        set { _balance = value; }
    }
}
