using UnityEngine;
using System.Collections;

public class myCamCon : MonoBehaviour {

	public GameObject player;
	private Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = this.transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		this.transform.position = player.transform.position + offset;
	}
}
