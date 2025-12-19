using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayingClockUI : MonoBehaviour
{
    [SerializeField] private Image timerImage;
    [SerializeField] private TextMeshProUGUI timerText;

    private void Update()
    {
        timerImage.fillAmount = GameManger.Instance.GetGamePlayingClockUINormalized();
        timerText.text = Mathf.Ceil(GameManger.Instance.GetGamePlayingToStartTimer()).ToString();
    }
}
