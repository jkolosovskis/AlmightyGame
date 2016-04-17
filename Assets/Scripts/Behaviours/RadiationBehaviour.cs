using UnityEngine;
using System.Collections;

public class RadiationBehaviour : MonoBehaviour {
    private GameObject[] radiationTargetObject;
    public float radiationForceMultiplier = 2f;
    public float maxRadiationDistance = 5f;
    // Use this for initialization
    void Start () {
	    // Do nothing
	}
	
	// Update is called once per frame
	void Update () {
        radiationTargetObject = GameObject.FindGameObjectsWithTag("SwarmElements");
        for (int i = 0; i < radiationTargetObject.Length; i++)
        {
            // First, let's see if any objects with swarm element tags are in range...
            // Bugfix - null exceptions invoked when attempting to affect an object that had been destroyed.
            if (!(radiationTargetObject[i] == null))
            {
                // Precalculate distance between two bodies in question for later use:
                float distBetweenObjs = Vector2.Distance(gameObject.transform.position, radiationTargetObject[i].transform.position);
                // Check if any affectable bodies are within maximum range
                if (distBetweenObjs < maxRadiationDistance)
                {
                    float radiationDamage = radiationForceMultiplier * (1 - distBetweenObjs/maxRadiationDistance);
                    // Now that we have calculated the precise damage, we apply it to the target object.
                    DamageCounter damageScript = radiationTargetObject[i].GetComponent<DamageCounter>();
                    damageScript.addDamage(radiationDamage * Time.fixedDeltaTime);
                }
            }
        }
    }
}
