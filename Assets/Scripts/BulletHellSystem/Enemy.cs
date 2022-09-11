using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class describes enemys and their movement
/// </summary>
public class Enemy : MonoBehaviour {

    /// <summary>
    /// health of enemy
    /// </summary>
    public float health;
    private float maxHealth;


    /// <summary>
    /// physic object of enemy
    /// </summary>
    public Rigidbody2D body;
    private Waypoint_Designer designer;
    /// <summary>
    /// waypoint liste for movement
    /// </summary>
    public List<Vector2> waypoints;
    /// <summary>
    /// makes the enemy more to a random waypoint
    /// </summary>
    public bool moveToRandomWaypoints;
    /// <summary>
    /// renembers the current player position and moves towards it 
    /// </summary>
    public bool moveToPlayer;

    /// <summary>
    /// follows the player on the x Position
    /// </summary>
    public bool followPlayerMovementX;
    /// <summary>
    /// follows the palyer on the y Position
    /// </summary>
    public bool followPlayerMovementY;
    /// <summary>
    /// describes how precise the follow movement has to be
    /// </summary>
    public float playerfollowRange;
    /// <summary>
    /// the used force to move the enemy
    /// </summary>
    public float force;
    /// <summary>
    /// the maximum speed the enemy can have
    /// </summary>
    public float maxSpeed;


    /// <summary>
    /// loops the waypoints if it has more than 1
    /// </summary>
    public bool loop;
    private float restartAfter;
    /// <summary>
    /// waypoint prefab
    /// </summary>
    public GameObject waypointPrefab;
    private float restartTime;
    private List<GameObject> waypointObject;
    private int waypointIndex;


    /// <summary>
    /// the delay between waypoint movements
    /// </summary>
    public float delayToNextWaypoint;




    /// <summary>
    /// collision dmg the player takes
    /// </summary>
    public int collisionDmg;
    /// <summary>
    /// if the enemy destorys itself with a collision
    /// </summary>
    public bool destoryAfterCollison;




    private Vector2 savedDirection;
    private bool stopMove;



    private Enemy_Spawner spawnerCallback;


    public bool CountAsAtackableEnemy;

    private bool maxDurationReached;

    public float maxDuration;

    public float addTimeOnKill;
    public int addAmmoOnKill;

    /// <summary>
    /// lets the enemy move always at full speed
    /// </summary>
    public bool doNotUseForceToMove;

    /// <summary>
    /// the spawner of the enemy
    /// </summary>
    public Enemy_Spawner SpawnerCallback {
        get {
            return spawnerCallback;
        }

        set {
            spawnerCallback = value;
        }
    }
    /// <summary>
    /// maximum health of enemy
    /// </summary>
    public float MaxHealth {
        get {
            return maxHealth;
        }

        set {
            maxHealth = value;
        }
    }


    /// <summary>
    /// creates all enemy waypoints out of the vector list
    /// starts the max duration corutine
    /// </summary>
    void Start() {

        maxDurationReached = false;

        maxHealth = health;
        restartTime = 0;


        stopMove = false;

        waypointObject = new List<GameObject>();
        try {
            designer = GetComponentInParent<Waypoint_Designer>();

            waypoints = new List<Vector2>(designer.waypoints);
            force = designer.force;
            maxSpeed = designer.speed;
            loop = designer.loop;
            restartAfter = designer.restartAfter;
            waypointPrefab = designer.waypointPrefab;
            delayToNextWaypoint = designer.enemyDelayToNextWaypoint;
            maxDuration = 0;

        }
        catch {
            //       Debug.Log("no designer mode");
        }
        waypointIndex = 0;





        for (int i = 0; i < waypoints.Count;) {
            createNextWaypoint(waypoints[i]);
            i = i + 1;
        }
        savedDirection = Vector2.zero;

        if (maxDuration == -1) {
            maxDurationReached = true;

        }
        else if (maxDuration != 0) {
            StartCoroutine(startMaxDurationTimer(maxDuration));
        }

        try {
            BoxCollider2D collider = GetComponent<BoxCollider2D>();
            collider.isTrigger = false;
        }
        catch {

        }



    }

