using UnityEngine;
using System.Collections;

public class RandomMovementScript : MonoBehaviour {
	public float maxRandomForceMagnitudeX = 5f;
	public float maxRandomForceMagnitudeY = 5f;
	public float minRandomForceMagnitudeX = -5f;
	public float minRandomForceMagnitudeY = -5f;

	private Rigidbody2D swarmObject;
    private FuelMonitor fuelObject;

	// Use this for initialization
	void Start () 
	{
		swarmObject = GetComponent<Rigidbody2D>();
        fuelObject = GetComponent<FuelMonitor>();
	}
	
	// Update is called once per frame
	void Update () 
	{
        Vector2 calculatedForce = new Vector2(Random.Range(minRandomForceMagnitudeX, maxRandomForceMagnitudeX), Random.Range(minRandomForceMagnitudeY, maxRandomForceMagnitudeY));
        swarmObject.AddForce (calculatedForce);
        fuelObject.updateFuel(Mathf.Abs(Vector2.Distance(Vector2.zero, calculatedForce)));
	}
}
