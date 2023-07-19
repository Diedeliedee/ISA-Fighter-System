using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public struct JoystickInput
{
    public Direction    direction;
    public Vector2Int   vector;
    public Vector2      raw;

    public JoystickInput(StickControl stick)
    {
        raw         = stick.ReadValue();
        direction   = ToDirection(raw);
        vector      = ToVector(direction);
    }

    public JoystickInput(DpadControl dpad)
    {
        vector = Vector2Int.zero;

        if (dpad.up.isPressed)      vector.y++;
        if (dpad.down.isPressed)    vector.y--;
        if (dpad.left.isPressed)    vector.x--;
        if (dpad.right.isPressed)   vector.x++;

        raw         = vector;
        direction   = ToDirection(vector);
    }

    public JoystickInput(KeyCode up, KeyCode down, KeyCode left, KeyCode right)
    {
        vector = Vector2Int.zero;

        if (Input.GetKey(up))       vector.y++;
        if (Input.GetKey(down))     vector.y--;
        if (Input.GetKey(left))     vector.x--;
        if (Input.GetKey(right))    vector.x++;

        raw         = vector;
        direction   = ToDirection(vector);
    }

    public override bool Equals(object? obj)
    {
        return obj is JoystickInput other && Equals(other);
    }

    public bool Equals(JoystickInput other)
    {
        return direction == other.direction;
    }

    public static bool operator ==(JoystickInput lhs, JoystickInput rhs)
    {
        return lhs.Equals(rhs);
    }

    public static bool operator !=(JoystickInput lhs, JoystickInput rhs)
    {
        return !lhs.Equals(rhs);
    }

    private static Direction ToDirection(Vector2 direction)
    {
        if (direction.y > 0)
        {
            if (direction.x > direction.y / 2)  return Direction.TopRight;
            if (direction.x < -direction.y / 2) return Direction.TopRight;
                                                return Direction.Up;
        }
        else if (direction.y < 0)
        {
            if (direction.x > -direction.y / 2) return Direction.DownLeft;
            if (direction.x < direction.y / 2)  return Direction.DownRight;
                                                return Direction.Down;
        }
        else
        {
            if (direction.x > 0)                return Direction.Right;
            if (direction.x < 0)                return Direction.Left;
                                                return Direction.None;
        }
    }

    private static Vector2Int ToVector(Direction direction)
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