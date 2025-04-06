using System.Collections.Generic;

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
    /// <summary>
    /// Listado de atributos por nivel y tipo de enemigo
    /// </summary>
    private static readonly Dictionary<(string tag, int nivel), EnemyStats> StatsByTypeAndLevel = new()
    {
        { ("Enemy", 1), new EnemyStats(10f, 100) },
        { ("Enemy", 2), new EnemyStats(20f, 150) },
        { ("Enemy", 3), new EnemyStats(30f, 200) },
        { ("Boss", 1), new EnemyStats(30f, 1000) },
        { ("Boss", 2), new EnemyStats(40f, 1500) },
        { ("Boss", 3), new EnemyStats(50f, 2000) },
        { ("BulletEnemy", 1), new EnemyStats(5f, 0) },
        { ("BulletEnemy", 2), new EnemyStats(15f, 0) },
        { ("BulletEnemy", 3), new EnemyStats(25f, 0) },
        { ("BulletBoss", 1), new EnemyStats(25f, 0) },
        { ("BulletBoss", 2), new EnemyStats(35f, 0) },
        { ("BulletBoss", 3), new EnemyStats(45f, 0) },
    };


    /// <summary>
    /// Obtiene el daño recibido por actor
    /// </summary>
    /// <param name="tag">tag del objeto a buscar</param>
    /// <param name="nivel">nivel del objeto a buscar</param>
    public static float GetDamage(string tag, int nivel = 1)
    {
        return StatsByTypeAndLevel.TryGetValue((tag, nivel), out var stats) ? stats.Damage : 5f;
    }

    /// <summary>
    /// Obtiene el punteo ganado por actor
    /// </summary>
    /// <param name="tag">tag del objeto a buscar</param>
    /// <param name="nivel">nivel del objeto a buscar</param>
    public static int GetPoints(string tag, int nivel = 1)
    {
        return StatsByTypeAndLevel.TryGetValue((tag, nivel), out var stats) ? stats.Points : 10;
    }
}