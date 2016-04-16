using UnityEngine;
using System.Collections;

public class magneticBehaviour : MonoBehaviour {
    private GameObject[] magneticTargetObject;
    public float magneticForceMultiplier = -2f;
    public float maxMagneticDistance = 5f;
    
    // On start we find all objects that we could affect by our magnetic field
    // Note that if any swarm objects are made during simulation, this script needs to also include updates of the possible target objects.
    void Start () {
        //magneticTargetObject = GameObject.FindGameObjectsWithTag("SwarmElements");
    }

    // On fixed update we check if any affectable objects are in range, then apply the appropriate force to them if necessary.
    void FixedUpdate(){
        magneticTargetObject = GameObject.FindGameObjectsWithTag("SwarmElements");
        for (int i = 0; i < magneticTargetObject.Length; i++)
        {
            // First, let's see if any objects with swarm element tags are in range...
            // Bugfix - null exceptions invoked when attempting to affect an object that had been destroyed.
            if (!(magneticTargetObject[i] == null))
            {
                // Precalculate distance between two bodies in question for later use:
                float distBetweenObjs = Vector2.Distance(gameObject.transform.position, magneticTargetObject[i].transform.position);
                // Check if any affectable bodies are within maximum range
                if (distBetweenObjs < maxMagneticDistance)
                {
                    // Determine distance vector between two bodies:
                    Vector3 magneticForce = (gameObject.transform.position - magneticTargetObject[i].transform.position);
                    // Then normalise the distance vector to ensure max range does not affect max force:
                    magneticForce.Normalize();
                    // After normalisation, we apply distance scaling (the closer the distance, the higher the force):
                    magneticForce = magneticForce * (1 - (distBetweenObjs / maxMagneticDistance));
                    // Finally, we apply our predefined multiplier of magnetic interaction
                    magneticForce = magneticForce * magneticForceMultiplier;
                    // Now that we have calculated the precise force, we apply it to the target object.
                    FuelMonitor fuelScript = magneticTargetObject[i].GetComponent<FuelMonitor>();
                    //targetBody.AddForce(magneticForce);
                    fuelScript.AddForce(magneticForce,true);
                }
            }
        }
    }

}
