using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// This is the player class, originally it was supposed to be the base entity class for all entities but
// since scripts are now component-based, it makes more sense to have dedicated classes for now.
[RequireComponent(typeof(HealthComponent))]  // <- This is a thing btw
public class Entity : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask staticObjectLayer;  // Note: This doesn't do anything anymore
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Transform damagePointTransform;
    [SerializeField] private AttackComponent attackComponent;
    
    private HealthComponent healthComponent;

    private float tempDamageRange = 1.5f;  // This lines up with damagePointTransform

    private void Start()
    {
        // Subscribing to health component
        healthComponent = GetComponent<HealthComponent>();
        healthComponent.OnZeroHPLeft += HealthComponent_OnZeroHPLeft;

        Inventory.Instance.OnEquipableSwapped += Inventory_OnEquipableSwapped;

        tempDamageRange = (damagePointTransform.position - transform.position).magnitude;
    }

    private void Inventory_OnEquipableSwapped(object sender, Inventory.OnEquipableSwappedArgs e)
    {
        if (attackComponent == null)
        {
            Debug.LogError(gameObject.name + " has no Attack Component to set a swapped weapon to!");
            return;
        }
        attackComponent.setWeaponSO(e.equipableSO);
    }

    private void Update()
    {
        TestHandleMovement();
        UpdateDamagePointVisual();
        TestDamageTaken();
        TestDealDamage();
    }

    private void HealthComponent_OnZeroHPLeft(object sender, EventArgs e)
    {
        // TODO: Implement death state and processing
        Debug.Log("Player has died!");
    }

    // Note: temporary for now
    private void UpdateDamagePointVisual()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 mouseWorldPosition = hit.point;
            mouseWorldPosition.y = 0f;
            Vector3 mouseDirection = mouseWorldPosition - transform.position;
            mouseDirection = mouseDirection.normalized;

            damagePointTransform.position = transform.position + mouseDirection * tempDamageRange;
        }
    }


    private void TestDealDamage()
    {
        bool dealDamage = Input.GetKeyDown(KeyCode.Mouse0);
        if (dealDamage)
        {
            if (attackComponent == null)
            {
                Debug.Log(gameObject.name + " has no AttackComponent, cannot perform attack");
                return;
            }
            Vector3 damageDirection = (damagePointTransform.position - transform.position).normalized;
            attackComponent.PerformAttack(damageDirection);
        }
    }

    // Temporary Method testing if player dies
    private void TestDamageTaken()
    {
        bool takeDamage = Input.GetKeyDown(KeyCode.T);
        if (takeDamage)
        {
            healthComponent.DealDamage(1);
            Debug.Log("Took 1/4 heart of damage. Health left(4HP = 1 Heart): " + healthComponent.GetCurrentHP());
        }
    }

    private void TestHandleMovement()
    {
        // Input Vector returned from GameInput Class which uses Unity's new input system
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        float moveAmount = moveSpeed * Time.deltaTime;

        Vector3 movementVector = new Vector3(inputVector.x, 0f, inputVector.y) * moveAmount;

        characterController.Move(movementVector);
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
    }
}
