using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.InputSystem;

// Visualizes equipped weapons and tools
// NOTE: Perhaps it would be better to rotate the player visuals entirely instead of this,
// will make animations much easier to make down the line.
[RequireComponent(typeof(SpriteRenderer))]
public class EquipableVisual : MonoBehaviour
{
    [SerializeField] private Entity weaponHolder;
    [SerializeField] private WeaponSO equipableSO;

    private SpriteRenderer spriteRenderer;

    private float visualDistance;
    private float yAxisValue;

    private const float yRotationalOffset = 45f;
    private const float xRotation = 90f;
    private const float zRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && equipableSO != null)
        {
            spriteRenderer.sprite = equipableSO.weaponSprite;
        }

        Vector3 relativeDistance = new Vector3(transform.position.x, 0f, transform.position.z);
        visualDistance = (relativeDistance - weaponHolder.transform.position).magnitude;
        yAxisValue = transform.position.y;

        weaponHolder.OnEquipableSwapped += WeaponHolder_OnEquipableSwapped;
    }

    private void WeaponHolder_OnEquipableSwapped(object sender, Entity.OnEquipableSwappedArgs e)
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
            float yRotation = -Mathf.Atan2(mouseDirection.z, mouseDirection.x) * Mathf.Rad2Deg + yRotationalOffset;
            transform.eulerAngles = new Vector3(xRotation, yRotation, zRotation);
        }
    }
}
