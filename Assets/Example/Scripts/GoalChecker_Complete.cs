using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GoalChecker_Complete : MonoBehaviour {

	public GameObject retryButton;
    public GameObject unityChan;
    
    public AudioSource goalJingle;
    public AudioSource bgm;


	void Start()
	{
		retryButton.SetActive(false);
	}

	void OnTriggerEnter(Collider other) {			
		
        retryButton.SetActive(true);

        other.GetComponent<Rigidbody>().isKinematic = true;

        unityChan.transform.LookAt(Camera.main.transform);
        unityChan.GetComponent<Animator>().SetTrigger("Goal");
      
        bgm.Stop();
		goalJingle.Play();
	}

	public void RetryStage()
	{
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
