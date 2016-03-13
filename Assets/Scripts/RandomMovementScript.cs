using UnityEngine;
using System.Collections;

public class RandomMovementScript : MonoBehaviour {
	public float maxRandomForceMagnitudeX = 5f;
	public float maxRandomForceMagnitudeY = 5f;
	public float minRandomForceMagnitudeX = -5f;
	public float minRandomForceMagnitudeY = -5f;

	private Rigidbody2D swarmBody;

	// Use this for initialization
	void Start () 
	{
		swarmBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		swarmBody.AddForce (new Vector2 (Random.Range (minRandomForceMagnitudeX, maxRandomForceMagnitudeX), Random.Range (minRandomForceMagnitudeY, maxRandomForceMagnitudeY)));
	}
}
