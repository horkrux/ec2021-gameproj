using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DigitalRuby.Tween;
using TMPro;

public class CombatXP : MonoBehaviour
{
    public enum CombatXPMode
    {
        XP,
        Miss
    }

    public CombatManager combatMan;
    public CombatXPMode mode;
    public GameObject panelThing;
    public Vector3 position;
    UnityEvent AnimDoneEvent;

    // Start is called before the first frame update
    void Start()
    {
        if (AnimDoneEvent == null)
        {
            AnimDoneEvent = new UnityEvent();
        }

        AnimDoneEvent.AddListener(combatMan.NextAction);
    }

    private void OnEnable()
    {
        float duration;

        if (mode == CombatXPMode.XP)
        {
            duration = 2.0f;
            GetComponentsInChildren<TextMeshProUGUI>()[0].enabled = true;
            GetComponentsInChildren<TextMeshProUGUI>()[1].enabled = false;
        } 
        else
        {
            duration = 1.0f;
            GetComponentsInChildren<TextMeshProUGUI>()[0].enabled = false;
            GetComponentsInChildren<TextMeshProUGUI>()[1].enabled = true;
        }

        transform.position = position;
        //panelThing.transform.position = position;
        Vector3 posz = transform.position;

        System.Action<ITween<Vector3>> movePanelUp = (t) =>
        {
            transform.position = t.CurrentValue;
        };

        System.Action<ITween<Vector3>> movePanelUpCompleted = (t) =>
        {
            if (mode == CombatXPMode.Miss)
            {
                AnimDoneEvent.Invoke();
            }

            gameObject.SetActive(false);
            //panelThing.transform.position = new Vector3(panelThing.transform.position.x, panelThing.transform.position.y + 400.0f, panelThing.transform.position.z);
            //panelThing.transform.position = posz;
        };

        Vector3 startPos = transform.position;
        Vector3 finalPos = new Vector3(transform.position.x, transform.position.y + 4.0f, transform.position.z);

        gameObject.Tween("MoveXPUp", startPos, finalPos, duration, TweenScaleFunctions.Linear, movePanelUp, movePanelUpCompleted);


    }

}