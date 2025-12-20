using System;
using UnityEngine;


// this class is a middle man between abilites and healthBar
public class PlayerHealth : MonoBehaviour
{
    public HealthSystem healthSystem;
    [SerializeField] private float healthAmount = 100f;
    [SerializeField] private GameInput.PlayerID playerID;
    public event EventHandler OnHealthSystemChanged;
    private void Awake()
    {
        healthSystem = new HealthSystem(healthAmount);
    }

    private void Start()
    {
        SubscribeToEvents();
    }

    private void SubscribeToEvents() 
    {
        healthSystem.OnHealthChanged -= HealthSystem_OnHealthChanged;
        healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
    }
    private void HealthSystem_OnHealthChanged(object sender, EventArgs e)
    {
        if(healthSystem.GetHealth() <= 0) 
        {
            Die();
        }
    }

    private void Die() 
    {
        GameInput.PlayerID winnerID;
        if (playerID == GameInput.PlayerID.Player1)
        {
            winnerID = GameInput.PlayerID.Player2;
        }
        else 
        {
            winnerID = GameInput.PlayerID.Player1;
        }

        GameManger.Instance.ProcessRoundWin(winnerID);
    }

    public void ResetHealth() 
    {
        healthSystem = new HealthSystem(healthAmount);
        SubscribeToEvents();
        OnHealthSystemChanged?.Invoke(this, EventArgs.Empty);
    }
}
