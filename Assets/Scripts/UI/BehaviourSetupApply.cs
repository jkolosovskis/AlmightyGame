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

    private BehavioursManager behavioursManager;

    private Toggle FollowEnabledToggle;
    private Toggle MaintainMotionEnabledToggle;
    private Toggle RandomMovementEnabledToggle;
    private Toggle RepulseEnabledToggle;
    private Toggle OrbitEnabledToggle;

    private Slider FollowForceSlider;
    private Slider FollowDistanceSlider;
    private Slider MaintainMotionCoefficientSlider;
    private Slider RandMovMaxForceXSlider;
    private Slider RandMovMaxForceYSlider;
    private Slider RandMovMinForceXSlider;
    private Slider RandMovMinForceYSlider;
    private Slider RepulseForceSlider;
    private Slider OrbitForceSlider;
    private Slider OrbitDistanceSlider;

    private int currentDropdown = 0; 

    void Start() {
        // We start by identifying all behaviour UI containers.
        FollowContainer = GameObject.Find("FollowContainer");
        MaintainMotionContainer = GameObject.Find("MaintainMotionContainer");
        RandomMovementContainer = GameObject.Find("RandomMovementContainer");
        RepulseContainer = GameObject.Find("RepulseContainer");
        OrbitContainer = GameObject.Find("OrbitContainer");
        
        // Followed by identifying all behaviour UI sliders and toggles.
        FollowEnabledToggle = GameObject.Find("FollowEnabledToggle").GetComponent<Toggle>();
        MaintainMotionEnabledToggle = GameObject.Find("MaintainMotionEnabledToggle").GetComponent<Toggle>();
        RandomMovementEnabledToggle = GameObject.Find("RandomMovementEnabledToggle").GetComponent<Toggle>();
        RepulseEnabledToggle = GameObject.Find("RepulseEnabledToggle").GetComponent<Toggle>();
        OrbitEnabledToggle = GameObject.Find("OrbitEnabledToggle").GetComponent<Toggle>();

        FollowForceSlider = GameObject.Find("FollowForceSlider").GetComponent<Slider>();
        FollowDistanceSlider = GameObject.Find("FollowDistanceSlider").GetComponent<Slider>();
        MaintainMotionCoefficientSlider = GameObject.Find("MaintainMotionCoefficientSlider").GetComponent<Slider>();
        RandMovMaxForceXSlider = GameObject.Find("RandMovMaxForceXSlider").GetComponent<Slider>();
        RandMovMaxForceYSlider = GameObject.Find("RandMovMaxForceYSlider").GetComponent<Slider>();
        RandMovMinForceXSlider = GameObject.Find("RandMovMinForceXSlider").GetComponent<Slider>();
        RandMovMinForceYSlider = GameObject.Find("RandMovMinForceYSlider").GetComponent<Slider>();
        RepulseForceSlider = GameObject.Find("RepulseForceSlider").GetComponent<Slider>();
        OrbitForceSlider = GameObject.Find("OrbitForceSlider").GetComponent<Slider>();
        OrbitDistanceSlider = GameObject.Find("OrbitDistanceSlider").GetComponent<Slider>();

        //We also find the root container of UI behaviours that will be passed to other scenes.
        behavioursManager = GameObject.Find("BehavioursConfigContainer").GetComponent<BehavioursManager>();

        // Now that all behaviour UI containers and elements have been found, we deactivate them (since they otherwise overlay each other)
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
    // Reuseable function to populate swarm behaviours config with values from UI
    private void applyCurrentUIParams() {
        switch (currentDropdown) {
            case 0:
                // Save follow behaviour settings here
                behavioursManager.setFollowEnabled(FollowEnabledToggle.isOn);
                behavioursManager.setFollowForce(FollowForceSlider.value);
                behavioursManager.setFollowDistance(FollowDistanceSlider.value);
                break;
            case 1:
                // Save maintain motion behaviour settings here
                behavioursManager.setMaintainMotionEnabled(MaintainMotionEnabledToggle.isOn);
                behavioursManager.setMaintainMotionCoefficient(MaintainMotionCoefficientSlider.value);
                break;
            case 2:
                // Save random movement behaviour settings here
                behavioursManager.setRandomMovementEnabled(RandomMovementEnabledToggle.isOn);
                behavioursManager.setRandMovMaxForceX(RandMovMaxForceXSlider.value);
                behavioursManager.setRandMovMaxForceY(RandMovMaxForceYSlider.value);
                behavioursManager.setRandMovMinForceX(RandMovMinForceXSlider.value);
                behavioursManager.setRandMovMinForceY(RandMovMinForceYSlider.value);
                break;
            case 3:
                // Save repulse behaviour settings here
                behavioursManager.setRepulseEnabled(RepulseEnabledToggle.isOn);
                behavioursManager.setRepulseForce(RepulseForceSlider.value);
                break;
            case 4:
                // Save orbit behaviour settings here
                behavioursManager.setOrbitEnabled(OrbitEnabledToggle.isOn);
                behavioursManager.setOrbitForce(OrbitForceSlider.value);
                behavioursManager.setOrbitDistance(OrbitDistanceSlider.value);
                break;
            default:
                // Do nothing
                break;
        }
    }
    // Function for switching between behaviour configuration panels
    // In every case, we update our swarm settings with latest values 
    // from UI sliders before disabling the previous UI part.
	private void selectBehaviourEdit (Dropdown behaviourSelector) {
        deactivateAllUIMenus();
        switch (behaviourSelector.value) {
            case 0:
                applyCurrentUIParams();
                FollowContainer.SetActive(true);
                currentDropdown = BehaviourSelector.value;
                break;
            case 1:
                applyCurrentUIParams();
                MaintainMotionContainer.SetActive(true);
                currentDropdown = BehaviourSelector.value;
                break;
            case 2:
                applyCurrentUIParams();
                RandomMovementContainer.SetActive(true);
                currentDropdown = BehaviourSelector.value;
                break;
            case 3:
                applyCurrentUIParams();
                RepulseContainer.SetActive(true);
                currentDropdown = BehaviourSelector.value;
                break;
            case 4:
                applyCurrentUIParams();
                OrbitContainer.SetActive(true);
                currentDropdown = BehaviourSelector.value;
                break;
            default:
                applyCurrentUIParams();
                FollowContainer.SetActive(true);
                currentDropdown = 0;
                break;
        }
	}
    // Function to be executed when pressing Start Game button.
    public void moveToSimulationMenu() {
        applyCurrentUIParams();
        SceneManager.LoadScene("TestScene");
        // *** TODO: figure out a way to pass the behavioursManager object to simulation scene.
    }
    // Clear all callbacks on the scene when moving away from it (??)
    void Destroy() {
        BehaviourSelector.onValueChanged.RemoveAllListeners();
    }
}
