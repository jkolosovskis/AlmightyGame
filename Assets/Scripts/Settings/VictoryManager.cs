using UnityEngine;
using System.Collections;

public class VictoryManager : MonoBehaviour {
    public bool WorkVictoryEnabled = true;
    public bool SurvivalVictoryEnabled = true;
    public bool DestructionVictoryEnabled = true;

    public bool survivalComplete = false;
    public bool workComplete = false;
    public bool destructionComplete = false;

    private VictoryDestruction victoryDestruction;
    private VictorySurvival victorySurvival;
    private VictoryWork victoryWork;

	void Start () {
        // First we find all of our individual victory evaluation scripts:
        victoryDestruction = gameObject.GetComponent<VictoryDestruction>();
        victorySurvival = gameObject.GetComponent<VictorySurvival>();
        victoryWork = gameObject.GetComponent<VictoryWork>();
        // Now we set all unused victory conditions as complete and disable their corresponding scripts
        // and for all required victory conditions we explicitly enable their scripts
        if (!WorkVictoryEnabled) {
            workComplete = true;
            victoryWork.enabled = false;
        } else victoryWork.enabled = true;
        if (!SurvivalVictoryEnabled) {
            survivalComplete = true;
            victorySurvival.enabled = false;
        } else victoryWork.enabled = true;
        if (!DestructionVictoryEnabled) {
            destructionComplete = true;
            victoryDestruction.enabled = false;
        } else victoryDestruction.enabled = true;
	}
	
	public void notifyDestruction(bool isSuccessful) {
        if (isSuccessful) {
            destructionComplete = true;
            checkAllConditionsCompleted();
        }
        else showDefeatDialog();
    }
    public void notifyWork(bool isSuccessful) {
        if (isSuccessful) {
            workComplete = true;
            checkAllConditionsCompleted();
        }
        else showDefeatDialog();
    }
    public void notifySurvival(bool isSuccessful) {
        if (isSuccessful) {
            survivalComplete = true;
            checkAllConditionsCompleted();
        }
        else showDefeatDialog();
    }
    private void showDefeatDialog() {
        // Show a defeat dialog here
        Debug.Log("One of the victory conditions has been irreversibly failed");
    }
    private void checkAllConditionsCompleted() {
        if (workComplete & survivalComplete & destructionComplete) {
            // Show a victory dialog here
            Debug.Log("All victory conditions satisfied, well done!");
        }
        else Debug.Log("A victory condition has been notified to be successful to victory manager script");
    }
}
