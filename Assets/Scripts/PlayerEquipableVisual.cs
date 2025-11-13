using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.InputSystem;

// Visualizes equipped weapons and tools for the player
// NOTE: This may need to be made into a component for enemies
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerEquipableVisual : MonoBehaviour
{
    [SerializeField] private Entity weaponHolder;
    private WeaponSO equipableSO;
    private SpriteRenderer spriteRenderer;

    private float visualDistance;
    private float yAxisValue;

    private const float X_ROTATION = 90f;
    private const float Z_ROTATION = 0f;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Set sprite to WeaponSO Sprite if it exists
        if (equipableSO != null)
        {
            spriteRenderer.sprite = equipableSO.weaponSprite;
        }
        else
        {
            spriteRenderer.sprite = null;
        }

        // Setup for position of tool
        Vector3 relativeDistance = new Vector3(transform.position.x, 0f, transform.position.z);
        visualDistance = (relativeDistance - weaponHolder.transform.position).magnitude;
        yAxisValue = transform.position.y;

        Inventory.Instance.OnEquipableSwapped += Inventory_OnEquipableSwapped;
    }

    private void Inventory_OnEquipableSwapped(object sender, Inventory.OnEquipableSwappedArgs e)
    {
        equipableSO = e.equipableSO;
        if (equipableSO != null)
        {
            gameObject.SetActive(true);
            spriteRenderer.sprite = equipableSO.weaponSprite;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
