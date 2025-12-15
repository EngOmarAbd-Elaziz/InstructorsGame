using System;
using UnityEngine;


// this class is a middle man between abilites and healthBar
public class PlayerHealth : MonoBehaviour
{
    public HealthSystem healthSystem;
    [SerializeField] private float healthAmount = 100f;
    private void Awake()
    {
        healthSystem = new HealthSystem(healthAmount);
    }
}
