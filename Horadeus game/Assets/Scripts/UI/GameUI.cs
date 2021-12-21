using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public TextMeshProUGUI arrowCountText;

    public void Init() { 
        
    }

    public void UpdatePlayerInventoryHUD()
    {
        ItemData arrowItem = Game.inst.player.inventory.GetItem(ItemType.Arrow);
        arrowCountText.text = "Arrows: " + arrowItem.count;
    }

}
