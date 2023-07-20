using UnityEngine;

public class EntityManager : MonoBehaviour
{
    [SerializeField] private Player m_player;

    public void Setup()
    {
        m_player.Setup();
    }

    public void Tick(float deltaTime)
    {
        m_player.Tick(deltaTime);
    }
}
