using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// UI Component script that manages the health visuals
public class HealthUI : MonoBehaviour
{
    [SerializeField] private HealthComponent playerHealthComponent;

    [SerializeField] private Sprite emptyHealthSprite;
    [SerializeField] private Sprite oneQuarterHealthSprite;
    [SerializeField] private Sprite twoQuartersHealthSprite;
    [SerializeField] private Sprite threeQuartersHealthSprite;
    [SerializeField] private Sprite fullHealthSprite;

    private const int HEART_HEALTH_AMOUNT = 4;

    private int maxHealthAmount;
    private int activeHearts;

    private Image[] heartImages;


    // Start is called before the first frame update
    void Start()
    {
        heartImages = gameObject.GetComponentsInChildren<Image>();

        Debug.Log("Testing if HealthUI grabbed child image components: " +  heartImages.Length);

        if (playerHealthComponent != null)
        {
            playerHealthComponent.OnHPChanged += PlayerHealthComponent_OnHPChanged;
            playerHealthComponent.OnZeroHPLeft += PlayerHealthComponent_OnZeroHPLeft;
            maxHealthAmount = Mathf.Min(playerHealthComponent.GetMaxHP(), heartImages.Length * HEART_HEALTH_AMOUNT);
        } 
        else
        {
            Debug.Log("Health UI does not detect a HealthComponent. Set one later using SetPlayerHealthComponent to use this component.");
            maxHealthAmount = 0;
        }

        SetUpHealthUI();
    }

    public void SetPlayerHealthComponent(HealthComponent newHealthComponent)
    {
        if (playerHealthComponent != null)
        {
            playerHealthComponent.OnHPChanged -= PlayerHealthComponent_OnHPChanged;
            playerHealthComponent.OnZeroHPLeft -= PlayerHealthComponent_OnZeroHPLeft;
        }

        playerHealthComponent = newHealthComponent;
        playerHealthComponent.OnHPChanged += PlayerHealthComponent_OnHPChanged;
        playerHealthComponent.OnZeroHPLeft += PlayerHealthComponent_OnZeroHPLeft;
    }

    private void SetUpHealthUI()
    {
        //TODO: Account for more health than icons!
        int activeHeartAmount = maxHealthAmount / HEART_HEALTH_AMOUNT;
        int remainderAmount = maxHealthAmount % HEART_HEALTH_AMOUNT;
        bool remainderAvailable = remainderAmount > 0;
        if (remainderAvailable)
        {
            activeHeartAmount++;
        }
        activeHearts = activeHeartAmount;

        for (int  i = 0; i < heartImages.Length; i++)
        {
            if (i < activeHeartAmount - 2)
            {
                continue;
            }
            if (i == activeHeartAmount - 1 && remainderAvailable)
            {
                heartImages[i].sprite = GetQuarterHeart(remainderAmount);
                continue;
            }
            if (i > activeHeartAmount - 1)
            {
                heartImages[i].gameObject.SetActive(false);
            }
        }
    }

    private Sprite GetQuarterHeart(int remainderAmount)
    {
        switch (remainderAmount)
        {
            case 1:
                return oneQuarterHealthSprite;
            case 2:
                return twoQuartersHealthSprite;
            case 3:
                return threeQuartersHealthSprite;
            default:
                Debug.LogError("HealthUI.GetQuarterHeart: Invalid remainderAmount. Sprite set to emptyHealthSprite.");
                return emptyHealthSprite;
        }
    }

    private void PlayerHealthComponent_OnZeroHPLeft(object sender, System.EventArgs e)
    {
        for (int i = 0; i < activeHearts; i++)
        {
            heartImages[i].sprite = emptyHealthSprite;
        }
    }

    private void PlayerHealthComponent_OnHPChanged(object sender, HealthComponent.OnHPChangedEventArgs e)
    {
        if (e.hitPoints >= maxHealthAmount) return;

        int heartChangeIndex = e.hitPoints / HEART_HEALTH_AMOUNT;
        for (int i = 0; i < activeHearts; i++)
        {
            if (i < heartChangeIndex)
            {
                heartImages[i].sprite = fullHealthSprite;
                continue;
            }
            
            if (i == heartChangeIndex)
            {
                bool remainder = e.hitPoints % HEART_HEALTH_AMOUNT != 0;
                if (remainder)
                {
                    heartImages[i].sprite = GetQuarterHeart(e.hitPoints % HEART_HEALTH_AMOUNT);
                }
                else
                {
                    heartImages[i].sprite = emptyHealthSprite;
                }
                    continue;
            }
            heartImages[i].sprite = emptyHealthSprite;
        }

    }
}
