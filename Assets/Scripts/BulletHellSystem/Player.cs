using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//using UnityEngine.InputSystem;
//using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

/// <summary>
/// class of the player
/// imports the controler Interface
/// </summary>
public class Player : MonoBehaviour, InputController.IPlayerMovementActions {
    /// <summary>
    /// player max health
    /// </summary>
    public float maxBaseHealth;
    private float currentHealth;
    /// <summary>
    /// health bar
    /// </summary>
    public Image healthbar;
    /// <summary>
    /// health bar coloring above 60%
    /// </summary>
    public Color healthbarAbove60;
    /// <summary>
    /// health bar coloring above 30%
    /// </summary>
    public Color healthbarAbove30;
    /// <summary>
    /// health bar coloring below 30%
    /// </summary>
    public Color healthbarBelow30;

    /// <summary>
    /// physic object of player
    /// </summary>
    public Rigidbody2D body;



    public List<Weapon> weapons;

    public GameObject weaponHolder;

    /// <summary>
    /// ship object
    /// </summary>
    public GameObject ship;


    private Vector2 impulse;

    private bool shooting;


    /// <summary>
    /// additional player dmg
    /// </summary>
    public float additionalDmg;
    /// <summary>
    /// player dmg multiplier
    /// </summary>
    public float dmgModifier;

    /// <summary>
    /// immunity flicker rate
    /// </summary>
    public float immunityFlickerRate;
    /// <summary>
    /// immunity flicker visibility range
    /// </summary>
    [Range(0, 1)] public float maxFlickerRange;
    private int flickerDirection;


    /// <summary>
    /// immunity time after hit
    /// </summary>
    public float immunityTimeAfterHit;


    public float moveSpeed;
    public float turrentRotationSpeed;

    private Coroutine immunityTimer;

    private Coroutine flickerCo;


    private bool isImmun;
    private SpriteRenderer sp;



    private InputController controller;

    public Vector2 aimTarget;
    private bool useAimTarget;

    /// <summary>
    /// returns the impulse direction
    /// </summary>
    public Vector2 Impulse {
        get {
            return impulse;
        }


    }


    /// <summary>
    /// returns and sets the current health
    /// </summary>
    public float CurrentHealth {
        get {
            return currentHealth;
        }

        set {
            currentHealth = value;
        }
    }


    /// <summary>
    /// creates the controler object and loads all rebindings
    /// </summary>
    void Start() {



        Globals.player = gameObject;
        impulse = new Vector2(0, 0);


        if (Globals.enemyList == null) {
            Globals.enemyList = new List<Enemy>();
        }

    }

    /// <summary>
    /// sets base values and starts every corutine
    /// loads equiped weapons and creates weapon objects
    /// </summary>
    private void OnEnable() {


        if (controller == null) {
            controller = new InputController();
            controller.playerMovement.SetCallbacks(this);
            controller.playerMovement.Enable();

        }

        shooting = true;

        isImmun = false;



        currentHealth = maxBaseHealth;
        //currentschield = maxschield;
        StartCoroutine(shootingHandler());



        flickerDirection = -1;
        sp = ship.GetComponent<SpriteRenderer>();


    }

    private void OnDisable() {
        controller.playerMovement.Disable();
        controller.Dispose();
        controller = null;
    }



    private void Update() {
        body.velocity = moveSpeed * impulse;


        Vector2 dir = Vector2.zero;



        if (useAimTarget == true) {
            dir = aimTarget - (Vector2)transform.position;
        }
        else {
            Enemy nearestEnemy = null;
            float currentDistance = 0;
            foreach (Enemy enemy in Globals.enemyList) {
                if (nearestEnemy == null) {
                    nearestEnemy = enemy;
                    currentDistance = (transform.position - enemy.transform.position).magnitude;

                    continue;
                }
                float distance = (transform.position - enemy.transform.position).magnitude;
                if (currentDistance > distance) {
                    nearestEnemy = enemy;
                    currentDistance = distance;
                }
            }
            if (nearestEnemy != null) {
                dir = nearestEnemy.transform.position - transform.position;
            }
        }



        float angle = Vector2.SignedAngle(Vector2.right, dir);

        weaponHolder.transform.rotation = Quaternion.RotateTowards(weaponHolder.transform.rotation, Quaternion.Euler(0, 0, angle), turrentRotationSpeed * Time.deltaTime);

    }



