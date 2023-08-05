using UnityEngine;

public class EntityManager : MonoBehaviour
{
    [SerializeField] private Player m_player;
    [SerializeField] private PunchingBag m_punchingBag;

    public void Setup()
    {
        m_player.Setup();
        m_punchingBag.Setup();
    }

    public void Tick()
    {
        m_player.Tick();
        m_punchingBag.Tick();
    }
}
