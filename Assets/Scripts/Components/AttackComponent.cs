using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A component script that enables a gameObject to attack based on some sort of attack input,
// such as a player input or an enemy attack signal
public class AttackComponent : MonoBehaviour
{
    [SerializeField] private WeaponSO currentWeaponSO;
    [SerializeField] private GameObject attackDirectionProviderGameObject;  // Interfaces cannot be serialized, so a GetComponent is done instead on this object

    private IAttackDirectionProvider attackDirectionProvider;
    private float currentAttackCooldown;  // NOTE: Not planned to be used yet

    private bool isAttacking = false;
    private bool performedAttack = false;
    private float currentAttackTimer = 0f;

    private const int ATTACK_LAYER = 6;

    // NOTE: This will most likely be moved to a Scriptable Object for attack types
    private const float BASE_STRAIGHT_ATTACK_SPEED = 0.25f;
    private const float BASE_STRAIGHT_COMPLETE_ATTACK_SPEED = 0.5f;

    private const float BASE_SWING_ATTACK_SPEED = 0.25f;
    private const float BASE_SWING_COMPLETE_ATTACK_SPEED = 0.5f;

    public event EventHandler<OnAttackStartEventArgs> OnAttackStart;
    public class OnAttackStartEventArgs: EventArgs
    {
        public WeaponType attackType;
        public float attackSpeed;
    }

    private void Start()
    {
        attackDirectionProvider = attackDirectionProviderGameObject.GetComponent<IAttackDirectionProvider>();
        if (attackDirectionProvider == null)
        {
            Debug.LogError("Attack Component of " + transform.name + " does not have a component that implements IAttackDirectionProvider!");
        }
    }

    // Determines the attack used by the AttackComponent
    // NOTE: Probably temporary, again probably will be moved to an SO
    public enum WeaponType
    {
        STRAIGHT_ATTACK,
        SWING_ATTACK,
    }

    public void SetWeaponSO(WeaponSO newWeaponSO)
    {
        if (isAttacking)
        {
            Debug.LogError(transform.name + " is currently performing an attack. Unable to set a new WeaponSO!");
            return;
        }
        currentWeaponSO = newWeaponSO;
    }

    // Can be used to prevent weapon switching during an attack
    public bool IsAttacking()
    {
        return isAttacking;
    }

    // This could be performed by an event, but for now this is fine
    // Starts the attack sequence with the currentWeaponSO
    public void PerformAttack()
    {
        if (currentWeaponSO == null)
        {
            Debug.Log("No WeaponSO assigned to AttackComponent on " + gameObject.name + ", cannot perform attack!");
            return;
        }
        if (isAttacking) return;

        isAttacking = true;
        performedAttack = false;
        currentAttackTimer = 0f;

        OnAttackStart?.Invoke(this, new OnAttackStartEventArgs
        {
            attackType = currentWeaponSO.weaponType,
            attackSpeed = currentWeaponSO.weaponAttackSpeed
        });
    }

    // Straight attacts perform a raycast check for health components and deals damage to them
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

    private void ProcessStraightAttack()
    {
        if (!performedAttack)
        {
            float attackTime = BASE_STRAIGHT_ATTACK_SPEED / currentWeaponSO.weaponAttackSpeed;
            if (currentAttackTimer >= attackTime)
            {
                Vector3 normalizedAttackDirection = attackDirectionProvider.GetNormalizedAttackDirectionVector();
                PerformStraightAttack(normalizedAttackDirection);
                performedAttack = true;
            }
            return;
        }
        float attackCompleteTime = BASE_STRAIGHT_COMPLETE_ATTACK_SPEED / currentWeaponSO.weaponAttackSpeed;
        if (currentAttackTimer >= attackCompleteTime)
        {
            isAttacking = false;
        }
    }

    //TODO: create swing attack
    private void ProcessSwingAttack()
    {
        if (!performedAttack)
        {
            float attackTime = BASE_SWING_ATTACK_SPEED / currentWeaponSO.weaponAttackSpeed;
            if (currentAttackTimer >= attackTime)
            {
                Vector3 normalizedAttackDirection = attackDirectionProvider.GetNormalizedAttackDirectionVector();
                PerformSwingAttack(normalizedAttackDirection);
                performedAttack = true;
            }
            return;
        }
        float attackCompleteTime = BASE_SWING_COMPLETE_ATTACK_SPEED / currentWeaponSO.weaponAttackSpeed;
        if (currentAttackTimer >= attackCompleteTime)
        {
            isAttacking = false;
        }
    }

    // Just increments attack timer and calls appropriate attack processer function
    private void ProcessAttack()
    {
        currentAttackTimer += Time.deltaTime;

        switch (currentWeaponSO.weaponType)
        {
            case WeaponType.SWING_ATTACK:
                ProcessSwingAttack(); break;
            case WeaponType.STRAIGHT_ATTACK:
                ProcessStraightAttack(); break;
        }
    }

    private void Update()
    {
        if (!isAttacking) return;

        ProcessAttack();
    }

}
