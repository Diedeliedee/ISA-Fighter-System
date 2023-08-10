using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] private GameObject m_particleObject;

    public void Setup()
    {
        GameManager.instance.events.onEntityHit += OnHitConnect;
    }

    public void OnHitConnect(HitInstance hit)
    {
        SpawnParticle(hit.connection);
    }

    public void SpawnParticle(Vector2 position)
    {
        var spawnedObject   = Instantiate(m_particleObject, position, Quaternion.identity, transform);
        var component       = spawnedObject.GetComponent<ParticleSystem>();

        Destroy(spawnedObject, component.main.duration);
    }
}
