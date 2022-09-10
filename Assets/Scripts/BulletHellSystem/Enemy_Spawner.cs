using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class to control the spawning of enemys
/// </summary>
public class Enemy_Spawner : MonoBehaviour {

    /// <summary>
    /// list of enemies to spawn
    /// </summary>
    public List<Enemy_Spawner_Info> enemysToSpawn;
    /// <summary>
    /// max amount of enemies to spawn
    /// </summary>
    public int spawnLimit;
    private int currentSpawnCount;

    /// <summary>
    /// start delay
    /// </summary>
    public float delay;
    /// <summary>
    /// trigger area to activate the spawner
    /// </summary>
    public GameObject triggerArea;
    /// <summary>
    /// uses the trigger area to start the spawner
    /// </summary>
    public bool useTriggerArea;
    private bool isActive;








    /// <summary>
    /// startet all stopped spawner anew if they can be started again
    /// </summary>
    void Update() {
        if (isActive == true) {


            // spawnen neustartet nachdem sie gestopped sind, wenn das spawnlimit erreicht wurde
            if (spawnLimit > currentSpawnCount || spawnLimit == 0) {
                foreach (Enemy_Spawner_Info e in enemysToSpawn) {
                    if (e.SpawnConditonFulfilled == true && e.SpawnStartet == false) {
                        StartCoroutine(startSpawntimer(e.delay, e));
                        e.SpawnStartet = true;
                    }
                }
            }

        }
    }

    /// <summary>
    /// starts the activation time of the spawner itself
    /// </summary>
    private void OnEnable() {
        isActive = false;
        currentSpawnCount = 0;

        if (Globals.spawnerListe == null) {
            Globals.spawnerListe = new List<Enemy_Spawner>();
        }

        Globals.spawnerListe.Add(this);


        if (useTriggerArea == true) {
            return;
        }
        else {

            StartCoroutine(spawnerActivationTimer(delay));
        }
    }


    /// <summary>
    /// starts  all spawn timer, who do not need a trigger area
    /// </summary>
    private void activateSpawning() {

        isActive = true;


        foreach (Enemy_Spawner_Info e in enemysToSpawn) {
            if (e.useTriggerArea == false && e.SpawnStartet == false && e.SpawnConditonFulfilled == false) {
                StartCoroutine(startSpawntimer(e.delay, e));
                e.SpawnStartet = true;
            }

        }

    }


    /// <summary>
    /// removes the spawner from the Global spawner list
    /// </summary>
    private void OnDisable() {
        Globals.spawnerListe.Remove(this);
        isActive = false;
        currentSpawnCount = 0;
    }

    /// <summary>
    /// spawn timer who spawns enemies
    /// </summary>
    /// <param name="wait"> time in seconds for enemy spawn</param>
    /// <param name="enemySpawnInfo"> information which enemy and how many are to be spawned</param>
    /// <returns></returns>
    private IEnumerator startSpawntimer(float wait, Enemy_Spawner_Info enemySpawnInfo) {



        yield return new WaitForSeconds(wait);
        // spawn
        if (spawnLimit > currentSpawnCount || spawnLimit == 0) {

            currentSpawnCount = currentSpawnCount + 1;
            GameObject g = Instantiate(enemySpawnInfo.enemyPrefab, transform);



            g.layer = (int)Layer_enum.enemy;
            // callback setzten, um spawncounter zu verringern
            g.GetComponentInChildren<Enemy>(true).SpawnerCallback = this;



            if (enemySpawnInfo.enemysToSpawn == 0 || enemySpawnInfo.enemysToSpawn > enemySpawnInfo.CurrentEnemysSpawned + 1) {
                enemySpawnInfo.CurrentEnemysSpawned = enemySpawnInfo.CurrentEnemysSpawned + 1;
                StartCoroutine(startSpawntimer(wait, enemySpawnInfo));
            }
            else {
                // spawner fertig
                enemySpawnInfo.SpawnConditonFulfilled = false;

            }
        }
        else {
            enemySpawnInfo.SpawnStartet = false;
        }


    }

    /// <summary>
    /// timer to start the spawner itself
    /// </summary>
    /// <param name="wait"> delay in seconds</param>
    /// <returns></returns>
    private IEnumerator spawnerActivationTimer(float wait) {

        if (wait != 0) {
            yield return new WaitForSeconds(wait);
        }
        activateSpawning();

    }




    /// <summary>
    /// check spawntimer who react to trigger areas
    /// </summary>
    /// <param name="trigger"> trigger area who got triggerd</param>
    /// <returns> true if the spawner is active and the check was accepted</returns>
    public bool checkSpawnTrigger(GameObject trigger) {
        if (isActive == false) {

            return false;
        }

        foreach (Enemy_Spawner_Info e in enemysToSpawn) {
            if (e.useTriggerArea == true && trigger == e.triggerArea && e.SpawnStartet == false && e.SpawnConditonFulfilled == false) {
                StartCoroutine(startSpawntimer(e.delay, e));
                e.SpawnStartet = true;
            }

        }
        return true;
    }

    /// <summary>
    /// starts the activation of the spawner
    /// </summary>
    /// <param name="trigger"> trigger area who got triggerd</param>
    /// <returns> true if spawner activation was started. false if the spawner is already active</returns>
    public bool checkSpawnerActivationTrigger(GameObject trigger) {
        if (isActive == false) {
            if (useTriggerArea == true && triggerArea == trigger) {
                StartCoroutine(spawnerActivationTimer(delay));
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// reduce the spawn count after a enemy was killed
    /// </summary>
    public void spawnKilled() {
        currentSpawnCount = currentSpawnCount - 1;

    }
}
