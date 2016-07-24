using UnityEngine;
using System.Collections;

public class PathMarkerFade : MonoBehaviour {
    private float timeToLive = 500f;
    private float fadeCoefficient;
    public float aliveTime = 0f;
    public Color spriteColor;
    private SpriteRenderer pathSprite;

	void Start () {
        fadeCoefficient = 1 / timeToLive;
        pathSprite = gameObject.GetComponent<SpriteRenderer>();
	}
	
	void FixedUpdate () {
        aliveTime++;
        spriteColor = new Color(1.0f, 1.0f, 1.0f, 1.0f - (aliveTime / timeToLive));
        pathSprite.color = spriteColor;
        if (aliveTime >= timeToLive) {
            Destroy(gameObject);
        }
	}
}
