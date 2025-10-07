using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    [SerializeField] private float speed = 7f;

    // Update is called once per frame
    private void Update()
    {
        Vector2 inputVector = getMovementInput();

        inputVector = inputVector.normalized;
        Vector3 movementVector = new Vector3(inputVector.x, 0f, inputVector.y);
        transform.position += movementVector * speed * Time.deltaTime;
    }

    private Vector2 getMovementInput()
    {
        Vector2 movementDirection = new Vector2();

        if (Input.GetKey(KeyCode.W))
        {
            movementDirection.y = +1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movementDirection.y = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movementDirection.x = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movementDirection.x = +1;
        }

        return movementDirection;
    }
}
