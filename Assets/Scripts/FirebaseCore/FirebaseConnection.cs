using FirebaseWebGL.Scripts.FirebaseBridge;
using FirebaseCore.DTOs;
using Newtonsoft.Json;
using UnityEngine;
using System;

namespace FirebaseCore
{
    public class FirebaseConnection : MonoBehaviour
    {
        private const string Room = "A1B1";

        public static Action<UserInputDto> OnUserInput;
        
        public void ListenToDatabaseChanges()
        {
#if !UNITY_EDITOR
            FirebaseDatabase.ListenForValueChanged(Room, gameObject.name, nameof(HandleValueChanged), nameof(HandleError));
#endif
        }

        private void HandleValueChanged(string newValue)
        {
            UserInputDto inputDto = ConvertTo<UserInputDto>(newValue);

            OnUserInput?.Invoke(inputDto);
        }
        
        private void HandleError(string error)
        {
            Debug.LogError(error);
        }

        private void OnDisable()
        {
#if !UNITY_EDITOR
            FirebaseDatabase.StopListeningForValueChanged(Room, gameObject.name, nameof(HandleValueChanged), nameof(HandleError));
#endif
        }
        
        private static T ConvertTo<T>(string obj) where T : class
        {
            return JsonConvert.DeserializeObject<T>(obj);
        }
    }
}