using UnityEngine;using System.Collections;public class AttributesManager : MonoBehaviour{    public float absorptionCoefficient = 0.1f;    public float health = 50f;    public float fuel = 5000f;    public float mass = 10f;    private Collider2D assignedCollider;    private Rigidbody2D assignedRigidbody;    private GameObject consumedBody = null;

    public void setConsumedBody(GameObject body){
        consumedBody = body;
    }
    public GameObject getConsumedBody(){
        return consumedBody;
    }

    void Start ()    {        // Retrieve correct attribute values from defined sources (saved files, UI, etc)        // ***TODO***        // Apply absorption and mass attributes to physics collider properties (if any colliders exist)        assignedCollider = gameObject.GetComponent<Collider2D>();        assignedRigidbody = gameObject.GetComponent<Rigidbody2D>();        if (assignedCollider != null)        {            assignedCollider.sharedMaterial.bounciness = 1f - absorptionCoefficient;        }        if (assignedRigidbody != null)        {            assignedRigidbody.mass = mass;                }    }		// Update is called once per frame	void Update ()    {        // AttributeManager script is used to establish default attributes for objects,        // so nothing expected to be updated every frame in this script.	}}