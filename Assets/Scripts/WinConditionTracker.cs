using UnityEngine;

public class WinConditionTracker : MonoBehaviour {

    public float activateAfter;
    private bool isActive;
    public GameObject WinScreen;
    // Start is called before the first frame update
    void Start() {
        isActive = false;
    }

    // Update is called once per frame
    void Update() {
        Debug.Log(Globals.enemyCounter);
        if (isActive == true) {
            if (WinScreen.activeSelf == false && Globals.enemyCounter <= 0) {
                Globals.player.GetComponent<Player>().gameWon = true;
                WinScreen.SetActive(true);

            }

        }
        else {
            activateAfter = activateAfter - Time.deltaTime;
            if (activateAfter <= 0) {
                isActive = true;
            }
        }



    }
}
