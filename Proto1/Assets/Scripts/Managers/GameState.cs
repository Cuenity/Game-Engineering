﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    // Serialize deze dingen nog
    public RollingPhaseManager rollingPhaseManager;
    public BuildingPhaseManager buildingPhaseManager;
    public LevelManager levelManager;

    public PlayerManager playerManager;
    public PlayerBallManager playerBallManager;
    public PlayerCamera playerCamera;

    public PlatformManager platformManager;
    public CollectableManager collectableManager;
    public GridManager gridManager;

    public UIManager UIManager;
    public UIRollingManager uIRollingManager;

    public TutorialManager tutorialManager;

    // Privates
    private bool buildingPhaseActive = true;
    private bool rollingPhaseActive = false;

    //public int PreviousLevel;

    private static GameState instance;
    public static GameState Instance
    {
        get { return instance; }
    }

    public bool BuildingPhaseActive
    {
        get
        {
            return buildingPhaseActive;
        }
        set
        {
            buildingPhaseActive = value;
            if (buildingPhaseActive)
            {
                buildingPhaseManager.Init();
                UIManager.ActivateGarbageBinButton();
            }
            else
            {
                UIManager.DeactivateInventoryButtons();
                UIManager.DeactivateGarbageBinButton();
            }
        }
    }


    public bool RollingPhaseActive
    {
        get { return rollingPhaseActive; }
        set
        {
            rollingPhaseActive = value;
            GameState gameState = GameState.Instance;
            if (rollingPhaseActive)
            {
                rollingPhaseManager.Init();
                gameState.playerBallManager.activePlayer.GetComponent<Rigidbody>().useGravity = true;
                gameState.playerBallManager.activePlayer.GetComponent<SphereCollider>().isTrigger = false;
            }
            else
            {
                gameState.playerBallManager.activePlayer.GetComponent<Rigidbody>().useGravity = false;
                gameState.playerBallManager.activePlayer.GetComponent<SphereCollider>().isTrigger = true;
            }
        }
    }


    void Awake()
    {
        //DontDestroyOnLoad(this);

        instance = this;

        rollingPhaseManager = Instantiate(rollingPhaseManager, instance.transform);
        rollingPhaseManager.transform.parent = this.transform;

        buildingPhaseManager = Instantiate(buildingPhaseManager, instance.transform);
        buildingPhaseManager.transform.parent = this.transform;

        playerManager = Instantiate(playerManager, instance.transform);
        playerManager.transform.parent = this.transform;

        playerBallManager = Instantiate(playerBallManager, instance.transform);
        playerBallManager.transform.parent = this.transform;

        playerCamera = Instantiate(playerCamera, instance.transform);
        playerCamera.transform.parent = this.transform;

        platformManager = Instantiate(platformManager, instance.transform);
        platformManager.transform.parent = this.transform;

        collectableManager = Instantiate(collectableManager, instance.transform);
        collectableManager.transform.parent = this.transform;

        gridManager = Instantiate(gridManager, instance.transform);
        gridManager.transform.parent = this.transform;

        levelManager = Instantiate(levelManager, instance.transform);
        levelManager.transform.parent = this.transform;

        uIRollingManager = Instantiate(uIRollingManager, instance.transform);
        uIRollingManager.transform.parent = this.transform;

        tutorialManager = Instantiate(tutorialManager, instance.transform);
        tutorialManager.transform.parent = this.transform;

        //dit hoeft niet meer??? maar het zou wel es shit kunnen breken
        //Scene scene = SceneManager.GetActiveScene();
        //levelManager.InitScene(scene.name);
        levelManager.InitScene();
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerDataController.instance.PreviousSceneCoinCount = 0;
    }

}