using UnityEngine;
using UnityREST;

[CreateAssetMenu(fileName = "GameConfigurationSO", menuName = "Config/GameConfigurationSO")]
public class GameConfiguration : ScriptableObject
{
    public static readonly GameType GameType = GameType.endless_runner;
    
    [Header("Game Type")]
    [SerializeField] private ReleaseType releaseType;
    
    [Header("Environment Paths")]
    [SerializeField] private APIPaths dev;
    [SerializeField] private APIPaths prod;
    
    public ReleaseType ReleaseType => releaseType;
    
    public APIPaths GetPathByType()
    {
        if (releaseType == ReleaseType.Dev)
            return dev;
        if (releaseType == ReleaseType.Prod)
            return prod;
        
        return null;
    }
}

public enum ReleaseType
{
    Dev,
    Prod
}

public enum GameType
{
    endless_runner
}
