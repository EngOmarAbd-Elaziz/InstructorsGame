using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class AbilityManger : MonoBehaviour
{
    // list that will hold all abilites and thier details
    public List<AbilityHolder> abilities;

    private void Update()
    {
        if (!GameManger.Instance.IsGamePlaying() || GameManger.Instance.IsGameOver()
            || GameManger.Instance.isGamePaused) { return; }

        // will loop on every ability that is in the list and cast it if the player presses
        // certain key that is in the abilityholderSO
        foreach (AbilityHolder ability in abilities)
        {
            ability.CastAbility(gameObject);
        }
    }
}