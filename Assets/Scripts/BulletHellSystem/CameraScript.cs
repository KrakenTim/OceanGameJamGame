using UnityEngine;

public class CameraScript : MonoBehaviour {
    private void Awake() {

        Globals.currentCamera = gameObject.GetComponent<Camera>();
    }
}
