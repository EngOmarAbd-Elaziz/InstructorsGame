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

    private HealthSystem currentHealthSystem;
    private void Start()
    {
<<<<<<< HEAD
        HealthSystem healthSystem = playerHealth.healthSystem;

        healthSlider.maxValue = healthSystem.GetHealthMax();
        followUpHealthSlider.maxValue = healthSystem.GetHealthMax();

        healthSlider.value = healthSystem.GetHealth();
        followUpHealthSlider.value = healthSystem.GetHealth();
=======
        playerHealth.OnHealthSystemChanged += PlayerHealth_OnHealthSystemChanged;
>>>>>>> f701581f65074aaba322fb59c267a050345dd216

        SetupHealthSystem();    
    }

    private void PlayerHealth_OnHealthSystemChanged(object sender, EventArgs e)
    {
        SetupHealthSystem();   
    }

    private void SetupHealthSystem() 
    {
        if (currentHealthSystem != null)
        {
            currentHealthSystem.OnHealthChanged -= HealthSystem_OnHealthChanged;
        }

        currentHealthSystem = playerHealth.healthSystem;

        currentHealthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;

        UpdateVisuals();
    }

    private void UpdateVisuals() 
    {
        healthSlider.maxValue = currentHealthSystem.GetHealthMax();
        followUpHealthSlider.maxValue = currentHealthSystem.GetHealthMax();

        healthSlider.value = currentHealthSystem.GetHealth();
        followUpHealthSlider.value = currentHealthSystem.GetHealth();

        healthAmountText.text = Mathf.Floor(currentHealthSystem.GetHealth()).ToString();
        critEffectImage.gameObject.SetActive(false);
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
        if (healthSlider.value != followUpHealthSlider.value)
        {
            followUpHealthSlider.value = Mathf.Lerp(followUpHealthSlider.value, healthSlider.value, lerpSpeed);
        }
    }
    private void OnDestroy()
    {
        // Always clean up!
        if (currentHealthSystem != null)
            currentHealthSystem.OnHealthChanged -= HealthSystem_OnHealthChanged;

        if (playerHealth != null)
            playerHealth.OnHealthSystemChanged -= PlayerHealth_OnHealthSystemChanged;
    }
}