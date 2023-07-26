using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Joeri.Tools.Debugging;

[System.Serializable]
public class Hurtbox
{
                public Vector2  position;
    [Min(0f)]   public float    width;
    [Min(0f)]   public float    height;

    /// <returns>True if one ore more 2D colliders overlap with this hurtbox in the specified layermask.</returns>
    public bool Hit(LayerMask mask, out Collider2D[] collider)
    {
        collider = Physics2D.OverlapBoxAll(position, new Vector2(width, height), 0f, mask);
        return (collider != null);
    }

    /// <summary>
    /// Draw the hurtbox as a flat 2D red rectangle.
    /// </summary>
    public void Draw(float zPos)
    {
        var pos     = new Vector3(position.x, position.y, zPos);
        var size    = new Vector3(width, height, 0f);

        GizmoTools.DrawOutlinedBox(pos, size, Color.red, 0.5f, true, 0.5f);
    }
}
