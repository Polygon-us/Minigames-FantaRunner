using UnityEngine.EventSystems;
using FirebaseCore.DTOs;
using FirebaseCore;
using UnityEngine;

public class UIInputMapper : MonoBehaviour
{
    private void OnEnable()
    {
        FirebaseConnection.OnMovementInput += OnMovementInput;
    }
    
    private void OnDisable()
    {
        FirebaseConnection.OnMovementInput -= OnMovementInput;
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnMovementInput(int direction)
    {
        print("Input direction: " + direction);
        
        // 1 -> jump
        // 2 -> slide
        // 3 -> left
        // 4 -> right
        
        AxisEventData eventData = new AxisEventData(EventSystem.current)
        {
            moveVector = direction switch
            {
                1 => Vector2.up,
                2 => Vector2.down,
                3 => Vector2.left,
                4 => Vector2.right,
                _ => Vector2.zero
            },
            moveDir = direction switch
            {
                1 => MoveDirection.Up,
                2 => MoveDirection.Down,
                3 => MoveDirection.Left,
                4 => MoveDirection.Right,
                _ => MoveDirection.None
            }
        };

        ExecuteEvents.Execute(EventSystem.current.currentSelectedGameObject, eventData, ExecuteEvents.moveHandler);
    }
}
