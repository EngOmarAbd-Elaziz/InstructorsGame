using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Need this to reference Image type

public class RoundScoreUI : MonoBehaviour
{
    // 1. Choose which player this UI belongs to
    [SerializeField] private GameInput.PlayerID playerID;

    // 2. A list to hold the specific "GoldenFillImage" objects
    [SerializeField] private List<Image> goldenFillImages;

    private void Start()
    {
        // Subscribe to the event when scores change
        GameManger.Instance.OnScoreChanged += GameManager_OnScoreChanged;

        // Run once at start to set the initial state (all empty)
        UpdateVisuals();
    }

    private void GameManager_OnScoreChanged(object sender, System.EventArgs e)
    {
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        // 1. Get the correct score based on the ID we set in the inspector
        int currentScore = 0;
        if (playerID == GameInput.PlayerID.Player1)
        {
            currentScore = GameManger.Instance.player1Wins;
        }
        else
        {
            currentScore = GameManger.Instance.player2Wins;
        }

        // 2. Loop through all the golden images in our list
        for (int i = 0; i < goldenFillImages.Count; i++)
        {
            if (i < currentScore)
            {
                goldenFillImages[i].gameObject.SetActive(true);
            }
            else
            {
                goldenFillImages[i].gameObject.SetActive(false);
            }
        }
    }

    // Always unsubscribe from events when the object is destroyed
    //private void OnDestroy()
    //{
    //    if (GameManger.Instance != null)
    //    {
    //        GameManger.Instance.OnScoreChanged -= GameManager_OnScoreChanged;
    //    }
    //}
}