    /// <summary>
    /// timer which describes the max duration of the enemy
    /// </summary>
    /// <param name="wait"> max duration in seconds</param>
    /// <returns></returns>
    private IEnumerator startMaxDurationTimer(float wait) {
        yield return new WaitForSeconds(wait);

        maxDurationReached = true;
    }
    /// <summary>
    /// movement control and duration check
    /// </summary>
    void Update() {

        if (stopMove == false) {
            movement();
        }
        if (maxDurationReached == true) {
            if (moveToPlayer == true || followPlayerMovementX == true || followPlayerMovementY == true || (waypoints.Count == waypointIndex && loop == false && waypoints.Count != 0)) {
                // sofort rausbewegen bei den anderen wird es erst am wegpunkt gemacht
                startMovingOut();
            }


        }


    }




    private void Awake() {
        if (Globals.enemyList == null) {
            Globals.enemyList = new List<Enemy>();
        }
        Globals.enemyList.Add(this);

        if (CountAsAtackableEnemy == true) {
            Globals.enemyCounter = Globals.enemyCounter + 1;
        }

    }


    /// <summary>
    /// starts the move out of this enemy if the enemy has reached its max duration
    /// deaactivates the enemy script
    /// </summary>
    public void startMovingOut() {
        //Debug.Log(transform.parent.gameObject.name);

        try {
            Move_in_out_Scene m = GetComponentInParent<Move_in_out_Scene>();
            // speed auf anderen rigidbody übergbene 
            m.body.bodyType = RigidbodyType2D.Dynamic;
            m.body.velocity = body.velocity;

            // weil sich sonst das schiff nicht mitbewegt
            //body.bodyType = RigidbodyType2D.Kinematic;
            //Debug.Log("versuch zu rausbewegung zu starten");
            m.startMoveOut();
            enabled = false;
        }
        catch {
            //kein moveout script vorhanden zerstöre enemy an dieser Stelle
            Destroy(transform.parent.gameObject);
        }

    }

    /// <summary>
    /// timer for delays after reaching the waypoint befor the enemy moves to the next waypoint
    /// </summary>
    /// <param name="wait"> delay in seconds</param>
    /// <returns></returns>
    private IEnumerator startMoveDelay(float wait) {
        yield return new WaitForSeconds(wait);

        stopMove = false;

    }




    /// <summary>

