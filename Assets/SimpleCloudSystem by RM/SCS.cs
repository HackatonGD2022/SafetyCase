using UnityEngine;

public class SCS : MonoBehaviour {

	public Transform Player;
	public float CloudsSpeed;
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.position = Player.transform.position;
		transform.Rotate(0, Time.deltaTime * CloudsSpeed, 0); 
	}

}
