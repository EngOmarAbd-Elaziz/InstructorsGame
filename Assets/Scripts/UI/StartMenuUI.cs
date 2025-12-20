using UnityEngine;
using UnityEngine.UI;

public class StartMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject systemUI;
    [SerializeField] private Button playButton;

    private void Awake()
    {
        playButton.onClick.AddListener(() => { 
            GameManger.Instance.StartMenuInteractButton(); Hide();});
    }

    private void Start()
    {
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
        systemUI.gameObject.SetActive(false);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
        systemUI.gameObject.SetActive(true);
    }
}