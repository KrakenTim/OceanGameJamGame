using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// static class to manage global variables
/// </summary>
public static class Globals {

    /// <summary>
    /// current main camera
    /// </summary>
    public static Camera currentCamera;
    /// <summary>
    /// player object
    /// </summary>
    public static GameObject player;
    /// <summary>
    /// bulletpool list
    /// </summary>
    public static List<Skill> bulletPool;

    /// <summary>
    /// spawner list
    /// </summary>
    public static List<Enemy_Spawner> spawnerListe;

    public static List<Enemy> enemyList;
}
