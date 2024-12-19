using Samples.ScriptableObject.Common.Environment;
using Core.Functions.Environment.Handler;
using UnityEngine;

public class EnvironmentInitializer : MonoBehaviour
{
    [SerializeField] private EnvironmentScriptable environmentScriptable;

    private EnvironmentHandler _environmentHandler;

    private void Awake()
    {
        _environmentHandler = new EnvironmentHandler();
        
        string environment = environmentScriptable.GetSelectedEnvironment();
        
        _environmentHandler.EnvironmentConfig(environment);
    }
}