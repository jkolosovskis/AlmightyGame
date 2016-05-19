using UnityEngine;
using System.Collections;

public class VictoryDestruction : MonoBehaviour {
    public float targetDestructionCount = 50f;

    private VictoryManager victoryManager;
    public float currentTotalDamage = 0f;
    private bool notifyOnceComplete = false;

	void Start () {
        victoryManager = gameObject.GetComponent<VictoryManager>();
	}
	
	public void updateDestructionLevel (float damage) {
        currentTotalDamage += damage;
        if (currentTotalDamage >= targetDestructionCount & !notifyOnceComplete) {
            victoryManager.notifyDestruction(true);
            notifyOnceComplete = true;
            Debug.Log("Destruction victory conditions satisfied");
        }
	}
}
