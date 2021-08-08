using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EnemyOptions : MonoBehaviour
{
    public CombatManager combatMan;
    EnemyCharacter _enemy;
    public EnemyCharacter Enemy
    {
        get { return _enemy; }
        set { _enemy = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        TextMeshProUGUI hitChanceText = GetComponentsInChildren<TextMeshProUGUI>()[0];
        Image healthEmptyImage = GetComponentsInChildren<Image>()[2];
        Image healthDamageImage = GetComponentsInChildren<Image>()[3];
        Image healthCurrentImage = GetComponentsInChildren<Image>()[4];

        hitChanceText.text = "Hit Chance: " + ((1.0f - _enemy.Stats.Dexterity * 0.01f) * 100) + "%";

        RectTransform rtDamage = healthDamageImage.rectTransform;
        rtDamage.sizeDelta = new Vector2(200 * (1.0f * _enemy.CurrentHealth / _enemy.MaxHealth), 20);

        RectTransform rtCurrent = healthCurrentImage.rectTransform;
        rtCurrent.sizeDelta = new Vector2(200 * (1.0f * Mathf.Max(0, _enemy.CurrentHealth - Mathf.Max(1, 2 * combatMan.playerUnit.Stats.Strength)) / _enemy.MaxHealth), 20);


    }

    public void OnClickCancel()
    {
        gameObject.SetActive(false);
        _enemy = null;
    }

    public void OnClickAttack()
    {
        combatMan.HasPerformedAction = true;
        combatMan.ExecAttackForPlayer(_enemy);
        gameObject.SetActive(false);
        _enemy = null;
    }
}
