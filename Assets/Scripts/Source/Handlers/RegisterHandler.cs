using System;
using UnityREST;

public class RegisterHandler : BaseHandler
{
    public static void Register(string fullName, string email, string phone, string username, string id, string address, string city, bool acceptedTerms,
        Action<WebResult<object>> onRegister = null)
    {
        var payload = new RegisterPayload
        {
            acceptedTerms = acceptedTerms,
            fullName = fullName,
            username = username,
            address = address,
            idCard = id,
            email = email,
            phone = phone,
            city = city
        };

        RestApiManager.Instance.PostRequest("register", payload, onRegister);
    }

    public static void UpdateUser(string city, string address, string id, Action<WebResult<object>> onUpdate = null)
    {
        var payload = new UpdatePayload
        {
            address = address,
            idCard = id,
            city = city
        };
        
        RestApiManager.Instance.PatchRequest("update", payload, onUpdate);
    }
}

public class RegisterPayload
{
    public string fullName;
    public string username;
    public string address;
    public string idCard;
    public string email;
    public string phone;
    public string city;
    public bool acceptedTerms;
}

public class UpdatePayload
{
    public string address;
    public string idCard;
    public string city;
}
