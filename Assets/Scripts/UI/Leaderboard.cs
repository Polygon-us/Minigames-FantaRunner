using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityREST;

// Prefill the info on the player data, as they will be used to populate the leadboard.
public class Leaderboard : MonoBehaviour
{
    public RectTransform entriesRoot;

    public HighscoreUI playerEntry;

    private List<PlayerRanking> _records;

    private RankingHandler _leaderboardHandler;
    
    private void Awake()
    {
        _leaderboardHandler = new RankingHandler();
    }

    public void Open()
    {
        gameObject.SetActive(true);

        Populate();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void Populate()
    {
        for (int i = 0; i < entriesRoot.childCount; ++i)
        {
            entriesRoot.GetChild(i).gameObject.SetActive(false);
        }

        _leaderboardHandler.GetRanking(OnRankingReceived);
    }

    private void OnRankingReceived(WebResult<RankingListResponse> response)
    {
        _records = response.data.data.global.ToList();
        PlayerRanking playerRecord = response.data.data.player;
        
        if (playerRecord != null)
        {
            int playerPlace = playerRecord.rank;
            int lastIndex = Mathf.Min(entriesRoot.childCount, playerPlace) - 1;
            
            playerEntry.transform.SetSiblingIndex(lastIndex);
            
            if (playerPlace >= entriesRoot.childCount)
                _records.Insert(lastIndex, playerRecord);
        }
        
        for (int i = 0; i < entriesRoot.childCount && i < _records.Count; ++i)
        {
            HighscoreUI hs = entriesRoot.GetChild(i).GetComponent<HighscoreUI>();
            
            hs.gameObject.SetActive(true);

            hs.playerName.text = _records[i].username;
            hs.number.text = _records[i].rank.ToString();
            hs.score.text = _records[i].distance.ToString();
        }
    }
}