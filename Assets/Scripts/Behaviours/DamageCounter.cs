﻿using UnityEngine;using System.Collections;public class DamageCounter : MonoBehaviour{    private AttributesManager Attributes;    public float currentDamage = 0;    public void addDamage(float damage){        currentDamage += damage;    }    // Use this for initialization    void Start(){        // On start we retrieve the attributes of the object to be used in damage calculations        // and the RigidBody object this script is applied to.        Attributes = gameObject.GetComponent<AttributesManager>();    }    void OnCollisionEnter2D(Collision2D other){        // Multiply collision impact relative velocity with object mass, apply absorption coefficient to calculate taken damage        currentDamage += Attributes.absorptionCoefficient * 0.5f * Mathf.Pow(other.relativeVelocity.magnitude, 2) * Attributes.mass;    }    void FixedUpdate(){
        if (Attributes.health < currentDamage){
            // Check if total received damage is higher than object health
            // NOTE: Current script uses raw attribute value, to be changed if health modifiers are implemented in the game.
            // ***ADD ANY SPECIAL EFFECTS HERE***
            GameObject.Destroy(gameObject);        }
    }}