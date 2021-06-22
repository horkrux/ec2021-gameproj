using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsUI : MonoBehaviour
{
    public PlayerCharacter player;

    TextMeshProUGUI strengthText;
    TextMeshProUGUI dexterityText;
    TextMeshProUGUI intelligenceText;
    TextMeshProUGUI balanceText;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnEnable()
    {
        if (strengthText == null)
            strengthText = GetComponentsInChildren<TextMeshProUGUI>()[5];
        if (dexterityText == null)
            dexterityText = GetComponentsInChildren<TextMeshProUGUI>()[6];
        if (intelligenceText == null)
            intelligenceText = GetComponentsInChildren<TextMeshProUGUI>()[7];
        if (balanceText == null)
            balanceText = GetComponentsInChildren<TextMeshProUGUI>()[8];

        strengthText.text = player.GetStrength().ToString();
        dexterityText.text = player.GetDexterity().ToString();
        intelligenceText.text = player.GetIntelligence().ToString();
        balanceText.text = player.GetBalance().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickClose()
    {
        gameObject.SetActive(false);
    }
}
