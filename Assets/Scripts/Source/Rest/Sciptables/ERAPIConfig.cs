using UnityEngine;
using UnityREST;

[CreateAssetMenu(fileName = "New APIConfig", menuName = "UnityREST/ERAPIConfig", order = 1)]
public class ERAPIConfig : APIConfig
{
    [SerializeField] private string xApiKeyDev;
    [SerializeField] private string xApiKeyProd;
    
    public string XApiKeyDev => xApiKeyDev;
    public string XApiKeyProd => xApiKeyProd;
}