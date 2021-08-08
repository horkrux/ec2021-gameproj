using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using DigitalRuby.Tween;

public class CombatUI : MonoBehaviour
{
    UnityEvent StartEvent;

    public CombatManager combatMan;
    public GameObject panelThing;
    public enum CombatUIMode
    {
        PlayerTurn,
        EnemyTurn,
        Start,
        Victory
    }
    public CombatUIMode mode;

    // Start is called before the first frame update
    void Start()
    {
        if (StartEvent == null)
            StartEvent = new UnityEvent();

        StartEvent.AddListener(combatMan.CombatUIContinue);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        if (mode == CombatUIMode.Start)
        {
            gameObject.GetComponentsInChildren<TextMeshProUGUI>()[0].enabled = true;
            gameObject.GetComponentsInChildren<TextMeshProUGUI>()[1].enabled = false;
            gameObject.GetComponentsInChildren<TextMeshProUGUI>()[2].enabled = false;
            gameObject.GetComponentsInChildren<TextMeshProUGUI>()[3].enabled = false;
        }
        else if (mode == CombatUIMode.PlayerTurn)
        {
            gameObject.GetComponentsInChildren<TextMeshProUGUI>()[0].enabled = false;
            gameObject.GetComponentsInChildren<TextMeshProUGUI>()[1].enabled = true;
            gameObject.GetComponentsInChildren<TextMeshProUGUI>()[2].enabled = false;
            gameObject.GetComponentsInChildren<TextMeshProUGUI>()[3].enabled = false;
        }
        else if (mode == CombatUIMode.EnemyTurn)
        {
            gameObject.GetComponentsInChildren<TextMeshProUGUI>()[0].enabled = false;
            gameObject.GetComponentsInChildren<TextMeshProUGUI>()[1].enabled = false;
            gameObject.GetComponentsInChildren<TextMeshProUGUI>()[2].enabled = true;
            gameObject.GetComponentsInChildren<TextMeshProUGUI>()[3].enabled = false;
        }
        else if (mode == CombatUIMode.Victory)
        {
            gameObject.GetComponentsInChildren<TextMeshProUGUI>()[0].enabled = false;
            gameObject.GetComponentsInChildren<TextMeshProUGUI>()[1].enabled = false;
            gameObject.GetComponentsInChildren<TextMeshProUGUI>()[2].enabled = false;
            gameObject.GetComponentsInChildren<TextMeshProUGUI>()[3].enabled = true;
        }

        Vector3 startPos = new Vector3(panelThing.transform.position.x, panelThing.transform.position.y + 1200.0f, panelThing.transform.position.z);
        Vector3 finalPos = panelThing.transform.position;//new Vector3(panelThing.transform.position.x, panelThing.transform.position.y - 600.0f, panelThing.transform.position.z);

        Vector3 startPosUp = panelThing.transform.position;
        Vector3 finalPosUp = new Vector3(panelThing.transform.position.x, panelThing.transform.position.y + 1200.0f, panelThing.transform.position.z);

        System.Action<ITween<Vector3>> movePanelDown = (t) =>
        {
            panelThing.transform.position = t.CurrentValue;
        };

        System.Action<ITween<Vector3>> movePanelUpCompleted = (t) =>
        {
            panelThing.transform.position = new Vector3(panelThing.transform.position.x, panelThing.transform.position.y - 1200.0f, panelThing.transform.position.z); ;
            StartEvent.Invoke();
            //gameObject.SetActive(false);
        };

        System.Action<ITween<Vector3>> movePanelDownCompleted = (t) =>
        {
            panelThing.Tween("MovePanelOut", startPosUp, finalPosUp, 1.0f, TweenScaleFunctions.CubicEaseIn, movePanelDown, movePanelUpCompleted);
            //gameObject.SetActive(false);

            //panelThing.transform.position = new Vector3(panelThing.transform.position.x, panelThing.transform.position.y + 400.0f, panelThing.transform.position.z);
            //panelThing.transform.position = posz;
        };


        panelThing.Tween("MovePanelIn", startPos, finalPos, 2.0f, TweenScaleFunctions.CubicEaseOut, movePanelDown, movePanelDownCompleted);
    }

}
