using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour {

	public void LoadLevel(string name) {
		Debug.Log("New Level load: " + name);
		SceneManager.LoadScene(name);
	}

	public void QuitRequest() {
		Debug.Log("Quit requested");
		Application.Quit();
	}

	public void PlayGame(GameObject obj)
    {
		Debug.Log("Play Game");
		obj.GetComponent<PanelController>().isPaused = false;
    }

}
