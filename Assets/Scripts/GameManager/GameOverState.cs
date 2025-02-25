using Source.DTOs.Request;
using Source.Handlers;
using TMPro;
using UnityEngine;
using UnityREST;
#if UNITY_ANALYTICS
using UnityEngine.Analytics;
#endif

/// <summary>
/// state pushed on top of the GameManager when the player dies.
/// </summary>
public class GameOverState : AState
{
    public TrackManager trackManager;
    public Canvas canvas;
    public MissionUI missionPopup;

	public AudioClip gameOverTheme;

	public Leaderboard miniLeaderboard;
	public Leaderboard fullLeaderboard;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text distanceText;
    
    private RankingHandler _rankingHandler;

    private void Awake()
    {
        _rankingHandler = new RankingHandler();
    }

    public override void Enter(AState from)
    {
        canvas.gameObject.SetActive(true);

        scoreText.text = trackManager.score.ToString();
        distanceText.text = trackManager.worldDistance.ToString("N0");
        
        SendLeaderboard();
        SendCheckpoints();
        
        if (MissionManager.Instance.AnyMissionComplete())
            StartCoroutine(missionPopup.Open());
        else
            missionPopup.gameObject.SetActive(false);

		CreditCoins();
        
		if (MusicPlayer.instance.GetStem(0) != gameOverTheme)
		{
            MusicPlayer.instance.SetStem(0, gameOverTheme);
			StartCoroutine(MusicPlayer.instance.RestartAllStems());
        }
    }

	public override void Exit(AState to)
    {
        canvas.gameObject.SetActive(false);
        FinishRun();
    }

    public override string GetName()
    {
        return "GameOver";
    }

    public override void Tick()
    {
        
    }

	public void OpenLeaderboard()
	{
		fullLeaderboard.Open();
    }

	public void GoToStore()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("shop", UnityEngine.SceneManagement.LoadSceneMode.Additive);
    }

    public void GoToLoadout()
    {
        trackManager.isRerun = false;
		manager.SwitchState("Loadout");
    }

    public void OpenRappi()
    {
        
    }
    
    public void RunAgain()
    {
        trackManager.isRerun = false;
        manager.SwitchState("Game");
    }

    protected void CreditCoins()
	{
		PlayerData.instance.Save();

#if UNITY_ANALYTICS // Using Analytics Standard Events v0.3.0
        var transactionId = System.Guid.NewGuid().ToString();
        var transactionContext = "gameplay";
        var level = PlayerData.instance.rank.ToString();
        var itemType = "consumable";
        
        if (trackManager.characterController.coins > 0)
        {
            AnalyticsEvent.ItemAcquired(
                AcquisitionType.Soft, // Currency type
                transactionContext,
                trackManager.characterController.coins,
                "fishbone",
                PlayerData.instance.coins,
                itemType,
                level,
                transactionId
            );
        }

        if (trackManager.characterController.premium > 0)
        {
            AnalyticsEvent.ItemAcquired(
                AcquisitionType.Premium, // Currency type
                transactionContext,
                trackManager.characterController.premium,
                "anchovies",
                PlayerData.instance.premium,
                itemType,
                level,
                transactionId
            );
        }
#endif 
	}

    private void SendLeaderboard()
    {
        RankingDto rankingDto = new RankingDto
        {
            score = trackManager.score,
        };
        
        _rankingHandler.PostRanking(rankingDto, OnRankingPosted);
    }

    private void OnRankingPosted(WebResult<object> _)
    {
        miniLeaderboard.Populate();
    }

    private void SendCheckpoints()
    {
        CheckpointsHandler.SendCheckpoints(trackManager.CheckpointTimeline);
    }
    
	protected void FinishRun()
    {
        CharacterCollider.DeathEvent de = trackManager.characterController.characterCollider.deathData;
        //register data to analytics
#if UNITY_ANALYTICS
        AnalyticsEvent.GameOver(null, new Dictionary<string, object> {
            { "coins", de.coins },
            { "premium", de.premium },
            { "score", de.score },
            { "distance", de.worldDistance },
            { "obstacle",  de.obstacleType },
            { "theme", de.themeUsed },
            { "character", de.character },
        });
#endif

        PlayerData.instance.Save();
        
        trackManager.End();
    }

    //----------------
}
