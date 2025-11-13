using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Shake animation component, attach to object visuals that will shake, health component needed for now a refactor may be needed later for more compatibity
public class ShakeAnimation : MonoBehaviour
{
    [SerializeField] private HealthComponent healthComponent;
    [SerializeField, Range(0f, 3f)] private float duration = 0.4f;
    [SerializeField, Range(0f, 1f)] private float intensity = 0.5f;
    [SerializeField, Range(0f, 2f)] private float range = 0.3f;

    private Vector3 restingPosition;
    private Vector3 animationStartPosition;
    private bool animating = false;
    private float animationTimer;

    private static float Y_ANIM_OFFSET = 0.01f;  // Raises block to enphasize it

    private void Awake()
    {
        restingPosition = transform.localPosition;
        animationStartPosition = new Vector3(restingPosition.x, restingPosition.y + Y_ANIM_OFFSET, restingPosition.z);
    }

    // Start is called before the first frame update
    private void Start()
    {
        healthComponent.OnHPChanged += HealthComponent_OnHPChanged;
    }

    private void HealthComponent_OnHPChanged(object sender, HealthComponent.OnHPChangedEventArgs e)
    {
        animating = true;
        animationTimer = 0f;
        transform.localPosition = animationStartPosition;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!animating) return;

        animationTimer += Time.deltaTime;

        Vector3 animationOffset = Random.insideUnitSphere;
        animationOffset.y = animationStartPosition.y;
        animationOffset *= range;

        Vector3 currentPosition = transform.localPosition;
        transform.localPosition = Vector3.Lerp(currentPosition, animationOffset, intensity);

        if (animationTimer > duration)
        {
            animating = false;
            transform.localPosition = restingPosition;
        }

    }
}
