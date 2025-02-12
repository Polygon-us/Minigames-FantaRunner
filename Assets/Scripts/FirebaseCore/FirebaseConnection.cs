using Firebase.Extensions;
using FirebaseCore.DTOs;
using Firebase.Database;
using Newtonsoft.Json;
using UnityEngine;
using Firebase;
using System;

namespace FirebaseCore
{
    public class FirebaseConnection : MonoBehaviour
    {
        private const string Room = "A1B1";

        public static Action<UserInputDto> OnUserInput;

#if !UNITY_EDITOR
        private void Start()
        {
            ListenToDatabaseChanges();
        }

        public void ListenToDatabaseChanges()
        {
#if !UNITY_EDITOR
            FirebaseDatabase.ListenForChildChanged(Room, gameObject.name, nameof(HandleValueChanged), nameof(HandleError));
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
#if UNITY_EDITOR
            FirebaseDatabase.StopListeningForChildChanged(Room, gameObject.name, nameof(HandleValueChanged),
                nameof(HandleError));
#endif
        }

        private static T ConvertTo<T>(string obj) where T : class
        {
            return JsonConvert.DeserializeObject<T>(obj);
        }
#else
        DatabaseReference reference;

        // Start is called before the first frame update
        private void Start()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(_ => {
                reference = FirebaseDatabase.DefaultInstance.GetReference(Room);

                // Listening for changes to child properties of Room
                reference.ChildChanged += HandleChildChanged;
            });
        }

        // Handle child changes under the Room node
        private void HandleChildChanged(object sender, ChildChangedEventArgs e)
        {
            if (e.DatabaseError != null)
            {
                Debug.LogError("Error: " + e.DatabaseError.Message);
                return;
            }

            // Log the changed child key and its new value
            Debug.Log("Child changed: " + e.Snapshot.Key + " -> " + e.Snapshot.Value);
        }

        private void OnDisable()
        {
            reference.ChildChanged -= HandleChildChanged;
        }
    }

#endif
    }