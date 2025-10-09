using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask staticObjectLayer;
    [SerializeField] private CharacterController characterController;

    // Update is called once per frame
    private void Update()
    {
        testHandleMovement();
    }

    private void testHandleMovement()
    {
        // Input Vector returned from GameInput Class which uses Unity's new input system
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        float moveAmount = moveSpeed * Time.deltaTime;

        Vector3 movementVector = new Vector3(inputVector.x, 0f, inputVector.y) * moveAmount;

        characterController.Move(movementVector);
    }
}
