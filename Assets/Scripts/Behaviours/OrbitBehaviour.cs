using UnityEngine;
using System.Collections;

public class OrbitBehaviour : MonoBehaviour {
    public bool isActivated = false;
    public float orbitForce;
    public float orbitDistance;
    private FuelMonitor fuelMonitor;
    
    // *** EXTERNAL TRIGGER FUNCTIONS ***
	public void initialise () {
        fuelMonitor = gameObject.GetComponent<FuelMonitor>();
        if (fuelMonitor != null) {
            Debug.Log("Successfully initialised orbit behaviour");
            isActivated = true;
        }
        else Debug.Log("Failed to initialise reactive behaviour - no FuelMonitor script found");
    }
    public void setOrbitForce (float value) {
        orbitForce = value;
    }
    public void setOrbitDistance (float value) {
        orbitDistance = value;
    }
	// *** INTERNAL TRIGGER FUNCTIONS ***
    // none so far, TODO
    
    // *** PERIODIC FUNCTIONS ***
	void FixedUpdate () {
	    //TODO
	}
}
