using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;

    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider followUpHealthSlider;
    private float lerpSpeed = 0.05f;

    [SerializeField] private Image critEffectImage;
    [SerializeField] private TextMeshProUGUI healthAmountText;

    private void Start()
    {
        HealthSystem healthSystem = playerHealth.healthSystem;
        
        healthSlider.maxValue = healthSystem.GetHealthMax();   
        followUpHealthSlider.maxValue = healthSystem.GetHealthMax();
        
        healthSlider.value = healthSystem.GetHealth();
        followUpHealthSlider.value = healthSystem.GetHealth();

        healthAmountText.text = Mathf.Floor(healthSystem.GetHealth()).ToString();
        critEffectImage.gameObject.SetActive(false);

        playerHealth.healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
    }

    private void HealthSystem_OnHealthChanged(object sender, EventArgs e)
    {
        healthSlider.value = playerHealth.healthSystem.GetHealth();
        healthAmountText.text = healthSlider.value.ToString();

        if (playerHealth.healthSystem.GetHealthPercent() < 0.25f)
        {
            critEffectImage.gameObject.SetActive(true);
        }
        else { critEffectImage.gameObject.SetActive(false); }
    }

    private void Update()
    {   
        if(healthSlider.value != followUpHealthSlider.value)
        {
            followUpHealthSlider.value = Mathf.Lerp(followUpHealthSlider.value, healthSlider.value, lerpSpeed);
        }
    }
}