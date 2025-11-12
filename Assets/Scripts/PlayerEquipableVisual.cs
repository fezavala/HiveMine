using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.InputSystem;

// Visualizes equipped weapons and tools for the player
// NOTE: This may need to be made into a component for enemies
// NOTE: Perhaps it would be better to rotate the player visuals entirely instead of this,
// will make animations much easier to make down the line.
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerEquipableVisual : MonoBehaviour
{
    [SerializeField] private Entity weaponHolder;
    private WeaponSO equipableSO;
    private SpriteRenderer spriteRenderer;

    private float visualDistance;
    private float yAxisValue;

    private const float Y_ROTATIONAL_OFFSET = 45f;
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

    // Update is called once per frame
    void Update()
    {
        // Change this to its own method
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 mouseWorldPosition = hit.point;
            mouseWorldPosition.y = 0f;
            Vector3 mouseDirection = mouseWorldPosition - weaponHolder.transform.position;
            mouseDirection = mouseDirection.normalized;

            transform.position = weaponHolder.transform.position + mouseDirection * visualDistance;
            transform.position = new Vector3(transform.position.x, yAxisValue, transform.position.z);

            // Rotating tool to face towards mouseDirection
            // I think Unity's rotations are inverted which is why I use a negative here
            float yRotation = -Mathf.Atan2(mouseDirection.z, mouseDirection.x) * Mathf.Rad2Deg + Y_ROTATIONAL_OFFSET;
            transform.eulerAngles = new Vector3(X_ROTATION, yRotation, Z_ROTATION);
        }
    }
}
