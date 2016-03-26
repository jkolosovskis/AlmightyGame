using UnityEngine;
using System.Collections;

public class SwarmSpawnScript : MonoBehaviour {
    public uint numSwarmElements = 10;
    public float spawnAreaDiameter = 2.5f;
    private SpriteRenderer assignedObject;
    public Transform swarmPrefab;

	// Use this for initialization
	void Start ()
    {
        // On start we perform the following actions:
        // 1. Disable the sprite for the swarm generation area;
        // 2. Generate a designated number of swarm objects if there is sufficient space for them.
        assignedObject = gameObject.GetComponent<SpriteRenderer>();
        assignedObject.enabled = false;
        for (int i = 0; i < numSwarmElements; i++)
        {
            Debug.Log("Creating Swarm Object Instance");
            Instantiate(swarmPrefab, assignedObject.transform.position - new Vector3 (Random.Range(0f - 0.5f * spawnAreaDiameter, 0.5f * spawnAreaDiameter),(Random.Range(0f - 0.5f * spawnAreaDiameter, 0.5f * spawnAreaDiameter)),0), Quaternion.identity);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
