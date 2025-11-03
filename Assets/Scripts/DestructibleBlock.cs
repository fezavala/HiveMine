using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Wall that can break
[RequireComponent(typeof(HealthComponent))]
public class DestructibleBlock : MonoBehaviour
{
    private HealthComponent healthComponent;

    // Start is called before the first frame update
    void Start()
    {
        healthComponent = GetComponent<HealthComponent>();
        // Quick Rule: Subscribe to events in the Start method, not the Awake method
        healthComponent.OnZeroHPLeft += HealthComponent_OnZeroHPLeft;
    }

    private void HealthComponent_OnZeroHPLeft(object sender, System.EventArgs e)
    {
        Debug.Log("Destroying " + gameObject.name);
        Destroy(gameObject);
    }
}
