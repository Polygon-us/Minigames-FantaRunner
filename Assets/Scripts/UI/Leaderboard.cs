using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Nakama;
using UnityEngine;

// Prefill the info on the player data, as they will be used to populate the leadboard.
public class Leaderboard : MonoBehaviour
{
    public RectTransform entriesRoot;

    public HighscoreUI playerEntry;

    private List<IApiLeaderboardRecord> _records;

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
        
        _records = await NakamaConnection.Instance.GetLeaderboard();
        IApiLeaderboardRecord playerRecord = await NakamaConnection.Instance.GetPlayerLeaderboard();

        if (playerRecord != null)
        {
            int playerPlace = int.Parse(playerRecord.Rank);
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
            hs.number.text = _records[i].Rank;
            hs.score.text = _records[i].Score;
        }
    }
}