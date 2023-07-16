using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class JoystickInput
{
    private readonly float m_sqrDeadzone = 0f;

    public Direction direction 
    { 
        get; 
        private set; 
    }

    public Vector2Int vector
    {
        get
        {
            switch (direction)
            {
                default:                    return Vector2Int.zero;

                case Direction.Up:          return Vector2Int.up;
                case Direction.Down:        return Vector2Int.down;
                case Direction.Left:        return Vector2Int.left;
                case Direction.Right:       return Vector2Int.right;

                case Direction.TopLeft:     return new Vector2Int(-1, 1);
                case Direction.TopRight:    return new Vector2Int(1, 1);
                case Direction.DownLeft:    return new Vector2Int(-1, -1);
                case Direction.DownRight:   return new Vector2Int(1, -1);
            }
        }
    }

    public Vector2 raw
    { 
        get;
        private set; 
    }

    public JoystickInput(float deadzone)
    {
        m_sqrDeadzone = deadzone * deadzone;
    }
    
    public void Set(StickControl stick)
    {
        var input = stick.ReadValue();

        raw = input;

        if (input.sqrMagnitude < m_sqrDeadzone) direction = ToDirection(input);
        else                                    direction = Direction.None;
    }

    private Direction ToDirection(Vector2 direction)
    {
        if (direction.y > 0)
        {
            if (direction.x > direction.y / 2)  return Direction.TopRight;
            if (direction.x < -direction.y / 2) return Direction.TopRight;
                                                return Direction.Up;
        }
        else if (direction.y < 0)
        {
            if (direction.x > direction.y / 2)  return Direction.DownLeft;
            if (direction.x < -direction.y / 2) return Direction.DownRight;
                                                return Direction.Down;
        }
        else
        {
            if (direction.x > 0)                return Direction.Right;
            if (direction.x < 0)                return Direction.Left;
                                                return Direction.None;
        }
    }

    public enum Direction
    {
        None        = -1,

        Up          = 0,
        Right       = 2,
        Down        = 4,
        Left        = 6,
    
        TopLeft     = 7,
        TopRight    = 1,
        DownLeft    = 5,
        DownRight   = 3,
    }
}