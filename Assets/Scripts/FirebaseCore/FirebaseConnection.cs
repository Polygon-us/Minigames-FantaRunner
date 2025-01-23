using System;
using FirebaseWebGL.Scripts.FirebaseBridge;
using System.Collections.Generic;
using FirebaseCore.DTOs;
using Newtonsoft.Json;
using UnityEngine;

namespace FirebaseCore
{
    public class FirebaseConnection : MonoBehaviour
    {
        private string room = "A1B1";
        
        public void ListenToDatabaseChanges()
        {
            FirebaseDatabase.ListenForValueChanged(room, gameObject.name, nameof(HandleValueChanged), nameof(HandleError));
            print($"Started ListenToDatabaseChanges to {room}");
        }

        private void HandleValueChanged(string newValue)
        {
            UserInputDto inputDto = ConvertTo<UserInputDto>(newValue);

            print($"count: {inputDto.count}\n" +
                  $"direction: {inputDto.direction}");
        }
        
        private void HandleError(string error)
        {
            Debug.LogError(error);
        }

        private void OnDisable()
        {
            FirebaseDatabase.StopListeningForValueChanged(room, gameObject.name, nameof(HandleValueChanged), nameof(HandleError));
        }
        
        private static T ConvertTo<T>(Dictionary<string, object> dictionary) where T : class
        {
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(dictionary));
        }
        
        private static T ConvertTo<T>(object obj) where T : class
        {
            return ConvertTo<T>((Dictionary<string, object>)obj);
        }
    }
}