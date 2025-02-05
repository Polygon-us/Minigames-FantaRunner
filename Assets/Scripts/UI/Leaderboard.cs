using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityREST;

// Prefill the info on the player data, as they will be used to populate the leadboard.
public class Leaderboard : MonoBehaviour
{
    public RectTransform entriesRoot;

    public HighscoreUI playerEntry;

    private List<LeaderboardRecordDto> _records;

    private RankingHandler _leaderboardHandler;
    
    private void Awake()
    {
        _leaderboardHandler = new RankingHandler();
    }

    public void Open()
    {
        gameObject.SetActive(true);

        Populate().Forget();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public async UniTaskVoid Populate()
    {
        for (int i = 0; i < entriesRoot.childCount; ++i)
        {
            entriesRoot.GetChild(i).gameObject.SetActive(false);
        }

        WebResult<RankingListResponse> response = await _leaderboardHandler.GetRanking();
        
        _records = response.Data.Records.ToList();
        
        ResultResponse<LeaderboardRecordDto> playerRecord = await  _leaderboardHandler.ListPlayerLeaderboard();

        if (playerRecord != null)
        {
            int playerPlace = playerRecord.Data.Rank;
            int lastIndex = Mathf.Min(entriesRoot.childCount, playerPlace) - 1;
            
            playerEntry.transform.SetSiblingIndex(lastIndex);
            
            if (playerPlace >= entriesRoot.childCount)
                _records.Insert(lastIndex, playerRecord);
        }
        
        for (int i = 0; i < entriesRoot.childCount && i < _records.Count; ++i)
        {
            HighscoreUI hs = entriesRoot.GetChild(i).GetComponent<HighscoreUI>();
            
            hs.gameObject.SetActive(true);

            hs.playerName.text = _records[i].Username;
            hs.number.text = _records[i].Rank.ToString();
            hs.score.text = _records[i].Score.ToString();
        }
    }
}