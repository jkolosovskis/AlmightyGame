using UnityEngine;
using System.Collections;

public class FollowBehaviour : MonoBehaviour {
    private GameObject[] followTargetObject;
    private Rigidbody2D swarmBody;
    private FuelMonitor fuelObject;
    public float followForceMultiplier = -5f;
    public float maxFollowDistance = 5f;

    [HideInInspector] public void updateReferences(){
        // Call to use when a followable object is destroyed in the environment
        followTargetObject = GameObject.FindGameObjectsWithTag("SwarmElements");
    }	
    // Use this for initialization
	void Start (){
        swarmBody = GetComponent<Rigidbody2D>();
        followTargetObject = GameObject.FindGameObjectsWithTag("SwarmElements");
        fuelObject = GetComponent<FuelMonitor>();
    }
	
	// Update is called once per frame
	void FixedUpdate (){
        Vector3 totalForce = Vector3.zero;
        for (int i=0; i < followTargetObject.Length; i++){
            // Bugfix - null exceptions invoked when attempting to follow an object that had been destroyed.
            if (!(followTargetObject[i] == null)){
                // Check if a followable body is within maximum range
                // Distance between objects precalculated here to avoid multiple calculations of this value later
                float distanceBetweenObjects = Vector2.Distance(swarmBody.transform.position, followTargetObject[i].transform.position);
                if (distanceBetweenObjects < maxFollowDistance){
                    // Calculate the vector between this object and followable object, 
                    //then invert its direction and invert its relative magnitude to maximum follow distance
                    Vector3 followForce = -(swarmBody.transform.position - followTargetObject[i].transform.position);
                    // <testVersion> Vector3 followForce = Vector3.Lerp(swarmBody.transform.position, followTargetObject[i].transform.position, maxFollowDistance - Vector2.Distance(swarmBody.transform.position, followTargetObject[i].transform.position)) - swarmBody.transform.position;
                    // In order to make the applied force independent of maximum follow distance, the resulting vector is normalised
                    followForce.Normalize();
                    // Once our vector is normalised, we scale it according to the distance between the two positions.
                    followForce = followForce * (1 - (distanceBetweenObjects / maxFollowDistance));
                    // After distance scaling, we apply our force scaling factor to define the actual following force and direction
                    // (maybe we would want the followbehaviour script to avoid objects instead?)
                    followForce = followForce * followForceMultiplier;
                    totalForce += followForce;
                }
            }
        }
        // Bugfix - overzealous fuel consumption fixed by locally calculating all follow forces before applying the resulting force vector
        // to the rigidbody and updating fuelObject data.
        fuelObject.AddForce(totalForce,false);
    }
}