    /// executes the movementbehavior of the enemy depending on the set variables
    /// priority of variables
    /// moveToPlayer
    /// followPlayerMovementX and followPlayerMovementY
    /// waypoints
    /// moveToRandomWaypoint
    /// </summary>
    private void movement() {

        if (moveToPlayer == true && Globals.player != null) {

            Vector2 direction;
            if (savedDirection == Vector2.zero) {
                direction = Globals.player.transform.position - transform.position;
                savedDirection = direction;
            }
            else {
                direction = savedDirection;
            }

            if (followPlayerMovementX == true && followPlayerMovementY == true && Globals.player != null) {
                //waypointDirectionSet = false;

                direction = Globals.player.transform.position - transform.position;

            }
            else if (followPlayerMovementX == true && Globals.player != null) {
                //waypointDirectionSet = false;

                if (Globals.player.transform.position.x - transform.position.x >= -playerfollowRange && Globals.player.transform.position.x - transform.position.x <= playerfollowRange) {
                    //return;
                }
                else {
                    direction = new Vector2(Globals.player.transform.position.x - transform.position.x, direction.y);

                }

            }
            else if (followPlayerMovementY == true && Globals.player != null) {
                //waypointDirectionSet = false;
                if (Globals.player.transform.position.y - transform.position.y >= -playerfollowRange && Globals.player.transform.position.y - transform.position.y <= playerfollowRange) {
                    //return;
                }
                else {
                    direction = new Vector2(direction.x, Globals.player.transform.position.y - transform.position.y);
                }
            }
            if (doNotUseForceToMove == true) {
                body.velocity = direction.normalized * maxSpeed;
            }
            else {
                body.AddForce(direction.normalized * force * Time.deltaTime, ForceMode2D.Impulse);
            }


            Vector2 normalizedSpeed = body.velocity.normalized * maxSpeed;
            normalizedSpeed.x = Mathf.Abs(normalizedSpeed.x);
            normalizedSpeed.y = Mathf.Abs(normalizedSpeed.y);

            body.velocity = new Vector2(Mathf.Clamp(body.velocity.x, -normalizedSpeed.x, normalizedSpeed.x), Mathf.Clamp(body.velocity.y, -normalizedSpeed.y, normalizedSpeed.y));

        }
        else if (followPlayerMovementX == true && Globals.player != null) {
            Vector2 direction;
            if (Globals.player.transform.position.x - transform.position.x >= -playerfollowRange && Globals.player.transform.position.x - transform.position.x <= playerfollowRange) {
                return;
            }
            else {
                direction = new Vector2(Globals.player.transform.position.x - transform.position.x, 0);
            }
            if (doNotUseForceToMove == true) {
                body.velocity = direction.normalized * maxSpeed;
            }
            else {
                body.AddForce(direction.normalized * force * Time.deltaTime, ForceMode2D.Impulse);
            }
            Vector2 normalizedSpeed = body.velocity.normalized * maxSpeed;
            normalizedSpeed.x = Mathf.Abs(normalizedSpeed.x);
            normalizedSpeed.y = Mathf.Abs(normalizedSpeed.y);

            body.velocity = new Vector2(Mathf.Clamp(body.velocity.x, -normalizedSpeed.x, normalizedSpeed.x), Mathf.Clamp(body.velocity.y, -normalizedSpeed.y, normalizedSpeed.y));
        }
        else if (followPlayerMovementY == true && Globals.player != null) {

            Vector2 direction;
            if (Globals.player.transform.position.y - transform.position.y >= -playerfollowRange && Globals.player.transform.position.y - transform.position.y <= playerfollowRange) {
                return;
            }
            else {
                direction = new Vector2(0, Globals.player.transform.position.y - transform.position.y);
            }
            if (doNotUseForceToMove == true) {
                body.velocity = direction.normalized * maxSpeed;
            }
            else {
                body.AddForce(direction.normalized * force * Time.deltaTime, ForceMode2D.Impulse);
            }
            Vector2 normalizedSpeed = body.velocity.normalized * maxSpeed;
            normalizedSpeed.x = Mathf.Abs(normalizedSpeed.x);
            normalizedSpeed.y = Mathf.Abs(normalizedSpeed.y);

            body.velocity = new Vector2(Mathf.Clamp(body.velocity.x, -normalizedSpeed.x, normalizedSpeed.x), Mathf.Clamp(body.velocity.y, -normalizedSpeed.y, normalizedSpeed.y));
        }
        else if (waypoints.Count > waypointIndex) {
            //createNextWaypoint(waypoints[waypointIndex]);
            //waypointDirectionSet = true;
            Vector2 direction = waypointObject[waypointIndex].transform.position - transform.position;

            if (doNotUseForceToMove == true) {
                body.velocity = direction.normalized * maxSpeed;
            }
            else {
                body.AddForce(direction.normalized * force * Time.deltaTime, ForceMode2D.Impulse);
            }
            Vector2 normalizedSpeed = body.velocity.normalized * maxSpeed;
            normalizedSpeed.x = Mathf.Abs(normalizedSpeed.x);
            normalizedSpeed.y = Mathf.Abs(normalizedSpeed.y);

            body.velocity = new Vector2(Mathf.Clamp(body.velocity.x, -normalizedSpeed.x, normalizedSpeed.x), Mathf.Clamp(body.velocity.y, -normalizedSpeed.y, normalizedSpeed.y));
            waypointObject[waypointIndex].SetActive(true);
        }
        else if (waypoints.Count == waypointIndex && loop == true && waypoints.Count != 0) {
            //createNextWaypoint(new Vector2(0, 0));
            //waypointDirectionSet = true;
            waypointIndex = 0;
            Vector2 direction = waypointObject[waypointIndex].transform.position - transform.position;
            if (doNotUseForceToMove == true) {
                body.velocity = direction.normalized * maxSpeed;
            }
            else {
                body.AddForce(direction.normalized * force * Time.deltaTime, ForceMode2D.Impulse);
            }
            Vector2 normalizedSpeed = body.velocity.normalized * maxSpeed;
            normalizedSpeed.x = Mathf.Abs(normalizedSpeed.x);
            normalizedSpeed.y = Mathf.Abs(normalizedSpeed.y);

            body.velocity = new Vector2(Mathf.Clamp(body.velocity.x, -normalizedSpeed.x, normalizedSpeed.x), Mathf.Clamp(body.velocity.y, -normalizedSpeed.y, normalizedSpeed.y));

            waypointObject[waypointIndex].SetActive(true);


        }

        else if (designer != null && waypoints.Count == waypointIndex) {


            restartTime = restartTime + Time.deltaTime;
            if (restartAfter <= restartTime) {
                restartTime = 0;
                waypointIndex = 0;
                transform.position = waypointObject[waypointIndex].transform.position;
                waypointObject[waypointIndex].SetActive(true);


            }
        }




    }

