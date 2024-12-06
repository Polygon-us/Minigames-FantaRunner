using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEnvironmentSO", menuName = "Env/GameEnvironmentSO")]
public class GameEnvironment : ScriptableObject
{
    [Header("Game Type")]
    [SerializeField] private ReleaseType releaseType;
    
    [Header("Environment Paths")]
    [SerializeField] private string dev;
    [SerializeField] private string prod;
    
    [Header("Environment Keys")]
    [SerializeField] private string devKey;
    [SerializeField] private string prodKey;
    
    public string GetPathByType()
    {
        return releaseType switch
        {
            ReleaseType.Dev => dev,
            ReleaseType.Prod => prod,
            _ => null
        };
    }
    
    public string GetKey()
    {
        return releaseType switch
        {
            ReleaseType.Dev => devKey,
            ReleaseType.Prod => prodKey,
            _ => null
        };
    }
}

public enum ReleaseType
{
    Dev,
    Prod
}
