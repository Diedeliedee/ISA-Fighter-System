using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Joeri.Tools.Movement;

public class Player : MonoBehaviour
{
    [Header("Properties:")]
    [SerializeField] private MovementBase.Settings m_movementSettings;

    private PlayerController m_movement = null;

    public void Setup()
    {
        m_movement = new PlayerController(gameObject, m_movementSettings);
    }

    public void Tick(float deltaTime)
    {
        var movementInput = GameManager.instance.latestInput.joystick.vector;

        m_movement.ApplyInput(movementInput, deltaTime);
    }
}
