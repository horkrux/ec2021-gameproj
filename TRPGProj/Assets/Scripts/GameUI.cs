using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    bool populate = false;

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

    public void InventoryOnClick()
    {
        inventory.gameObject.SetActive(true);
    }

    public void StatsOnClick()
    {
        stats.gameObject.SetActive(true);
    }

    
}
