using UnityEngine;using System.Collections;public class FuelMonitor : MonoBehaviour{    public float fuelLevel = 0f;    private bool scriptsEnable = true;    private RandomMovementScript randomMovementScript;    private FollowBehaviour followBehaviorScript;    public float getFuelLevel()    {        return fuelLevel;    }    // Use this for initialization	void Start ()    {
        // On start we attempt to retrieve the nominal amount of fuel for an element
        if (gameObject.GetComponent<AttributesManager>() != null)
        {
            fuelLevel = gameObject.GetComponent<AttributesManager>().fuel;
        }
        // then we attempt to retrieve information regarding what scripts the fuel monitor will control
        if (gameObject.GetComponent<RandomMovementScript>() != null)
        {
            randomMovementScript = gameObject.GetComponent<RandomMovementScript>();
        }        if (gameObject.GetComponent<FollowBehaviour>() != null)
        {
            followBehaviorScript = gameObject.GetComponent<FollowBehaviour>();
        }    }	void FixedUpdate ()    {        if (scriptsEnable == false & randomMovementScript != null) randomMovementScript.enabled = false;        if (scriptsEnable == false & followBehaviorScript != null) followBehaviorScript.enabled = false;    }    public void updateFuel(float fuelDelta)    {        fuelLevel -= fuelDelta;        if (fuelLevel <= 0f)        {            //stop all fuel consuming scripts if fuel level is zero and any behaviour is still running            Debug.Log(string.Concat("Empty fuel handling entered"));            fuelLevel = 0;            scriptsEnable = false;        }        if (fuelLevel <= 0f) fuelLevel = 0f;        //***Reserved space here for behaviour re-enable***    }}