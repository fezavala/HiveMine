using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.InputSystem;

// Visualizes equipped weapons and tools for the player and plays their appropriate animations
// NOTE: This may need to be made into a component for enemies
[RequireComponent(typeof(SpriteRenderer)), RequireComponent(typeof(Animator))]
public class PlayerEquipableVisual : MonoBehaviour
{
    [SerializeField] private Entity weaponHolder;
    [SerializeField] private AttackComponent weaponHolderAttackComponent;
    private WeaponSO equipableSO;
    private SpriteRenderer spriteRenderer;
    private Animator weaponAnimator;

    private const string ANIMATION_SPEED = "SpeedMultiplier";

    // VERY TEMPORARY! To be changed to an SO type
    private readonly Dictionary<AttackComponent.WeaponType, string> WEAPON_TYPE_TO_ANIMATION_NAME = new() 
    {
        {AttackComponent.WeaponType.STRAIGHT_ATTACK, "StartStabAnimation" },
        {AttackComponent.WeaponType.SWING_ATTACK, "StartStabAnimation"},
    };

    // Start is called before the first frame update
    private void Start()
    {
        weaponAnimator = GetComponent<Animator>();
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

        Inventory.Instance.OnEquipableSwapped += Inventory_OnEquipableSwapped;
        weaponHolderAttackComponent.OnAttackStart += WeaponHolderAttackComponent_OnAttackStart;

    }

    private void WeaponHolderAttackComponent_OnAttackStart(object sender, AttackComponent.OnAttackStartEventArgs e)
    {
        weaponAnimator.SetFloat(ANIMATION_SPEED, e.attackSpeed);
        weaponAnimator.SetTrigger(WEAPON_TYPE_TO_ANIMATION_NAME[e.attackType]);
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
