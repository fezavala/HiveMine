using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Script that rotates the player to wherever the mouse is pointing
public class PlayerVisuals : MonoBehaviour
{
    private const float Y_ROTATIONAL_OFFSET = 45f;

    // Update is called once per frame
    void Update()
    {
        RotateToFaceMousePosition();
    }

    private void RotateToFaceMousePosition()
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


            // Rotating tool to face towards mouseDirection
            // I think Unity's rotations are inverted which is why I use a negative here
            float yRotation = -Mathf.Atan2(mouseDirection.z, mouseDirection.x) * Mathf.Rad2Deg + Y_ROTATIONAL_OFFSET;
            transform.eulerAngles = new Vector3(0f, yRotation, 0f);
        }
    }
}