    /// <summary>
    /// function to create a flcikering effect
    /// </summary>
    private void flicker() {

        float deltaTime = Time.deltaTime;
        sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, sp.color.a + (flickerDirection * immunityFlickerRate * deltaTime));

        if (sp.color.a <= maxFlickerRange) {
            flickerDirection = 1;

        }
        else if (sp.color.a >= 1) {
            flickerDirection = -1;
        }


    }


    /// <summary>
    /// corutine which checks if the shoot input is pushed and fires shots of weapons
    /// </summary>
    /// <returns></returns>
    private IEnumerator shootingHandler() {


        while (true) {
            if (shooting == true && weapons.Count != 0) {
                foreach (Weapon w in weapons) {
                    w.shoot(additionalDmg, dmgModifier);
                }
            }
            yield return null;
        }
    }


    /// <summary>
    /// take dmg funktion
    /// </summary>
    /// <param name="dmg"> the dmg the player takes</param>
    public void takeDmg(float dmg) {
        if (isImmun == true) {
            return;
        }




        currentHealth = currentHealth - dmg;

        if (currentHealth <= 0) {
            return;
            // destroy moved to smooth health drop
            //Globals.gameoverHandler.gameOver();
        }

        isImmun = true;
        gameObject.layer = (int)Layer_enum.player_immunity; // immunity layer
        immunityTimer = StartCoroutine(immunityTime(immunityTimeAfterHit));
    }


    /// <summary>
    /// corutine which shows the flickering of the player if he is immmun to dmg
    /// </summary>
    /// <returns></returns>
    private IEnumerator immunityFlickerHandler() {


        while (isImmun == true) {
            flicker();
            yield return null;
        }

        sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 1);


        flickerCo = null;
    }

    /// <summary>
    /// immunity timer
    /// </summary>
    /// <param name="time"> duration of immunity in seconds</param>
    /// <returns></returns>
    private IEnumerator immunityTime(float time) {
        if (flickerCo == null) {
            flickerCo = StartCoroutine(immunityFlickerHandler());
        }
        yield return new WaitForSeconds(time);
        isImmun = false;
        gameObject.layer = (int)Layer_enum.player; //player layer
    }

    public void OnMoveUp(InputAction.CallbackContext context) {

        if (context.started) {
            impulse = impulse + Vector2.up;
        }
        else if (context.canceled) {
            impulse = impulse + Vector2.down;
        }
    }

    public void OnMoveDown(InputAction.CallbackContext context) {
        if (context.started) {
            impulse = impulse + Vector2.down;
        }
        else if (context.canceled) {
            impulse = impulse + Vector2.up;
        }
    }

    public void OnMoveRight(InputAction.CallbackContext context) {
        if (context.started) {
            impulse = impulse + Vector2.right;
        }
        else if (context.canceled) {
            impulse = impulse + Vector2.left;
        }
    }

    public void OnMoveLeft(InputAction.CallbackContext context) {
        if (context.started) {
            impulse = impulse + Vector2.left;
        }
        else if (context.canceled) {
            impulse = impulse + Vector2.right;
        }
    }

    public void OnAimTarget(InputAction.CallbackContext context) {
        //throw new System.NotImplementedException();

        if (context.started) {
            aimTarget = (Vector2)Globals.currentCamera.ScreenToWorldPoint((Vector2)Mouse.current.position.ReadValue());
            useAimTarget = true;
        }

    }

    public void OnStopAimTarget(InputAction.CallbackContext context) {
        if (context.started) {
            useAimTarget = false;
        }
    }
}
