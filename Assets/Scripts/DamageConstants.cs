using System.Collections.Generic;
using UnityEngine;

public class EnemyStats
{
    public float Damage { get; set; }
    public int Points { get; set; }

    public EnemyStats(float damage, int points)
    {
        Damage = damage;
        Points = points;
    }
}

public static class DamageConstants
{
    private static readonly Dictionary<string, EnemyStats> StatsByTag = new()
    {
        { "BulletEnemy", new EnemyStats(15f, 0) },
        { "Enemy", new EnemyStats(25f, 100) },
        { "Boss", new EnemyStats(100f, 1000) }
    };

    /// <summary>
    /// Obtiene el daño recibido por actor
    /// </summary>
    public static float GetDamage(string tag)
    {
        return StatsByTag.TryGetValue(tag, out var stats) ? stats.Damage : 10f;
    }

    /// <summary>
    /// Obtiene el punteo ganado por actor
    /// </summary>
    public static int GetPoints(string tag)
    {
        return StatsByTag.TryGetValue(tag, out var stats) ? stats.Points : -1;
    }
}