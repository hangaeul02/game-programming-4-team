using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float roundDuration = 10f;

    public float TimeRemaining { get; private set; }
    public bool IsRoundActive { get; private set; }
    public int Score { get; private set; }


    private void Start()
    {
        StartRound();
    }

    public void StartRound()
    {
        Score = 0;
        TimeRemaining = roundDuration;
        IsRoundActive = true;
        Debug.Log("Round started");
    }

    private void Update()
    {
        if (!IsRoundActive) return;

        TimeRemaining = Mathf.Max(0f, TimeRemaining - Time.deltaTime);

        if (TimeRemaining <= 0f)
        {
            IsRoundActive = false;
            Debug.Log("Round finished");
        }
    }
    [ContextMenu("Test Successful Delivery")]
    public void RegisterSuccessfulDelivery()
    {
        if (!IsRoundActive) return;

        Score += 1;
        Debug.Log($"Delivery successful. Score: {Score}");
    }

}