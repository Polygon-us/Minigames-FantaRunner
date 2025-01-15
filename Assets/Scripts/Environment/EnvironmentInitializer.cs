using Samples.ScriptableObject.Common.Environment;
using Core.Functions.Environment.Handler;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EnvironmentInitializer : MonoBehaviour
{
    [SerializeField] private EnvironmentScriptable environmentScriptable;

    private EnvironmentHandler _environmentHandler;
    
    public static bool IsInitialized { get; private set; }
    
    private async void Start()
    {
        _environmentHandler = new EnvironmentHandler();
        
        string environment = environmentScriptable.GetSelectedEnvironment();
        
        await _environmentHandler.EnvironmentConfig(environment);
        
        IsInitialized = true;
        
        SceneManager.LoadScene(1);
    }

    public static void EnsureInitialized()
    {
        if (IsInitialized)
            return;
        
        SceneManager.LoadScene(0);
    }
}