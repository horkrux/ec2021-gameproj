using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyCharacter : Character
{
    public int AttackDmg;
    public int Defense;
    public EnemyInfo enemyInfo;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseOver()
    {
        enemyInfo.GetComponentsInChildren<TextMeshProUGUI>()[0].SetText("Attack: " + AttackDmg);
        enemyInfo.GetComponentsInChildren<TextMeshProUGUI>()[1].SetText("Defense: " + Defense);
        enemyInfo.gameObject.SetActive(true);
        //Debug.LogWarning("OVER");
    }

    private void OnMouseExit()
    {
        enemyInfo.gameObject.SetActive(false);
        //Debug.LogWarning("EXIT");
    }
}
