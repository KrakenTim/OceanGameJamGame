using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;
//using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

/// <summary>
/// class of the player
/// imports the controler Interface
/// </summary>
public class Player : MonoBehaviour {
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



    private List<Weapon> weapons;
    /// <summary>
    /// list of weapon slot objects
    /// </summary>
    public List<GameObject> WeaponSlots;

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








    // public List<GameObject> chargeBalls;






    private Coroutine immunityTimer;

    private Coroutine flickerCo;


    private bool isImmun;
    private SpriteRenderer sp;














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




    }

    /// <summary>
    /// sets base values and starts every corutine
    /// loads equiped weapons and creates weapon objects
    /// </summary>
    private void OnEnable() {

        shooting = false;

        isImmun = false;



        weapons = new List<Weapon>();







        currentHealth = maxBaseHealth;
        //currentschield = maxschield;
        StartCoroutine(shootingHandler());



        flickerDirection = -1;
        sp = ship.GetComponent<SpriteRenderer>();


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



}