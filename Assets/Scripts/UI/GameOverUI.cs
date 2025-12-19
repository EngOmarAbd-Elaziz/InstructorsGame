using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI winnerText;

    private void Start()
    {
        GameManger.Instance.OnStateChanged += GameManger_OnStateChanged;
        Hide();
    }

    private void Update()
    {
    
    }

    private void GameManger_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManger.Instance.IsGameOver())
        {
            Show();

            if (GameManger.Instance.WinnerID == GameInput.PlayerID.Player1) 
            {
                winnerText.text = "Player 1 Wins";
            }
            else 
            {
                winnerText.text = "Player 2 Wins";
            }
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
