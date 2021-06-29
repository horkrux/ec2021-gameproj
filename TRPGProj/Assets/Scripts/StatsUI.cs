using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DigitalRuby.Tween;

public class StatsUI : MonoBehaviour
{
    public PlayerCharacter player;

    public GameObject panelThing;
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

        System.Action<ITween<Vector3>> updatePanelPos = (t) =>
        {
            panelThing.transform.position = t.CurrentValue;
        };

        Vector3 finalPos = panelThing.transform.position;
        Vector3 startPos = new Vector3(panelThing.transform.position.x, panelThing.transform.position.y - 400.0f, panelThing.transform.position.z);

        panelThing.Tween("MovePanelUp", startPos, finalPos, 0.5f, TweenScaleFunctions.CubicEaseOut, updatePanelPos);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickClose()
    {
        Vector3 posy = panelThing.transform.position;

        System.Action<ITween<Vector3>> movePanelOut = (t) =>
        {
            panelThing.transform.position = t.CurrentValue;
        };

        System.Action<ITween<Vector3>> movePanelOutCompleted = (t) =>
        {
            gameObject.SetActive(false);
            //panelThing.transform.position = new Vector3(panelThing.transform.position.x, panelThing.transform.position.y + 400.0f, panelThing.transform.position.z);
            panelThing.transform.position = posy;
        };

        Vector3 startPos = panelThing.transform.position;
        Vector3 finalPos = new Vector3(panelThing.transform.position.x, panelThing.transform.position.y - 400.0f, panelThing.transform.position.z);

        panelThing.Tween("MovePanelDown", startPos, finalPos, 0.5f, TweenScaleFunctions.CubicEaseIn, movePanelOut, movePanelOutCompleted);

        
    }
}
