using System;
using Newtonsoft.Json;
using UnityEngine;
using UnityREST;

[DefaultExecutionOrder(-200)]
public class RestApiManager : APIManager
{
    [SerializeField] private GameConfiguration gameConfig;

    private ERWebTransport _erWebTransport;

    private static RestApiManager _instance;

    public static RestApiManager Instance
    {
        get
        {
            if (_instance)
                return _instance;

            GameObject go = Resources.Load<GameObject>("Prefabs/RestApi");

            _instance = go.GetComponent<RestApiManager>();

            return _instance;
        }
    }

    protected override void Awake()
    {
        if (!_instance)
        {
            _instance = this;
            transform.SetParent(null);

            DontDestroyOnLoad(_instance.gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        apiPaths = gameConfig.GetPathByType();

        base.Awake();

        _erWebTransport = new ERWebTransport(apiConfig as ERAPIConfig, gameConfig);
    }

    public void SetAuthToken(string token)
    {
        _erWebTransport.SetAuthToken(token);
    }

    public void GetRequest<T>(string endpoint, Action<WebResult<T>> callback = null, string[] args = null)
    {
        StartRequest(endpoint, path => _erWebTransport.GET(PathWithArgs(path, args), callback));
    }

    public void PostRequest<T>(string endpoint, object data, Action<WebResult<T>> callback = null, string[] args = null)
    {
        StartRequest(endpoint, path => _erWebTransport.POST(PathWithArgs(path, args), JsonConvert.SerializeObject(data), callback));
    }

    public void PatchRequest<T>(string endpoint, object data, Action<WebResult<T>> callback)
    {
        StartRequest(endpoint, path => _erWebTransport.PATCH(path, JsonConvert.SerializeObject(data), callback));
    }

    public static string GetErrorResponse(string json)
    {
        return JsonConvert.DeserializeObject<ErrorResponse>(json).error;
    }

    private void OnDestroy()
    {
        _erWebTransport?.SignOut();
    }

    private string PathWithArgs(string path, string[] args)
    {
        string pathWithArgs = path + (args != null ? $"?{string.Join("&", args)}" : "");
        return pathWithArgs;
    }
}

[Serializable]
public class ErrorResponse
{
    public string error;
}