using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSO", menuName = "ScriptableObjects/WeaponSO")]
public class WeaponSO : ScriptableObject
{
    public string weaponName;
    public Sprite weaponSprite;
    // NOTE: This may turn into an attackTypeSO, we will see
    public AttackComponent.WeaponType weaponType;  // Determines the attack used by the AttackComponent
    [Range(1, 50)] public int weaponDamage;  // Determines the damage done to enemies and the player
    [Range(1, 50)] public int toolDamage;  // Determines the damage done to walls and objects
    [Range(1, 50)] public int weaponPenetration;  // Amount of things the weapon can hit in a swing
    [Range(0f, 20)] public float weaponAttackRange; // The weapons reach
    [Range(1f, 50f)] public float weaponAttackSpeed;  // How long a swing takes
    [Range(0f, 50f)] public float weaponAttackCooldown;  // Time between attacks
}
