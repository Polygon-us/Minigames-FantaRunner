using UnityEngine.EventSystems;
using FirebaseCore.DTOs;
using FirebaseCore;
using UnityEngine;

public class UIInputMapper : MonoBehaviour
{
    private void OnEnable()
    {
        FirebaseConnection.OnUserInput += OnUserInput;
    }
    
    private void OnDisable()
    {
        FirebaseConnection.OnUserInput -= OnUserInput;
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnUserInput(UserInputDto input)
    {
        print("Input direction: " + input.direction);
        
        // 1 -> jump
        // 2 -> slide
        // 3 -> left
        // 4 -> right
        
        AxisEventData eventData = new AxisEventData(EventSystem.current);
        eventData.moveVector = input.direction switch
        {
            1 => Vector2.up,
            2 => Vector2.down,
            3 => Vector2.left,
            4 => Vector2.right,
            _ => Vector2.zero
        };

        eventData.moveDir = input.direction switch
        {
            1 => MoveDirection.Up,
            2 => MoveDirection.Down,
            3 => MoveDirection.Left,
            4 => MoveDirection.Right,
            _ => MoveDirection.None
        };
        
        ExecuteEvents.Execute(EventSystem.current.currentSelectedGameObject, eventData, ExecuteEvents.moveHandler);
    }
}
