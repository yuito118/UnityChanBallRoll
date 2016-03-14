using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BottomChecker : MonoBehaviour {
	void OnTriggerEnter(Collider other) {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	 
}
