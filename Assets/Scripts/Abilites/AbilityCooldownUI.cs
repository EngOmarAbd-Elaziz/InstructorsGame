using UnityEngine;
using UnityEngine.UI;

public class AbilityCooldownUI : MonoBehaviour
{
    [Header("Setup")]
    //[Tooltip("Drag the Player GameObject here")]
    [SerializeField] private AbilityManger playerAbilityManager;
    //[Tooltip("Which ability index is this? (0 for Q, 1 for W, 2 for E)")]
    [SerializeField] private int abilityIndex;

    [SerializeField] private Image cooldownOverlayImage;
    private AbilityHolder linkedAbility;

    private void Start()
    {
        if (playerAbilityManager != null && playerAbilityManager.abilities.Count > abilityIndex)
        {
            linkedAbility = playerAbilityManager.abilities[abilityIndex];
        }
        else
        {
            Debug.LogError($"AbilityCooldownUI Error: Could not find ability index {abilityIndex} on player {playerAbilityManager?.name}");
            enabled = false;
        }
    }

    private void Update()
    {
        if (linkedAbility == null) return;

        switch (linkedAbility.state)
        {
            case AbilityHolder.AbilityState.ready:
                cooldownOverlayImage.fillAmount = 0f;
                break;

            case AbilityHolder.AbilityState.active:
                cooldownOverlayImage.fillAmount = 0f;
                break;

            case AbilityHolder.AbilityState.cooldown:

                float maxTime = linkedAbility.abilitySO.cooldownTime;
                float currentTimer = linkedAbility.cooldownTime;
                cooldownOverlayImage.fillAmount = currentTimer / maxTime;

                break;
        }
    }
}