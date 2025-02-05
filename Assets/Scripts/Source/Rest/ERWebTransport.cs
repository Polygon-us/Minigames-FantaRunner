using UnityREST;

public class ERWebTransport : WebTransport
{
    private const string XApiKeyHeaderFieldName = "x-api-key";
    
    public ERWebTransport(ERAPIConfig apiConfig, GameConfiguration gameConfiguration) : base(apiConfig)
    {
        if (gameConfiguration.ReleaseType == ReleaseType.Dev)
           HeaderValues[XApiKeyHeaderFieldName] = apiConfig.XApiKeyDev;
        if (gameConfiguration.ReleaseType == ReleaseType.Prod)
           HeaderValues[XApiKeyHeaderFieldName] = apiConfig.XApiKeyProd;
    }
}
