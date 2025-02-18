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

        public static Action<int> OnMovementInput;
        public static Action OnSubmitInput;
        private void Start()
        {
            ListenToDatabaseChanges();
        }

        public void ListenToDatabaseChanges()
        {
            FirebaseDatabase.ListenForChildChanged(Room, gameObject.name, nameof(HandleValueChanged), nameof(HandleError));
        }

        private void HandleValueChanged(string data)
        {
            ChangedDataDto dataDto = ConvertTo<ChangedDataDto>(data);
            
            switch (dataDto.key)
            {
                case nameof(UserInputDto.direction):
                    OnMovementInput?.Invoke(int.Parse(dataDto.value));
                    break;
                case nameof(UserInputDto.submit):
                    OnSubmitInput?.Invoke();
                    break;
            }
        }

        private void HandleError(string error)
        {
            Debug.LogError(error);
        }

        private void OnDisable()
        {
            FirebaseDatabase.StopListeningForChildChanged(Room, gameObject.name, nameof(HandleValueChanged),
                nameof(HandleError));
        }

        private static T ConvertTo<T>(string obj) where T : class
        {
            return JsonConvert.DeserializeObject<T>(obj);
        }
    }
}