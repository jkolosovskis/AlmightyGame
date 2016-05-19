using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BehaviourSetupApply : MonoBehaviour {
    private GameObject FollowContainer;
    private GameObject MaintainMotionContainer;
    private GameObject RandomMovementContainer;
    private GameObject RepulseContainer;
    private GameObject OrbitContainer;
    private Dropdown BehaviourSelector;

    void Start() {
        // We start by identifying all behaviour UI containers.
        FollowContainer = GameObject.Find("FollowContainer");
        MaintainMotionContainer = GameObject.Find("MaintainMotionContainer");
        RandomMovementContainer = GameObject.Find("RandomMovementContainer");
        RepulseContainer = GameObject.Find("RepulseContainer");
        OrbitContainer = GameObject.Find("OrbitContainer");
        // Now that all behaviour UI containers have been found, we deactivate them (since they otherwise overlay each other)
        deactivateAllUIMenus();
        // Next, we set up a callback function to operate the dropdown menu for selecting behaviour settings to edit.
        BehaviourSelector = GameObject.FindObjectOfType<Dropdown>();
        BehaviourSelector.onValueChanged.AddListener(delegate {
            selectBehaviourEdit(BehaviourSelector);
        });
    }
    // Reusable function to clear all behaviour UI containers before activating the user selected container
    private void deactivateAllUIMenus() {
        FollowContainer.SetActive(false);
        MaintainMotionContainer.SetActive(false);
        RandomMovementContainer.SetActive(false);
        RepulseContainer.SetActive(false);
        OrbitContainer.SetActive(false);
    }
	// Function for switching between behaviour configuration panels
	private void selectBehaviourEdit (Dropdown behaviourSelector) {
        deactivateAllUIMenus();
        switch (behaviourSelector.value) {
            case 0:
                FollowContainer.SetActive(true);
                break;
            case 1:
                MaintainMotionContainer.SetActive(true);
                break;
            case 2:
                RandomMovementContainer.SetActive(true);
                break;
            case 3:
                RepulseContainer.SetActive(true);
                break;
            case 4:
                OrbitContainer.SetActive(true);
                break;
            default:
                FollowContainer.SetActive(true);
                break;
        }
	}
    // Function to be executed when pressing Start Game button.
    public void moveToSimulationMenu() {
        SceneManager.LoadScene("TestScene");
    }
    // Clear all callbacks on the scene when moving away from it (??)
    void Destroy() {
        BehaviourSelector.onValueChanged.RemoveAllListeners();
    }
}
