using UnityEngine;
using System.Collections;

public class PlayerFollower_Complete : MonoBehaviour {

	public GameObject player;
	Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = this.transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		this.transform.position = player.transform.position + offset;
	}
}