    /// <summary>
    /// take dmg function
    /// </summary>
    /// <param name="dmg"> dmg the enemy takes</param>
    public void takeDmg(float dmg) {
        //Debug.Log(dmg);
        //  Debug.Log(health);
        health = health - dmg;
        //  Debug.Log(health);


        if (health <= 0) {

            Destroy(gameObject.transform.parent.gameObject);
            //Instantiate(deathParticelSystem, transform.position, transform.rotation);


            Player p = Globals.player.GetComponent<Player>();
            p.remainingTime = p.remainingTime + addTimeOnKill;
            if (p.remainingTime > p.maxTimeSave) {
                p.remainingTime = p.maxTimeSave;
            }

            p.currentAmmo = p.currentAmmo + addAmmoOnKill;
            if (p.currentAmmo > p.maxAmmo) {
                p.currentAmmo = p.maxAmmo;
            }

        }

    }




    /// <summary>
    /// creates the waypoints using the given vector
    /// </summary>
    /// <param name="v2">  position of the waypoint</param>
    private void createNextWaypoint(Vector2 v2) {
        GameObject g = Instantiate(waypointPrefab, transform.parent);
        g.transform.localPosition = v2;
        // g.layer = gameObject.layer;
        waypointObject.Add(g);
        g.SetActive(false);

    }

    /// <summary>
    /// checks if a waypoint was reached and starts movement towards nextwaypoint with delay if delay exists
    /// </summary>
    /// <param name="collision"> collision object</param>
    private void OnTriggerEnter2D(Collider2D collision) {
        try {

            if (collision.gameObject == waypointObject[waypointIndex]) {
                if (moveToRandomWaypoints == true) {
                    int i = -1;
                    //Debug.Log("random Roll start");
                    while (i == -1 || waypointObject.Count == i || i == waypointIndex) {
                        // grenzen um 1 Zahl erweiter da eck zaheln nur selten ausgewürfelt werden
                        // wenn unmögliche Zahl kommt wird zahl neu ermittelt
                        i = Random.Range(-1, waypointObject.Count + 1);
                        //Debug.Log("random roll");
                    }
                    waypointIndex = i;


                }
                else {
                    waypointIndex = waypointIndex + 1;
                }


                collision.gameObject.SetActive(false);
                stopMove = true;

                if (maxDurationReached == true) {

                    startMovingOut();

                }
                else {
                    StartCoroutine(startMoveDelay(delayToNextWaypoint));
                }
            }
        }
        catch {
            //Debug.Log("no Waypoint collision");
            //Debug.Log(collision);
        }


    }

    /// <summary>
    /// if collision with player give player collision dmg and destroy enemys if it is destroyed by collision
    /// </summary>
    /// <param name="collision"> collsion object</param>
    private void OnCollisionEnter2D(Collision2D collision) {
        try {
            if (collision.gameObject == Globals.player) {
                Player p = Globals.player.GetComponent<Player>();


                p.takeDmg(collisionDmg);

                if (destoryAfterCollison == true) {
                    Destroy(transform.parent.gameObject);
                }
            }
        }
        catch {
            //Debug.Log(collision);
        }

    }

    /// <summary>



    /// <summary>
    /// if enemy is destroyed, destory all waypoints
    /// wincondition reduce enemy counter
    /// callback towards spawner, so that spawner can spawn new enemy in its place
    /// </summary>
    private void OnDestroy() {
        try {
            foreach (GameObject g in waypointObject) {
                Destroy(g);
            }
        }
        catch {

        }

        if (CountAsAtackableEnemy == true) {
            Globals.enemyCounter = Globals.enemyCounter - 1;
        }
        if (spawnerCallback != null) {
            spawnerCallback.spawnKilled();
        }
        Globals.enemyList.Remove(this);
    }
}
