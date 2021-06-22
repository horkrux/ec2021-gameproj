using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharUI : MonoBehaviour
{
    public PlayerCharacter player;
    TextMeshProUGUI healthText;
    TextMeshProUGUI manaText;
    Image charPic;
    Image HealthEmpty;
    Image HealthActual;
    Image ManaEmpty;
    Image ManaActual;

    // Start is called before the first frame update
    void Start()
    {
        healthText = GetComponentsInChildren<TextMeshProUGUI>()[0];
        manaText = GetComponentsInChildren<TextMeshProUGUI>()[1];
        charPic = GetComponentsInChildren<Image>()[0];
        HealthEmpty = GetComponentsInChildren<Image>()[1];
        HealthActual = GetComponentsInChildren<Image>()[2];
        ManaEmpty = GetComponentsInChildren<Image>()[3];
        ManaActual = GetComponentsInChildren<Image>()[4];
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "HP: " + player.CurrentHealth + "/" + player.MaxHealth;
        manaText.text = "MP: " + player.CurrentMana + "/" + player.MaxMana;

        //RectTransform healthActualRect = HealthActual.rectTransform;

        //healthActualRect.

    }
}
