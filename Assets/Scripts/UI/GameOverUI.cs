using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI winnerText;
    [SerializeField] private Button playAgainButton;

    private void Start()
    {
        GameManger.Instance.OnStateChanged += GameManger_OnStateChanged;
        Hide();

        playAgainButton.onClick.AddListener( () => { RestartGame(); });
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

    private void RestartGame()
    {
        int currentIdx = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIdx);
    }
}
