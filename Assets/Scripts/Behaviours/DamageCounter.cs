using UnityEngine;
using System.Collections;

public class DamageCounter : MonoBehaviour
{
    private AttributesManager Attributes;
    private FuelMonitor fuelMonitor;
    public GameObject craterPrefab;
    public float currentDamage = 0;
    public void addDamage(float damage){
        currentDamage += damage;
    }
    // Use this for initialization
    void Start(){
        // On start we retrieve the attributes of the object to be used in damage calculations
        // and the RigidBody object this script is applied to.
        Attributes = this.gameObject.GetComponent<AttributesManager>();
        fuelMonitor = this.gameObject.GetComponent<FuelMonitor>();
    }
    void OnCollisionEnter2D(Collision2D other){
        // Multiply collision impact relative velocity with object mass, apply absorption coefficient to calculate taken damage
        currentDamage += Attributes.absorptionCoefficient * 0.5f * Mathf.Pow(other.relativeVelocity.magnitude, 2) * Attributes.mass;
    }
    void FixedUpdate(){
        if (Attributes.health < currentDamage){
            // Check if total received damage is higher than object health
            // NOTE: Current script uses raw attribute value, to be changed if health modifiers are implemented in the game.
            // Here we also instantiate an explosion crater sprite with its associated behaviour.
            GameObject temp = (GameObject)Resources.Load("prefabs/effects/crater");
            craterPrefab = (GameObject)Instantiate(temp, (Vector3)gameObject.GetComponent<Rigidbody2D>().position, gameObject.transform.rotation);
            craterPrefab.AddComponent<ExplosionEffect>().setExplosionFuel(fuelMonitor.getFuelLevel());
            Debug.Log("Remaining fuel level for explosion passed as " + fuelMonitor.getFuelLevel().ToString());
            // If we had consumed an object before, we let it go free here.
            GameObject consumedObject = Attributes.getConsumedBody();
            if (consumedObject != null){
                consumedObject.gameObject.GetComponent<HingeJoint2D>().enabled = false;
            }

            // Finally we get rid of the swarm element that was destroyed.
            Destroy(gameObject);
        }
    }
}