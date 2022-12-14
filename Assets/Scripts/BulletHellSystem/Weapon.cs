
using System.Collections;
using UnityEngine;

/// <summary>
/// describes the weapon of the player
/// </summary>
public class Weapon : MonoBehaviour {

    /// <summary>
    /// weapon skill prefab
    /// </summary>
    public GameObject skill;
    /// <summary>
    /// weapon reload time
    /// </summary>
    public float reloadTime;
    public bool autoShoot;
    public bool useAmmo;
    private bool canShoot;

    [SerializeField] public int shootsToPreGenerate;


    /// <summary>
    /// additional weapon dmg
    /// </summary>
    public int additionalDmg;
    /// <summary>
    /// weapon dmg multiplier
    /// </summary>
    public float dmgModifier;



    /// <summary>
    /// weapon can shot from frame 1
    /// </summary>
    private void OnEnable() {
        canShoot = true;
    }

    /// <summary>
    /// creates skills ahead of time so they don't need to be created at runtime
    /// </summary>
    private void Awake() {



        for (int i = 0; i < shootsToPreGenerate;) {
            GameObject g = activateSkill(true);
            g.SetActive(false);
            i = i + 1;
        }

    }



    /// <summary>
    /// delay timer between shots
    /// </summary>
    /// <param name="wait"> delay between shots in seconds</param>
    /// <returns></returns>
    private IEnumerator shootTimer(float wait) {

        yield return new WaitForSeconds(wait);
        canShoot = true;
    }

    /// <summary>
    /// creates the skill object with dmg modifiers if the weapon can shoot
    /// </summary>
    /// <param name="additionalDmg"> increases the dmg of the bullet directly by this value</param>
    /// <param name="dmgModifier"> after adding the additional dmg to the bullet dmg multiply the resulting value by this value</param>
    /// <returns>remaining Ammo</returns>
    public int shoot(float additionalDmg, float dmgModifier, int ammo) {
        if (canShoot == true) {
            if (useAmmo == true) {
                if (ammo < 1) {
                    return ammo;
                }
                ammo = ammo - 1;
            }
            canShoot = false;

            GameObject g = activateSkill(false);
            g.GetComponent<Skill>().setDmgModifiers(additionalDmg + this.additionalDmg, dmgModifier * this.dmgModifier);

            StartCoroutine(shootTimer(reloadTime));

        }
        return ammo;
    }

    /// <summary>
    /// creates skills and sets them on the right position and activates them
    /// checks bevor creation new skills if they are in the bulletpool
    /// can also create skill ahead of time
    /// </summary>
    /// <param name="preCreation"> if true then creates bullets ahead of time</param>
    /// <returns>Gameobject of Skill</returns>
    public GameObject activateSkill(bool preCreation) {
        GameObject g;
        Skill skillObject;
        if (preCreation == false) {
            skillObject = Globals.bulletPool.Find(x => x.gameObject.name == skill.name && x.gameObject.activeSelf == false);
            if (skillObject == null) {
                //Debug.Log(sound);

                g = Instantiate(skill, transform.position, transform.rotation);
                g.name = skill.name;
                g.layer = (int)Layer_enum.player_bullets;
                g.GetComponent<Skill>().layerChange();
                Debug.Log("additional skill created");
            }
            else {
                Globals.bulletPool.Remove(skillObject);
                skillObject.transform.position = transform.position;
                skillObject.transform.rotation = transform.rotation;
                skillObject.gameObject.layer = (int)Layer_enum.player_bullets;
                skillObject.gameObject.SetActive(true);
                g = skillObject.gameObject;
            }

        }
        else {

            g = Instantiate(skill);
            g.name = skill.name;
            g.layer = (int)Layer_enum.player_bullets;



        }



        return g;
    }
}
