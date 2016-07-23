using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwarmSpawnScript : MonoBehaviour {
    public uint numSwarmElements = 10;
    public float spawnAreaDiameter = 2.5f;
    private SpriteRenderer assignedObject;
    private BehavioursManager behavioursManager;
    private FuelMonitor fuelMonitor;
    public GameObject swarmPrefab;
    private List<GameObject> spawnedSwarmObjects = new List<GameObject>();

	// Use this for initialization
	void Awake ()
    {
        // On start we perform the following actions:
        // 1. Get a handle of the behaviours manager script and request swarm group 1 to be built
        // 2. Disable the sprite for the swarm generation area;
        // 3. Generate a designated number of swarm objects if there is sufficient space for them.
        behavioursManager = GameObject.Find("BehavioursConfigContainer").GetComponent<BehavioursManager>();
        assignedObject = gameObject.GetComponent<SpriteRenderer>();
        assignedObject.enabled = false;
        for (int i = 0; i < numSwarmElements; i++)
        {
            swarmPrefab = behavioursManager.buildSwarmGroup1Element();
            GameObject temp = (GameObject)Instantiate(swarmPrefab, assignedObject.transform.position - new Vector3(Random.Range(0f - 0.5f * spawnAreaDiameter, 0.5f * spawnAreaDiameter), (Random.Range(0f - 0.5f * spawnAreaDiameter, 0.5f * spawnAreaDiameter)), 0), Quaternion.identity);
            spawnedSwarmObjects.Add(temp);
        }
        // Once we have spawned all swarm objects, we initialise their behaviour scripts.
        for (int i = 0; i < spawnedSwarmObjects.Count; i++) {
            // Initialise FuelMonitor logic:
            fuelMonitor = spawnedSwarmObjects[i].GetComponent<FuelMonitor>();
            fuelMonitor.initialise();
            // >>> TODO - set fuel level according to level presets!! <<<
            fuelMonitor.setFuelLevel(5000f);
            // Check if Follow behaviour is present, and initialise it if necessary.
            if (spawnedSwarmObjects[i].GetComponent<FollowBehaviour>() != null) {
                spawnedSwarmObjects[i].GetComponent<FollowBehaviour>().initialise();
            }
            else Debug.Log("Skipping FollowBehaviour initialisation - script not found or not active");
            // Check if Random Movement behaviour is present, and initialise it if necessary.
            if (spawnedSwarmObjects[i].GetComponent<RandomMovementScript>() != null) {
                spawnedSwarmObjects[i].GetComponent<RandomMovementScript>().initialise();
            }
            else Debug.Log("Skipping Random Movement behaviour initialisation - script not found or not active");
            // No need to initialise inertial behaviour - handled in fuel monitor script.
            // Check if Repulse behaviour is present, and initialise it if necessary.
            if (spawnedSwarmObjects[i].GetComponent<ReactiveBehaviour>() != null) {
                spawnedSwarmObjects[i].GetComponent<ReactiveBehaviour>().initialise();
            }
            else Debug.Log("Skipping Repulse behaviour initialisation - script not found or not active");
            // Check if Orbit behaviour is present, and initialise it if necessary.
            if (spawnedSwarmObjects[i].GetComponent<OrbitBehaviour>() != null) {
                spawnedSwarmObjects[i].GetComponent<OrbitBehaviour>().initialise();
            }
            else Debug.Log("Skipping Orbit behaviour initialisation - script not found or not active");
        }
    }
}
