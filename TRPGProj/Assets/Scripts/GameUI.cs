using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    bool populate = false;

    public HelpScreen help;
    public ReturnToMenuPrompt prompt;
    public CombatManager combatMan;
    public InventoryUI inventory;
    public StatsUI stats;
    public Texture2D mouse;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void HelpOnClick()
    {
        help.gameObject.SetActive(true);
    }

    public void MenuOnClick()
    {
        prompt.gameObject.SetActive(true);
    }

    public void InventoryOnClick()
    {
        inventory.gameObject.SetActive(true);
    }

    public void StatsOnClick()
    {
        stats.gameObject.SetActive(true);
    }

    public void EndTurnOnClick()
    {
        gameObject.GetComponentsInChildren<Button>(true)[0].interactable = false;
        gameObject.GetComponentsInChildren<Button>(true)[2].interactable = false;
        combatMan.SwapTurn();
    }

    public void EnableEndTurnButton()
    {
        gameObject.GetComponentsInChildren<Button>(true)[2].gameObject.SetActive(true);
    }

    public void DisableEndTurnButton() 
    {
        gameObject.GetComponentsInChildren<Button>(true)[2].gameObject.SetActive(false);
    }

    public void EnableInventoryButton()
    {
        gameObject.GetComponentsInChildren<Button>(true)[0].interactable = true;

    }

    public void DisableInventoryButton()
    {
        gameObject.GetComponentsInChildren<Button>(true)[0].interactable = false;
    }

    public void SwitchEndTurnButton()
    {
        Button endTurnButton = gameObject.GetComponentsInChildren<Button>(true)[2];

        endTurnButton.interactable ^= true;
    }
}
