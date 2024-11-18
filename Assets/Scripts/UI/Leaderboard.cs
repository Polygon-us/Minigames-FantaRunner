using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Nakama;
using UnityEngine;

// Prefill the info on the player data, as they will be used to populate the leadboard.
public class Leaderboard : MonoBehaviour
{
	public RectTransform entriesRoot;
	public int entriesCount;

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
		_records = await NakamaConnection.Instance.GetLeaderboard();
		
		// Start by making all entries enabled & putting player entry last again.
		playerEntry.transform.SetAsLastSibling();
		for(int i = 0; i < entriesCount; ++i)
		{
			entriesRoot.GetChild(i).gameObject.SetActive(true);
		}

		// Find all index in local page space.
		int localStart = 0;
		int place = -1;
		int localPlace = -1;

		place = _records.FindIndex(record => record.OwnerId == NakamaConnection.GetDeviceIdentifier());
		localPlace = place - localStart;

		if (localPlace >= 0 && localPlace < entriesCount)
		{
			playerEntry.gameObject.SetActive(true);
			playerEntry.transform.SetSiblingIndex(localPlace);
		}

		if (_records.Count < entriesCount)
			entriesRoot.GetChild(entriesRoot.transform.childCount - 1).gameObject.SetActive(false);

		int currentHighScore = localStart;

		for (int i = 0; i < entriesCount; ++i)
		{
			HighscoreUI hs = entriesRoot.GetChild(i).GetComponent<HighscoreUI>();

            if (hs == playerEntry || hs == null)
			{
				// We skip the player entry.
				continue;
			}

		    if (_records.Count > currentHighScore)
		    {
		        hs.gameObject.SetActive(true);
		        
		        hs.playerName.text = _records[i].Username;
		        hs.number.text = (localStart + i + 1).ToString();
		        hs.score.text = _records[i].Score;
		        
		        currentHighScore++;
		    }
		    else
		        hs.gameObject.SetActive(false);
		}

		playerEntry.number.text = (place + 1).ToString();
	}
}
