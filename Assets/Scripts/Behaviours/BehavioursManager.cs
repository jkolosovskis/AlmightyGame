using UnityEngine;
using System.Collections;

public class BehavioursManager : MonoBehaviour {
    private bool followEnabled = false;
    private bool maintainMotionEnabled = false;
    private bool randomMovementEnabled = false;
    private bool repulseEnabled = false;
    private bool orbitEnabled = false;

    private bool isReadEnabled = false;

    private float followForce = 0f;
    private float followDistance = 0f;
    private float maintainMotionCoefficient = 0f;
    private float randMovMaxForceX = 0f;
    private float randMovMaxForceY = 0f;
    private float randMovMinForceX = 0f;
    private float randMovMinForceY = 0f;
    private float repulseForce = 0f;
    private float orbitForce = 0f;
    private float orbitDistance = 0f;

    private FollowBehaviour followBehaviour;
    private InertialBehaviour inertialBehaviour;
    private RandomMovementScript randomMovementScript;
    private ReactiveBehaviour reactiveBehaviour;
    private OrbitBehaviour orbitBehaviour;
    private FuelMonitor fuelMonitor;

    // Declarations for enable/disable configs link between UI and simulation
    public void setFollowEnabled(bool yesno) {
        followEnabled = yesno;
    }
    public void setMaintainMotionEnabled(bool yesno) {
        maintainMotionEnabled = yesno;
    }
    public void setRandomMovementEnabled(bool yesno) {
        randomMovementEnabled = yesno;
    }
    public void setRepulseEnabled(bool yesno) {
        repulseEnabled = yesno;
    }
    public void setOrbitEnabled(bool yesno) {
        orbitEnabled = yesno;
    }

    // Declarations for slider value links between UI and simulation
    public void setFollowForce(float value) {
        followForce = value;
    }
    public void setFollowDistance(float value) {
        followDistance = value;
    }
    public void setMaintainMotionCoefficient(float value) {
        maintainMotionCoefficient = value;
    }
    public void setRandMovMaxForceX(float value) {
        randMovMaxForceX = value;
    }
    public void setRandMovMaxForceY(float value) {
        randMovMaxForceY = value;
    }
    public void setRandMovMinForceX(float value) {
        randMovMinForceX = value;
    }
    public void setRandMovMinForceY(float value) {
        randMovMinForceY = value;
    }
    public void setRepulseForce(float value) {
        repulseForce = value;
    }
    public void setOrbitForce(float value) {
        orbitForce = value;
    }
    public void setOrbitDistance(float value) {
        orbitDistance = value;
    }

    // Here we build a swarm element group 1 prefab by assigning UI behaviour values to it.
    public GameObject buildSwarmGroup1Element() {
        // Load swarm element prefab here.
        GameObject swarm1Prefab = (GameObject)Resources.Load("prefabs/Swarms/swarmObject");
        // Set up follow behaviour here:
        followBehaviour = swarm1Prefab.GetComponent<FollowBehaviour>();
        followBehaviour.setFollowForceMultiplier(followForce);
        followBehaviour.setMaxFollowDistance(followDistance);
        followBehaviour.enabled = followEnabled;
        // Set up random movement behaviour here:
        randomMovementScript = swarm1Prefab.GetComponent<RandomMovementScript>();
        randomMovementScript.setMaxRandForceMagnitudeX(randMovMaxForceX);
        randomMovementScript.setMaxRandForceMagnitudeY(randMovMaxForceY);
        randomMovementScript.setMinRandForceMagnitudeX(randMovMinForceX);
        randomMovementScript.setMinRandForceMagnitudeY(randMovMinForceY);
        randomMovementScript.enabled = randomMovementEnabled;
        // Set up MaintainMotion behaviour here:
        inertialBehaviour = swarm1Prefab.GetComponent<InertialBehaviour>();
        inertialBehaviour.setInertialCoefficient(maintainMotionCoefficient);
        // Set up reactive behaviour here:
        reactiveBehaviour = swarm1Prefab.GetComponent<ReactiveBehaviour>();
        reactiveBehaviour.setReactiveForceMultiplier(repulseForce);
        reactiveBehaviour.enabled = repulseEnabled;
        // Set up orbit behaviour here:
        orbitBehaviour = swarm1Prefab.GetComponent<OrbitBehaviour>();
        orbitBehaviour.setOrbitForce(orbitForce);
        orbitBehaviour.setOrbitDistance(orbitDistance);
        orbitBehaviour.enabled = orbitEnabled;
        // All work done - pass the resulting swarm element prefab to caller function.
        return swarm1Prefab;
    }

    void Start() {
        DontDestroyOnLoad(gameObject);
    }
}
