using UnityEngine;
using System.Collections;

public class OrbitBehaviour : MonoBehaviour {
    public bool isActivated = false;
    public float orbitForce;
    public float orbitDistance;
    public float orbitFuelConsumed = 0f;
    private GameObject[] orbitReferenceObject;
    private FuelMonitor fuelMonitor;
    private GameObject inwardsJoinedObject = null;
    private Rigidbody2D outwardsJoinedObject = null;
    private SpringJoint2D spring;
    
    // *** EXTERNAL TRIGGER FUNCTIONS ***
	public void initialise () {
        orbitReferenceObject = GameObject.FindGameObjectsWithTag("SwarmElements");
        Debug.Log("Found " + orbitReferenceObject.Length.ToString() + " valid swarm objects for Orbit behaviour");
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
    public void setJointAccept (GameObject outwardObject) {
        if (outwardObject != null) {
            inwardsJoinedObject = outwardObject;
        }
        else inwardsJoinedObject = null;
    }
    public bool isAlreadyJoined() {
        if (inwardsJoinedObject != null) {
            //Bugfix - check for destroyed spring jointe before proceeding with evaluation
            if (inwardsJoinedObject.GetComponent<SpringJoint2D>() != null) {
                if (inwardsJoinedObject.GetComponent<SpringJoint2D>().connectedBody == gameObject.GetComponent<Rigidbody2D>())
                    return true;
                else return false;
            }
            else return false;
        }
        else return false;
    }
	// *** INTERNAL TRIGGER FUNCTIONS ***
    // none so far, TODO
    
    // *** PERIODIC FUNCTIONS ***
	void FixedUpdate () {
        ///Orbit Behaviour is intended to be a behaviour where a swarm object
        ///latches on the nearest swarm object within range by creating a rigid
        ///bond with a defined strength, consuming the equivalent amount of fuel
        ///as necessary to break the bond.
        ///Each swarm object is only allowed one 'outwards' bond and one 'inwards' bond.
        ///Two swarm objects must not be connected with two bonds to each other.
        if (isActivated) {
            //We allow the physics engine to handle the actual orbit movement with a spring joint.
            //...
            //First, we check if we can create any new bonds:
            if (outwardsJoinedObject == null && fuelMonitor.getFuelLevel() > orbitForce) {
                //We are allowed to make bonds! Ok, now what..?
                GameObject closestSwarmObject = null;
                float closestSwarmObjectDistance = orbitDistance + 1;
                //Evaluate all swarm objects for fitness to establish bonds:
                for (int i = 0; i < orbitReferenceObject.Length; i++) {
                    //Bugfix - do not proceed with evaluation of the full IF conditions if the orbit reference instance is null
                    if (orbitReferenceObject[i] != null) {
                        if (orbitReferenceObject[i].GetComponent<OrbitBehaviour>().isAlreadyJoined() != true &&
                        orbitReferenceObject[i] != inwardsJoinedObject &&
                        //Bugfix - exclude the object this script is attached to from evaluation!
                        orbitReferenceObject[i] != gameObject) {
                            //A free (and existing) swarm object is found! Now, is it close enough for bonding?
                            float distanceBetweenObjects = Vector2.Distance(gameObject.transform.position, orbitReferenceObject[i].transform.position);
                            if (distanceBetweenObjects < orbitDistance && distanceBetweenObjects < closestSwarmObjectDistance) {
                                //We found a swarm object that is closer than all the previous ones!
                                closestSwarmObject = orbitReferenceObject[i];
                                closestSwarmObjectDistance = distanceBetweenObjects;
                            }
                        }
                    }      
                }
                //Now that we have evaluated all objects for bond establishment fitness, we create the outwards bond:
                if (closestSwarmObject != null) {
                    if (gameObject.GetComponent<SpringJoint2D>() == null) spring = gameObject.AddComponent<SpringJoint2D>();
                    spring.connectedBody = closestSwarmObject.GetComponent<Rigidbody2D>();
                    spring.distance = orbitDistance;
                    spring.breakForce = orbitForce;
                    spring.dampingRatio = 0.8f;
                    spring.frequency = 0.2f;
                    fuelMonitor.modFuelLevel(orbitForce * -1.2f);
                    closestSwarmObject.GetComponent<OrbitBehaviour>().setJointAccept(gameObject);
                    outwardsJoinedObject = closestSwarmObject.GetComponent<Rigidbody2D>();
                }  
            }
            //***
            //***Now comes the part that should be regularly updated:
            //***
            //We pass the tensile information from the swarm object's own bond to fuel monitor:
            if (spring != null) {
                orbitFuelConsumed = spring.reactionForce.magnitude;
                fuelMonitor.modFuelLevel(spring.reactionForce.magnitude * -1f);
            }
            // Since the bonds are breakable, we must monitor them and reset the availability of outwards bonds.
            if (outwardsJoinedObject != null) {
                //Bugfix - check for existance of the spring joint instead of trying to compare target objects
                if (spring == null) {
                    outwardsJoinedObject = null;
                }
            }
            //Finally, we check if we are out of fuel, and if so, break the bonds manually
            //before empty fuel handling is performed
            if (spring != null) {
                if (fuelMonitor.getFuelLevel() < orbitForce * 1.2f) spring.enabled = false;
            }
        }
    }
}
