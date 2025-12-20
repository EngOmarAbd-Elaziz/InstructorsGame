using TMPro;
using UnityEngine;

public class GameStartCountDownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countDownText;

    private void Start()
    {
        GameManger.Instance.OnStateChanged += GameManger_OnStateChanged;
        Hide();
    }

    private void Update()
    {
                                                       // ToString("#.##") for 2 decimal places
        countDownText.text = Mathf.Ceil(GameManger.Instance.GetCountDownToStartTimer()).ToString();
    }

    private void GameManger_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManger.Instance.IsCountDownToStartActive())
        {
            Show();
        }
        else Hide();
    }

    private void Show() 
    {
        gameObject.SetActive(true);
    }

    private void Hide() 
    {
        gameObject.SetActive(false);
    }
}