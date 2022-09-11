using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {


    public void LoadingScene(int SceneID) {

        SceneManager.LoadScene(SceneID);
    }
}
