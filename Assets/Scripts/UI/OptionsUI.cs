using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }

    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI soundEffectText;
    [SerializeField] private TextMeshProUGUI musicText;
    private void Awake()
    {
        Instance = this;
        soundEffectsButton.onClick.AddListener(() => {
            //SoundManger.Instance.ChangeVolume();
            UpdateVisual();
        });
        musicButton.onClick.AddListener(() => {
            //MusicManger.Instance.ChangeVolume();
            UpdateVisual();
        });
        closeButton.onClick.AddListener(() => { Hide(); });
    }
    private void Start()
    {
        GameManger.Instance.OnGameUnPaused += GameManger_OnGameUnPaused;
        UpdateVisual();
        Hide();
    }

    private void GameManger_OnGameUnPaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual() 
    {
        //soundEffectText.text = "Sound Effects: " + Mathf.Round(SoundManger.Instance.GetVolume() * 10f);
        //musicText.text = "Music: " + Mathf.Round(MusicManger.Instance.GetVolume() * 10f);
    }

    public void Show() 
    {
        gameObject.SetActive(true);
    }
    private void Hide() 
    {
        gameObject.SetActive(false);
    }
}
