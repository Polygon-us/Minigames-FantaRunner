using Samples.ScriptableObject.Common.Environment;
using Core.Functions.Environment.Handler;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EnvironmentInitializer : MonoBehaviour
{
    [SerializeField] private EnvironmentScriptable environmentScriptable;

    private EnvironmentHandler _environmentHandler;
    
    private async void Start()
    {
        _environmentHandler = new EnvironmentHandler();
        
        string environment = environmentScriptable.GetSelectedEnvironment();
        
        await _environmentHandler.EnvironmentConfig(environment);
        
        SceneManager.LoadScene(1);
    }
}