using UnityEngine;
[System.Serializable]
public class AbilityHolder
{
    public string abilityName;
    public KeyCode triggerKey;
    public AbilitesSO abilitySO;
    public Transform enemyTarget;

    private float cooldownTime;
    private float activeTime;
    private AbilityState state = AbilityState.ready;
    
    enum AbilityState
    {
        ready,
        active,
        cooldown
    }

    public void CastAbility(GameObject parent)
    {
        switch (state)
        {
            case AbilityState.ready:

                if (Input.GetKeyDown(triggerKey))
                {
                    abilitySO.Activate(parent , enemyTarget);
                    state = AbilityState.active;
                    activeTime = abilitySO.activeTime;
                }
                break;

            case AbilityState.active:

                if (activeTime > 0)
                {
                    activeTime -= Time.deltaTime;
                }
                else
                {
                    Debug.Log(abilitySO.name + " Active Time Has Ended");
                    state = AbilityState.cooldown;
                    cooldownTime = abilitySO.cooldownTime;
                }
                break;

            case AbilityState.cooldown:

                if (cooldownTime > 0)
                {
                    cooldownTime -= Time.deltaTime;
                }
                else
                {
                    Debug.Log(abilitySO.name + " Cooldown Time Has ended and ability can be reused");
                    state = AbilityState.ready;
                }
                break;
        }
    }
}