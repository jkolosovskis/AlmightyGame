using UnityEngine;
using System.Collections;

public class RandomMovementScript : MonoBehaviour {
	public float maxRandomForceMagnitudeX = 5f;
	public float maxRandomForceMagnitudeY = 5f;
	public float minRandomForceMagnitudeX = -5f;
	public float minRandomForceMagnitudeY = -5f;

    private bool isInitialised = false;

    private FuelMonitor fuelObject;

    // *** EXTERNAL TRIGGER FUNCTIONS ***
    public void initialise () 
	{
        fuelObject = GetComponent<FuelMonitor>();
        if (fuelObject != null) {
            Debug.Log("Successfully initialised random movement behaviour");
            isInitialised = true;
        }
        else Debug.Log("Failed to initialise random movement behaviour - missing fuel monitor script");
    }
    public void setMaxRandForceMagnitudeX (float value) {
        maxRandomForceMagnitudeX = value;
    }
    public void setMaxRandForceMagnitudeY (float value) {
        maxRandomForceMagnitudeY = value;
    }
    public void setMinRandForceMagnitudeX (float value) {
        minRandomForceMagnitudeX = value;
    }
	public void setMinRandForceMagnitudeY (float value) {
        minRandomForceMagnitudeY = value;
    }

    // *** PERIODIC FUNCTIONS ***
	void FixedUpdate ()
	{
        if (isInitialised) {
            Vector2 calculatedForce = new Vector2(Random.Range(minRandomForceMagnitudeX, maxRandomForceMagnitudeX), Random.Range(minRandomForceMagnitudeY, maxRandomForceMagnitudeY));
            fuelObject.AddForce(calculatedForce, false);
        }
	}
}
