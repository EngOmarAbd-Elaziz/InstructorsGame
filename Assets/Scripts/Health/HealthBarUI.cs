using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    private HealthSystem healthSystem;
    private int healthAmount = 100;
    public event EventHandler OnLowHealthReached;

    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider followUpHealthSlider;
    private float lerpSpeed = 0.05f;

    [SerializeField] private Button damageButton;
    [SerializeField] private Button healButton;

    [SerializeField] private Image critEffectImage;
    [SerializeField] private TextMeshProUGUI healthAmountText;

    private void Start()
    {
        healthSystem = new HealthSystem(healthAmount);
        
        healthSlider.maxValue = healthSystem.GetHealthMax();   
        followUpHealthSlider.maxValue = healthSystem.GetHealthMax();
        
        healthSlider.value = healthSystem.GetHealth();
        followUpHealthSlider.value = healthSystem.GetHealth();

        healthAmountText.text = healthAmount.ToString();
        critEffectImage.gameObject.SetActive(false);

        damageButton.onClick.AddListener( () => { healthSystem.Damage(10);} );
        healButton.onClick.AddListener( () => {healthSystem.Heal(10);} );
    }

    private void Update()
    {
        if(healthSlider.value != healthSystem.GetHealth())
        {
            healthSlider.value = healthSystem.GetHealth();
            healthAmountText.text = healthSlider.value.ToString();
        }
        
        if(healthSlider.value != followUpHealthSlider.value)
        {
            followUpHealthSlider.value = Mathf.Lerp(followUpHealthSlider.value, healthSystem.GetHealth(), lerpSpeed);
            healthAmountText.text = healthSlider.value.ToString();
        }

        if (healthSystem.GetHealthPercent() < 0.25f)
        {
            critEffectImage.gameObject.SetActive(true);
            OnLowHealthReached?.Invoke(this, EventArgs.Empty);
        }
        else { critEffectImage.gameObject.SetActive(false); }
    }
}