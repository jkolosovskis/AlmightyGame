using UnityEngine;
using System.Collections;

public class AttributesManager : MonoBehaviour
{
    public float absorptionCoefficient = 0.5f;
    public float health = 500;
    public float fuel = 500;

	// Use this for initialization
	void Start ()
    {
	    // Retrieve correct attribute values from defined sources (saved files, UI, etc)
	}
	
	// Update is called once per frame
	void Update ()
    {
        // AttributeManager script is used to establish default attributes for objects,
        // so nothing expected to be updated every frame in this script.
	}
}
