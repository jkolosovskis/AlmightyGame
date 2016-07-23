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

    private Text FollowForceText;
    private Text FollowDistanceText;
    private Text MaintainMotionCoefficientText;
    private Text RandMovMaxXText;
    private Text RandMovMaxYText;
    private Text RandMovMinXText;
    private Text RandMovMinYText;
    private Text RepulseForceText;
    private Text OrbitForceText;
    private Text OrbitDistanceText;

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
        // Continued by identifying UI slider value text elements.
        FollowForceText = GameObject.Find("FollowForceText").GetComponent<Text>();
        FollowDistanceText = GameObject.Find("FollowDistanceText").GetComponent<Text>();
        MaintainMotionCoefficientText = GameObject.Find("MaintainMotionCoefficientText").GetComponent<Text>();
        RandMovMaxXText = GameObject.Find("RandMovMaxXText").GetComponent<Text>();
        RandMovMaxYText = GameObject.Find("RandMovMaxYText").GetComponent<Text>();
        RandMovMinXText = GameObject.Find("RandMovMinXText").GetComponent<Text>();
        RandMovMinYText = GameObject.Find("RandMovMinYText").GetComponent<Text>();
        RepulseForceText = GameObject.Find("RepulseForceText").GetComponent<Text>();
        OrbitForceText = GameObject.Find("OrbitForceText").GetComponent<Text>();
        OrbitDistanceText = GameObject.Find("OrbitDistanceText").GetComponent<Text>();

        //We also find the root container of UI behaviours that will be passed to other scenes.
        behavioursManager = GameObject.Find("BehavioursConfigContainer").GetComponent<BehavioursManager>();

        // Now that all behaviour UI containers and elements have been found, we deactivate them (since they otherwise overlay each other)
        deactivateAllUIMenus();
        // Next, we set up a callback function to operate the dropdown menu for selecting behaviour settings to edit.
        BehaviourSelector = GameObject.FindObjectOfType<Dropdown>();
        BehaviourSelector.onValueChanged.AddListener(delegate {
            selectBehaviourEdit(BehaviourSelector);
        });
        // Similarly, we add listeners for all sliders in order to update the text descriptions with current values.
        FollowForceSlider.onValueChanged.AddListener(delegate {
            updateFollowForceText(FollowForceSlider);
        });
        FollowDistanceSlider.onValueChanged.AddListener(delegate {
            updateFollowDistanceText(FollowDistanceSlider);
        });
        MaintainMotionCoefficientSlider.onValueChanged.AddListener(delegate {
            updateMaintainMotionCoefficientSliderText(MaintainMotionCoefficientSlider);
        });
        RandMovMaxForceXSlider.onValueChanged.AddListener(delegate {
            updateRandMovMaxXText(RandMovMaxForceXSlider);
        });
        RandMovMaxForceYSlider.onValueChanged.AddListener(delegate {
            updateRandMovMaxYText(RandMovMaxForceYSlider);
        });
        RandMovMinForceXSlider.onValueChanged.AddListener(delegate {
            updateRandMovMinXText(RandMovMinForceXSlider);
        });
        RandMovMinForceYSlider.onValueChanged.AddListener(delegate {
            updateRandMovMinYText(RandMovMinForceYSlider);
        });
        RepulseForceSlider.onValueChanged.AddListener(delegate {
            updateRepulseForceText(RepulseForceSlider);
        });
        OrbitForceSlider.onValueChanged.AddListener(delegate {
            updateOrbitForceText(OrbitForceSlider);
        });
        OrbitDistanceSlider.onValueChanged.AddListener(delegate {
            updateOrbitDistanceText(OrbitDistanceSlider);
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
    // *** Slider text update functions ***
    public void updateFollowForceText (Slider FollowForceSlider) {
        FollowForceText.text = FollowForceSlider.value.ToString("F2") + "N";
    }
    public void updateFollowDistanceText(Slider FollowDistanceSlider) {
        FollowDistanceText.text = FollowDistanceSlider.value.ToString("F2") + "m";
    }
    public void updateMaintainMotionCoefficientSliderText(Slider MaintainMotionCoefficientSlider) {
        MaintainMotionCoefficientText.text = MaintainMotionCoefficientSlider.value.ToString("F2");
    }
    public void updateRandMovMaxXText(Slider RandMovMaxForceXSlider) {
        RandMovMaxXText.text = RandMovMaxForceXSlider.value.ToString("F2") + "N";
    }
    public void updateRandMovMaxYText(Slider RandMovMaxForceYSlider) {
        RandMovMaxYText.text = RandMovMaxForceYSlider.value.ToString("F2") + "N";
    }
    public void updateRandMovMinXText(Slider RandMovMinForceXSlider) {
        RandMovMinXText.text = RandMovMinForceXSlider.value.ToString("F2") + "N";
    }
    public void updateRandMovMinYText(Slider RandMovMinForceYSlider) {
        RandMovMinYText.text = RandMovMinForceYSlider.value.ToString("F2") + "N";
    }
    public void updateRepulseForceText(Slider RepulseForceSlider) {
        RepulseForceText.text = RepulseForceSlider.value.ToString("F2") + "N";
    }
    public void updateOrbitForceText(Slider OrbitForceSlider) {
        OrbitForceText.text = OrbitForceSlider.value.ToString("F2") + "N";
    }
    public void updateOrbitDistanceText(Slider OrbitDistanceSlider) {
        OrbitDistanceText.text = OrbitDistanceSlider.value.ToString("F2") + "m";
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
