using UnityEngine;
using System.Collections;

public class FuelMonitor : MonoBehaviour
{
    public float fuelLevel = 500f;
    private bool scriptsEnable = true;

    public float getFuelLevel()
    {
        return fuelLevel;
    }
    // Use this for initialization
	void Start ()
    {
        //do nothing
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (scriptsEnable == false & this.gameObject.GetComponent<RandomMovementScript>() != null) this.gameObject.GetComponent<RandomMovementScript>().enabled = false;
        if (scriptsEnable == false & this.gameObject.GetComponent<FollowBehaviour>() != null) this.gameObject.GetComponent<FollowBehaviour>().enabled = false;
    }
    public void updateFuel(float fuelDelta)
    {
        fuelLevel -= fuelDelta;
        if (fuelLevel <= 0f)
        {
            //stop all fuel consuming scripts if fuel level is zero and any behaviour is still running
            Debug.Log(string.Concat("Empty fuel handling entered"));
            fuelLevel = 0;
            scriptsEnable = false;
        }
        if (fuelLevel <= 0f) fuelLevel = 0f;
        //***Reserved space here for behaviour re-enable***
    }
}
