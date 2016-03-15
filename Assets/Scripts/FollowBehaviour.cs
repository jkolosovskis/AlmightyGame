using UnityEngine;using System.Collections;public class FollowBehaviour : MonoBehaviour {    private GameObject[] followTargetObject;    private Rigidbody2D swarmBody;    public float maxFollowForce = 50f;    public float maxFollowDistance = 5f;	    // Use this for initialization	void Start ()    {        swarmBody = GetComponent<Rigidbody2D>();        followTargetObject = GameObject.FindGameObjectsWithTag("SwarmElements");    }		// Update is called once per frame	void Update ()    {
        for (int i=0; i< followTargetObject.Length; i++)        {
            if (Vector2.Distance(swarmBody.transform.position, followTargetObject[i].transform.position) < maxFollowDistance)
            {
                swarmBody.AddForce(Vector3.Lerp(swarmBody.transform.position, followTargetObject[i].transform.position, maxFollowDistance - Vector2.Distance(swarmBody.transform.position, followTargetObject[i].transform.position)) - swarmBody.transform.position);
            }
        }
    }}