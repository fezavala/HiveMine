using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A component script that enables a gameObject to attack based on some sort of attack input,
// such as a player input or an enemy attack signal
public class AttackComponent : MonoBehaviour
{
    [SerializeField] private WeaponSO currentWeaponSO;

    private float currentAttackCooldown = 0f;

    private const int ATTACK_LAYER = 6;


    // Determines the attack used by the AttackComponent
    // NOTE: Probably temporary
    public enum WeaponType
    {
        STRAIGHT_ATTACK,
        SWING_ATTACK,
    }

    public void setWeaponSO(WeaponSO newWeaponSO)
    {
        currentWeaponSO = newWeaponSO;
    }

    public void PerformAttack(Vector3 attackDirectionNormalized)
    {
        if (currentWeaponSO == null)
        {
            Debug.LogError("No WeaponSO assigned to AttackComponent on " + gameObject.name + ", cannot perform attack!");
            return;
        }

        switch (currentWeaponSO.weaponType)
        {
            case WeaponType.SWING_ATTACK:
                PerformSwingAttack(attackDirectionNormalized); break;
            case WeaponType.STRAIGHT_ATTACK:
                PerformStraightAttack(attackDirectionNormalized); break;
        }
    }

    private void PerformStraightAttack(Vector3 attackDirectionNormalized)
    {
        //TODO: To handle enemy attacks, two attacks are going to be performed, one for blocks and one for enemies. These should be separate

        // Basic raycast sequence
        if (Physics.Raycast(transform.position, attackDirectionNormalized, out RaycastHit raycastHit, currentWeaponSO.weaponAttackRange, LayerMask.GetMask(LayerMask.LayerToName(ATTACK_LAYER))))
        {
            if (raycastHit.transform.TryGetComponent(out HealthComponent healthComponent))
            {
                bool performToolDamage = raycastHit.transform.TryGetComponent(out DestructibleBlock destructibleBlock);
                int damage = performToolDamage ? currentWeaponSO.toolDamage : currentWeaponSO.weaponDamage;
                healthComponent.DealDamage(damage, performToolDamage);
            }
            else
            {
                Debug.Log(raycastHit.transform.name + " does not have a HealthComponent");
            }
        }
    }

    private void PerformSwingAttack (Vector3 attackDirectionNormalized)
    {
        //TODO: Make swing attack
        PerformStraightAttack(attackDirectionNormalized);
    }


    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
