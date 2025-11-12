using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolUI : MonoBehaviour
{
    [SerializeField] private Image toolImageSlot; 

    private WeaponSO equippedWeaponSO;

    // Start is called before the first frame update
    void Start()
    {
        toolImageSlot.sprite = null;

        // Subscribe to inventory to get notified on the currently equipped weapon
        Inventory.Instance.OnEquipableSwapped += Inventory_OnEquipableSwapped;
    }

    private void Inventory_OnEquipableSwapped(object sender, Inventory.OnEquipableSwappedArgs e)
    {
        equippedWeaponSO = e.equipableSO;

        if (equippedWeaponSO != null)
        {
            toolImageSlot.sprite = equippedWeaponSO.weaponSprite;
        }
        else
        {
            toolImageSlot.sprite = null;
        }
    }
}
