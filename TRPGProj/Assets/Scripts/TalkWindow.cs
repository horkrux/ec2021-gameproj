using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TalkWindow : MonoBehaviour
{

    public Button buttonPrefab;
    public Text textNPC;
    public GameObject playerResponses;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUIElements(string npcText, List<string> responseTexts, UnityEngine.Events.UnityAction[] responseActions)
    {
        textNPC.text = npcText;

        for (int i = 0; i < playerResponses.transform.childCount; i++)
        {
            Destroy(playerResponses.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < responseTexts.Count; i++)
        {
            Button responseButton = Instantiate(buttonPrefab);
            responseButton.transform.parent = playerResponses.transform;
            responseButton.onClick.AddListener(responseActions[i]);
            responseButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = responseTexts[i];
        }
    }
}
