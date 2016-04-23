using UnityEngine;
using System.Collections;

public class FuelMonitor : MonoBehaviour
{
    public float fuelLevel = 0f;
    private bool scriptsEnable = true;
    private float inertialCoefficient = 0f;
    private Vector2 totalIndependentForce = Vector2.zero;
    private Vector2 totalFuelEffectedForce = Vector2.zero;

    private RandomMovementScript randomMovementScript;
    private FollowBehaviour followBehaviourScript;
    private InertialBehaviour inertialBehaviourScript;
    private Rigidbody2D thisBody;

    public float getFuelLevel(){
        return fuelLevel;
    }
    public void AddForce(Vector2 force, bool isExternalForce){
        if (isExternalForce == false) totalFuelEffectedForce += force;
        else totalIndependentForce += force;
    }
    // Use this for initialization
	void Start (){
        // On start we attempt to retrieve the nominal amount of fuel for an element
        if (gameObject.GetComponent<AttributesManager>() != null){
            fuelLevel = gameObject.GetComponent<AttributesManager>().fuel;
        }
        // then we attempt to retrieve information regarding what scripts the fuel monitor will control
        if (gameObject.GetComponent<RandomMovementScript>() != null){
            randomMovementScript = gameObject.GetComponent<RandomMovementScript>();
        }
        if (gameObject.GetComponent<FollowBehaviour>() != null){
            followBehaviourScript = gameObject.GetComponent<FollowBehaviour>();
        }
        if (gameObject.GetComponent<InertialBehaviour>() != null){
            inertialBehaviourScript = gameObject.GetComponent<InertialBehaviour>();
            inertialCoefficient = inertialBehaviourScript.getInertialCoefficient();
        }
        // finally we identify what gameobject's rigidbody2D this script is attached to
        thisBody = gameObject.GetComponent<Rigidbody2D>();
    }
	void FixedUpdate (){
        // First we check if we have been given a signal to stop running scripts, and act accordingly.
        if (scriptsEnable == false & randomMovementScript != null) randomMovementScript.enabled = false;
        if (scriptsEnable == false & followBehaviourScript != null) followBehaviourScript.enabled = false;
        if (scriptsEnable == false & inertialBehaviourScript != null) inertialBehaviourScript.enabled = false;
        // Next, we apply the total requested force to the object this script is attached to, and calculate the loss
        // of fuel resulting from force application.
        // We also implement inertial behaviour here, but use the InertialBehaviour script to retrieve
        // the relevant inertia coefficients.
        Vector2 fullForce = (totalFuelEffectedForce + totalIndependentForce) * (1 - inertialCoefficient);
        Vector2 fuelEffectedForce = totalFuelEffectedForce + fullForce * Mathf.Abs(inertialCoefficient);
        thisBody.AddForce(fullForce);
        updateFuel(fuelEffectedForce.magnitude);
        // Bugfix - we reset the total applied force counters here
        totalFuelEffectedForce = Vector2.zero;
        totalIndependentForce = Vector2.zero;
    }
    public void updateFuel(float fuelDelta){
        if (scriptsEnable != false) {
            fuelLevel -= fuelDelta;
            if (fuelLevel <= 0f) {
                // stop all fuel consuming scripts if fuel level is zero and any behaviour is still running
                Debug.Log(string.Concat("Empty fuel handling entered"));
                fuelLevel = 0;
                scriptsEnable = false;
            }
            if (fuelLevel <= 0f) fuelLevel = 0f;
            //***Reserved space here for behaviour re-enable***
        }
    }
}
