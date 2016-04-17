using UnityEngine;
using System.Collections;

public class RandomMovementScript : MonoBehaviour {
	public float maxRandomForceMagnitudeX = 5f;
	public float maxRandomForceMagnitudeY = 5f;
	public float minRandomForceMagnitudeX = -5f;
	public float minRandomForceMagnitudeY = -5f;

    private FuelMonitor fuelObject;

	// Use this for initialization
	void Start () 
	{
        fuelObject = GetComponent<FuelMonitor>();
    }
	
	// Update is called once per frame
	void FixedUpdate () 
	{
        Vector2 calculatedForce = new Vector2(Random.Range(minRandomForceMagnitudeX, maxRandomForceMagnitudeX), Random.Range(minRandomForceMagnitudeY, maxRandomForceMagnitudeY));
        fuelObject.AddForce(calculatedForce,false);
	}
}
