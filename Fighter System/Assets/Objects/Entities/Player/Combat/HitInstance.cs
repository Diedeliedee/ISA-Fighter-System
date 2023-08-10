using UnityEngine;

public struct HitInstance
{
    public readonly Vector2 connection;

    public readonly int hitPause;
    public readonly int damage;

    public HitInstance(MoveConcept move, Collider2D collider, Hurtbox hurtbox)
    {
        connection = collider.ClosestPoint(hurtbox.position);

        hitPause    = move.hitPause;
        damage      = move.damage;
    }
}
