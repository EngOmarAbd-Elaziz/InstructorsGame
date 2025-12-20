using System;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private PlayerHealth player1Health;
    [SerializeField] private PlayerHealth player2Health;
    [SerializeField] private Transform p1SpawnPoint;
    [SerializeField] private Transform p2SpawnPoint;

    public static GameManger Instance { get; private set; }

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnPaused;
    public event EventHandler OnScoreChanged;

    private enum State
    {
        WaitingToStart,
        CountDownToStart,
        GamePlaying,
        RoundOver,
        GameOver,
    }

    public bool isGamePaused = false;
    private State state;
    private float WaitingToStartTimer = 1f;
    private float countDownToStartTimer = 3f;
    private float gamePlayingToStartTimer = 30f;
    private float gamePlayingToStartTimerMax = 3f;
    private float roundOverTimer = 2f;
    
    private int winsNeeded = 3;
    public int player1Wins {  get; private set; }
    public int player2Wins {  get; private set; }
    public GameInput.PlayerID WinnerID { get; private set; }

    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
    }

    private void Start()
    {
        if (GameInput.Instance != null)
        {
            GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        }
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        TogglePauseGame();
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                //WaitingToStartTimer -= Time.deltaTime;
                //if (WaitingToStartTimer < 0f)
                //{
                //    state = State.CountDownToStart;
                //    OnStateChanged?.Invoke(this, EventArgs.Empty);
                //}
                break;

            case State.CountDownToStart:
                countDownToStartTimer -= Time.deltaTime;
                if (countDownToStartTimer < 0f)
                {
                    state = State.GamePlaying;
                    gamePlayingToStartTimer = gamePlayingToStartTimerMax;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;

            // Inside GameManger.cs -> Update()

            case State.GamePlaying:
                gamePlayingToStartTimer -= Time.deltaTime;
                if (gamePlayingToStartTimer < 0f)
                {
                    float p1Health = player1Health.healthSystem.GetHealth();
                    float p2Health = player2Health.healthSystem.GetHealth();
                    if (p1Health > p2Health)
                    {
                        ProcessRoundWin(GameInput.PlayerID.Player1);
                    }
                    else if (p2Health > p1Health)
                    {
                        ProcessRoundWin(GameInput.PlayerID.Player2);
                    }
                    else
                    {
                        // Handle DRAW (Equal Health)
                        state = State.RoundOver;
                        roundOverTimer = 2f;
                        OnStateChanged?.Invoke(this, EventArgs.Empty);
                        Debug.Log("DRAW!");
                    }
                }
                break;

            case State.RoundOver:
                roundOverTimer -= Time.deltaTime;
                if (roundOverTimer < 0f) 
                {
                    countDownToStartTimer = 3f; // reset timer
                    state = State.CountDownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                    ResetPlayers(); // health and positions
                }
                
                break;

            case State.GameOver:
                break;
        }
        Debug.Log(state);
    }

    // Call this method from your Health System when a player dies
    public void ProcessRoundWin(GameInput.PlayerID roundWinner)
    {
        if (state != State.GamePlaying) return; // Prevent double triggers

        if (roundWinner == GameInput.PlayerID.Player1) player1Wins++;
        else player2Wins++;

        // Notify UI to update the "shining lights"
        OnScoreChanged?.Invoke(this, EventArgs.Empty);

        // Check for Game Win (Best of 5)
        if (player1Wins >= winsNeeded || player2Wins >= winsNeeded)
        {
            WinnerID = roundWinner; // Record who won the whole match
            state = State.GameOver;
            OnStateChanged?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            // Just a round win, not game win
            state = State.RoundOver;
            roundOverTimer = 2f;
            OnStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }

    public bool IsCountDownToStartActive()
    {
        return state == State.CountDownToStart;
    }

    public float GetCountDownToStartTimer()
    {
        return countDownToStartTimer;
    }

    public float GetGamePlayingToStartTimer() 
    {
        return gamePlayingToStartTimer;
    }
    public bool IsGameOver()
    {
        return state == State.GameOver;
    }

    public float GetGamePlayingClockUINormalized()
    {
        return 1 - (gamePlayingToStartTimer / gamePlayingToStartTimerMax);
    }

    public void TogglePauseGame()
    {
        isGamePaused = !isGamePaused;
        if (isGamePaused)
        {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            OnGameUnPaused?.Invoke(this, EventArgs.Empty);
        }
    }

    private void ResetPlayers()
    {
        // 1. Reset Health
        player1Health.ResetHealth();
        player2Health.ResetHealth();

        // 2. Reset Positions
        // We disable the CharacterController or Rigidbody temporarily to prevent 
        // physics interference during the "teleport"
        Rigidbody p1Rb = player1Health.GetComponent<Rigidbody>();
        Rigidbody p2Rb = player2Health.GetComponent<Rigidbody>();

        if (p1Rb != null) p1Rb.linearVelocity = Vector3.zero;
        if (p2Rb != null) p2Rb.linearVelocity = Vector3.zero;

        player1Health.transform.position = p1SpawnPoint.position;
        player2Health.transform.position = p2SpawnPoint.position;

        // 3. Reset Rotations (Make them face each other)
        player1Health.transform.rotation = p1SpawnPoint.rotation;
        player2Health.transform.rotation = p2SpawnPoint.rotation;
    }

    public void StartMenuInteractButton()
    {
        // Only allow this if we are actually waiting
        if (state == State.WaitingToStart)
        {
            state = State.CountDownToStart;
            OnStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
