using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

// Shake animation component, attach to object visuals that will shake, health component needed for now a refactor may be needed later for more compatibity
public class ShakeAnimation : MonoBehaviour
{
    [SerializeField] private HealthComponent healthComponent;  // Currently tied to healthComponent
    [SerializeField, Range(0f, 3f)] private float duration = 0.4f;  
    [SerializeField, Range(0f, 1f)] private float intensity = 0.5f;  // How fast the shaking looks
    [SerializeField, Range(0f, 2f)] private float range = 0.3f;  // The maximum distance the shaking will take place from the center

    private Vector3 restingPosition;  // The objects regular position
    private Vector3 animationStartPosition;  // The position the object will be in at the start of the animation
    private bool animating = false;
    private float animationTimer;
    private float returnDuration;  // Time it takes to smoothly return to animationStartPosition
    private float returnTimer;  // Timer for returning to animationStartPosition

    private static float Y_ANIM_OFFSET = 0.01f;  // Raises block to enphasize it
    private static float RETURN_DURATION_PERCENTAGE = 0.1f; // The returnDuration is this percentage of the regular duration 

    private void Awake()
    {
        restingPosition = transform.localPosition;
        animationStartPosition = new Vector3(restingPosition.x, restingPosition.y + Y_ANIM_OFFSET, restingPosition.z);
        returnDuration = duration * RETURN_DURATION_PERCENTAGE;
    }

    // Start is called before the first frame update
    private void Start()
    {
        healthComponent.OnHPChanged += HealthComponent_OnHPChanged;
    }

    private void HealthComponent_OnHPChanged(object sender, HealthComponent.OnHPChangedEventArgs e)
    {
        if (!animating) transform.localPosition = animationStartPosition;
        animating = true;
        animationTimer = 0f;
    }

    private void ProcessShakeAnimation()
    {
        animationTimer += Time.deltaTime;

        if (animationTimer < duration)
        {
            // Choose a random point and move towards it smoothly every frame, the faster the intensity the shakier it will appear
            Vector3 animationOffset = Random.insideUnitSphere;
            animationOffset.y = animationStartPosition.y;
            animationOffset *= range;

            transform.localPosition = Vector3.Lerp(transform.localPosition, animationOffset, intensity);
        }
        else
        {
            // Smoothly return to starting position quickly
            returnTimer += Time.deltaTime;
            float returnValue = 1f * (returnTimer / returnDuration);
            transform.localPosition = Vector3.Lerp(transform.localPosition, animationStartPosition, returnValue);
        }

        // End animation once localPosition is the start position
        if (transform.localPosition == animationStartPosition)
        {
            animating = false;
            transform.localPosition = restingPosition;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (!animating) return;

        ProcessShakeAnimation();
    }
}
