using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Talk : MonoBehaviour
{
    protected struct StateData
    {
        public string stateName;
        public string npcText;
        public List<string> responseStrings;
    }

    public PlayerCharacter player;
    public TalkWindow talkUI;
    public ItemPopup itemPopup;
    private int TALK_STATE = 0;
    private List<StateData> stateDataList = new List<StateData>();
    private List<Tuple<int, int>> rewards = new List<Tuple<int, int>>();

    private int ringItemId = 2;

    // Start is called before the first frame update
    void Start()
    {
        XDocument doc = XDocument.Load("Assets/Text/Talk1.txt");

        XElement root = doc.Root;

        foreach (XElement state in root.Elements())
        {
            StateData stateData = new StateData();
            stateData.responseStrings = new List<string>();

            stateData.stateName = state.Attribute("name").Value;
            stateData.npcText = state.Element("npctext").Value;

            XElement responses = state.Element("responses");

            foreach (XElement response in responses.Elements())
            {
                stateData.responseStrings.Add(response.Value);
            }

            stateDataList.Add(stateData);
        }

        rewards.Add(new Tuple<int, int>(0, 3));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init()
    {
        talkUI.gameObject.SetActive(true);
        LoadState();
    }

    private void LoadState()
    {
        if (player.gameObject.GetComponent<Inventory>().HasItem(ringItemId))
        {
            if (TALK_STATE == 0)
                TALK_STATE = 7;
            else if (TALK_STATE == 2)
                TALK_STATE = 8;
            else if (TALK_STATE == 4)
                TALK_STATE = 5;
        }

        if (TALK_STATE == 0)
        {
            UnityEngine.Events.UnityAction[] responseActions = { delegate { StateFunc0To3(); }, delegate { StateFunc0To1(); } };
            talkUI.SetUIElements(stateDataList[0].npcText, stateDataList[0].responseStrings, responseActions);
        }
        else if (TALK_STATE == 1)
        {
            UnityEngine.Events.UnityAction[] responseActions = { delegate { StateFunc1To2(); } };
            talkUI.SetUIElements(stateDataList[1].npcText, stateDataList[1].responseStrings, responseActions);
        }
        else if (TALK_STATE == 2)
        {
            UnityEngine.Events.UnityAction[] responseActions = { delegate { StateFunc2To3(); }, delegate { StateFunc2To1(); } };
            talkUI.SetUIElements(stateDataList[2].npcText, stateDataList[2].responseStrings, responseActions);
        }
        else if (TALK_STATE == 3)
        {
            UnityEngine.Events.UnityAction[] responseActions = { delegate { StateFunc3To4(); } };
            talkUI.SetUIElements(stateDataList[3].npcText, stateDataList[3].responseStrings, responseActions);
        }
        else if (TALK_STATE == 4)
        {
            UnityEngine.Events.UnityAction[] responseActions = { delegate { StateFunc4To3(); } };
            talkUI.SetUIElements(stateDataList[4].npcText, stateDataList[4].responseStrings, responseActions);
        }
        else if (TALK_STATE == 5)
        {
            UnityEngine.Events.UnityAction[] responseActions = { delegate { StateFunc5To6(); } };
            talkUI.SetUIElements(stateDataList[5].npcText, stateDataList[5].responseStrings, responseActions);
        }
        else if (TALK_STATE == 6)
        {
            UnityEngine.Events.UnityAction[] responseActions = { delegate { StateFunc6To9(); } };
            talkUI.SetUIElements(stateDataList[6].npcText, stateDataList[6].responseStrings, responseActions);
            player.gameObject.GetComponent<Inventory>().RemoveItem(ringItemId);
            player.gameObject.GetComponent<Inventory>().AddItem(rewards[0].Item1, rewards[0].Item2);
            itemPopup.Show(rewards);
        }
        else if (TALK_STATE == 7)
        {
            UnityEngine.Events.UnityAction[] responseActions = { delegate { StateFunc7To6(); } };
            talkUI.SetUIElements(stateDataList[7].npcText, stateDataList[7].responseStrings, responseActions);
        }
        else if (TALK_STATE == 8)
        {
            UnityEngine.Events.UnityAction[] responseActions = { delegate { StateFunc8To6(); } };
            talkUI.SetUIElements(stateDataList[8].npcText, stateDataList[8].responseStrings, responseActions);
        }
        else if (TALK_STATE == 9)
        {
            UnityEngine.Events.UnityAction[] responseActions = { delegate { StateFunc9To9(); } };
            talkUI.SetUIElements(stateDataList[9].npcText, stateDataList[9].responseStrings, responseActions);
        }
    }

    private void StateFunc0To3()
    {
        TALK_STATE = 3;
        LoadState();
    }

    private void StateFunc0To1()
    {
        TALK_STATE = 1;
        LoadState();
    }

    private void StateFunc1To2()
    {
        TALK_STATE = 2;
        talkUI.gameObject.SetActive(false);
    }

    private void StateFunc2To3()
    {
        TALK_STATE = 3;
        LoadState();
    }

    private void StateFunc2To1()
    {
        TALK_STATE = 1;
        LoadState();
    }

    private void StateFunc3To4()
    {
        TALK_STATE = 4;
        talkUI.gameObject.SetActive(false);
    }

    private void StateFunc4To3()
    {
        TALK_STATE = 3;
        LoadState();
    }

    private void StateFunc5To6()
    {
        TALK_STATE = 6;
        LoadState();
    }

    private void StateFunc6To9()
    {
        TALK_STATE = 9;
        talkUI.gameObject.SetActive(false);
    }

    private void StateFunc7To6()
    {
        TALK_STATE = 6;
        LoadState();
    }

    private void StateFunc8To6()
    {
        TALK_STATE = 6;
        LoadState();
    }

    private void StateFunc9To9()
    {
        talkUI.gameObject.SetActive(false);
    }
}
