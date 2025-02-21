using System.Collections.Generic;
using Source.DTOs.Response;
using Source.Handlers;
using System.Linq;
using Source.DTOs;
using UnityEngine;
using UnityREST;

// Prefill the info on the player data, as they will be used to populate the leadboard.
public class Leaderboard : MonoBehaviour
{
    public RectTransform entriesRoot;

    public HighscoreUI playerEntry;

    private List<PlayerRankingResponseDto> _records;

    private RankingHandler _rankingHandler;
    
    private void Awake()
    {
        _rankingHandler = new RankingHandler();
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

        _rankingHandler.GetRanking(OnLeaderboardResponse);
    }
    
    private void OnLeaderboardResponse(WebResult<ResponseDto<RankingResponseDto>> response)
    {
        _records = response.data.data.global.ToList();

        PlayerRankingResponseDto playerRecord = response.data.data.player;

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

            hs.Initialize(_records[i]);
        }
    }
}