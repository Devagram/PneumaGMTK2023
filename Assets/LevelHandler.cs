using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
/*using static UnityEditor.ObjectChangeEventStream;
using System;
using Unity.VisualScripting;
using static UnityEditor.VersionControl.Asset;*/

public class LevelHandler : MonoBehaviour
{
    [SerializeField]
    public LevelSO levelScriptObj;
    [SerializeField]
    public DragableObject chestObject;
    [SerializeField]
    public DragableObject pitObject;
    [SerializeField]
    public DragableObject spiderObject;
    [SerializeField]
    public DragableObject archerObject;
    [SerializeField]
    public DragableObject springObject;
    [SerializeField]
    public DragableObject spikeObject;
    //[SerializeField]
    //public DragableObject spikeObject;
    [SerializeField]
    public GameObject inventory;
    [SerializeField]
    public GameObject slotPrefab;
    [SerializeField]
    public TextMeshProUGUI levelText;
    [SerializeField]
    public TextMeshProUGUI timerText;
    [SerializeField]
    public TextMeshProUGUI goldCountText;
    [SerializeField]
    public GameObject playSpace;
    [SerializeField]
    public AudioSource music;
    [SerializeField]
    public AudioClip trapPhaseMusic;
    [SerializeField]
    public Scrollbar scrollBar;
    [SerializeField]
    public GameObject inventoryObject;
    [SerializeField]
    public GameObject heroPrefab;
    [SerializeField]
    public List<GameObject> locations;
    [SerializeField]
    public float startingGold;
    [SerializeField]
    DragableObject emptyDO;
    /*    [SerializeField]
        public AudioSource music;*/

    private int totalSlots;
    private int chestSlots;
    private int pitSlots;
    private int spiderSlots;
    private int archerSlots;
    private int springSlots;
    private int spikeSlots;
    private bool allChestsPlaced;

    private float currentGold;

    public SpawnPoint[] spawnPoints;

    private Coroutine phaseCoroutine;

    private bool playGame;
    //private int spikeSlots;
    // Start is called before the first frame update
    void Start()
    {
        SetUpGoldCount();
        SetLevelName();
        SetSpawnPoints();
        CalculateSlots();
        PopulateInventory();
        /*StartBuildPhase();*/
    }

    private void Update()
    {
        if (currentGold <= 0)
        {
            SceneManager.LoadScene("MainMenu");
        }
        goldCountText.text = "GOLD: " + currentGold;
        PlayerPrefs.SetFloat("GoldCount", currentGold);
        
        /*InventorySlot[] allSlots = inventory.GetComponentsInChildren<InventorySlot>();
        bool chestInInventory = false;
        foreach (InventorySlot slot in allSlots)
        {
            Debug.Log("Slot Name: " + slot.name);
            if (slot.name == "chest" && slot.containsItem) {
                chestInInventory = true;
            }
        }
        allChestsPlaced = !chestInInventory;
        Debug.Log("CHEST IN INVENTORY: " + chestInInventory);
        Debug.Log("ALL CHESTS PLACED: " + allChestsPlaced);*/
    }

    public void PlayButtonPressed(GameObject pressingButton)
    {
        InventorySlot[] allSlots = inventory.GetComponentsInChildren<InventorySlot>();
        bool chestInInventory = false;
        foreach (InventorySlot slot in allSlots)
        {
            //Debug.Log("Slot Name: " + slot.name);
            //Debug.Log("Is there an Item?: " + slot.containsItem);
            //Debug.Log("               )" + slot.dragObject.GetType().ToString());
            if (slot.name == "chest" && slot.dragObject != emptyDO)
            {
                //Debug.Log("FOUND A CHEST!!!!");
                chestInInventory = true;
                break;
            }
        }

        if (chestInInventory == false)
        {
            //todo play game from countdown
            playGame = true;
            StartPlayPhase();
            //turn off the button
            pressingButton.SetActive(false);
        } else
        {
            //todo warn user and prevent the game proceeding
        }
        //Debug.Log("CHEST IN INVENTORY: " + chestInInventory);
        //Debug.Log("ALL CHESTS PLACED: " + allChestsPlaced);
    }

    public void SetUpGoldCount()
    {
        if (levelScriptObj.levelName == "test" || levelScriptObj.levelName == "1")
        {
            PlayerPrefs.SetFloat("GoldCount", startingGold);
        }
        currentGold = PlayerPrefs.GetFloat("GoldCount");
    }

    public void CalculateSlots()
    {
        totalSlots = levelScriptObj.chestCount + levelScriptObj.pitCount + levelScriptObj.spiderCount + levelScriptObj.archerCount + levelScriptObj.springCount + levelScriptObj.spikeCount;
        chestSlots = levelScriptObj.chestCount;
        pitSlots = levelScriptObj.chestCount + levelScriptObj.pitCount;
        spiderSlots = levelScriptObj.chestCount + levelScriptObj.pitCount + levelScriptObj.spiderCount;
        archerSlots = levelScriptObj.chestCount + levelScriptObj.pitCount + levelScriptObj.spiderCount + levelScriptObj.archerCount;
        springSlots = levelScriptObj.chestCount + levelScriptObj.pitCount + levelScriptObj.spiderCount + levelScriptObj.archerCount + levelScriptObj.springCount;
        spikeSlots = totalSlots;
    }

    public void PopulateInventory()
    {
        
        GridLayoutGroup gridLayout = inventory.GetComponent<GridLayoutGroup>();
        //RectTransform parentRectTransform = inventory.GetComponent<RectTransform>();
        //RectTransform slotTransform;
        //List<GameObject> instantiatedObjects = new List<GameObject>();
        InventorySlot currentSlot;
        //TextMeshPro levelText = levelNameObject.GetComponent<TextMeshPro>();
        
        
        for (int i = totalSlots; i >= 0; i--)
        {
            GameObject slot = Instantiate(slotPrefab, gridLayout.transform);
            currentSlot = slot.GetComponent<InventorySlot>();
            //chest
            if (i < chestSlots)
            {
                slot.name = "chest";
                currentSlot.dragObject = chestObject;

            }//pit
            else if (i >= chestSlots && i < pitSlots)
            {
                slot.name = "pit";
                currentSlot.dragObject = pitObject;
            }//spider
            else if (i >= pitSlots && i < spiderSlots)
            {
                slot.name = "spider";
                currentSlot.dragObject = spiderObject;
            }//archer
            else if (i >= spiderSlots && i < archerSlots)
            {
                slot.name = "archer";
                currentSlot.dragObject = archerObject;
            }//spring
            else if (i >= archerSlots && i < springSlots)
            {
                slot.name = "spring";
                currentSlot.dragObject = springObject;
            }
            else if (i >= springSlots && i < spikeSlots)
            {
                slot.name = "spike";
                currentSlot.dragObject = spikeObject;
            }
        }
        
    }

    public void SetLevelName()
    {
        Debug.Log("LEVEL: " + levelScriptObj.levelName + " initializing.");
        levelText.text = levelScriptObj.levelName;
    }

    public void StartPlayPhase()
    {
        //List<GameObject> spaces = new List<GameObject>();

        float timerVar = levelScriptObj.buildPhaseLimit;
        Image[] spaces = playSpace.GetComponentsInChildren<Image>();
        if (playGame)
        {
            phaseCoroutine = StartCoroutine(BuildPhaseTimer(timerVar, spaces));
        }
    }

    public void RemoveGold(float goldToRemove)
    {
        currentGold = currentGold - goldToRemove;
    }

    private IEnumerator BuildPhaseTimer(float timer, Image[] spacesToManage)
    {
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            timerText.text = Mathf.RoundToInt(timer).ToString();

            yield return null;
        }

        EndBuildPhase(spacesToManage);
    }

    private void EndBuildPhase(Image[] spacesToDeactivate)
    {

        Debug.Log("Build phase has ended.");
        
        //inventory grayout
        music.clip = trapPhaseMusic;
        music.Play();
        DisableUI(spacesToDeactivate);
        SpawnHeroes();
        SpawnTraps();
    }

    public void DisableUI(Image[] spacesToDeactivate)
    {
        for (int i = 0; i < spacesToDeactivate.Length; i++)
        {
            spacesToDeactivate[i].enabled = false;
        }
        scrollBar.interactable = false;
        Image[] invImages = inventoryObject.GetComponentsInChildren<Image>();
        for (int j = 0; j < invImages.Length; j++)
        {
            invImages[j].color = Color.gray;
        }
    }

    public void SetSpawnPoints()
    {
        spawnPoints = FindObjectsOfType<SpawnPoint>();
    }

    public void SpawnHeroes()
    {
        foreach (SpawnPoint spawn in spawnPoints)
        {
            //Vector3 spawnPosition = spawn.transform.position;
            GameObject instantiatedHero = Instantiate(heroPrefab, spawn.transform.position, Quaternion.identity);
        }
    }
    public void SpawnTraps()
    {
        AreaSlot[] spawnSlots = FindObjectsOfType<AreaSlot>();
        foreach (AreaSlot slot in spawnSlots)
        {
            
            if (!slot.isWall && slot.dragObject.PrefabItem != null)
            {
                //Debug.Log("INFO: " + slot.isWall + " " + slot.dragObject.PrefabItem);
                Transform pos = slot.GetComponentInChildren<Transform>();
                GameObject slotObject = slot.gameObject;
                //Debug.Log("Type.GetType(slot.dragObject.PrefabItem.GetType().Name) + " + Type.GetType(slot.dragObject.PrefabItem.GetType().Name));
                //slotObject.AddComponent(Type.GetType(slot.dragObject.PrefabItem);
                GameObject instantiatedTrap = Instantiate(slot.dragObject.PrefabItem, pos.position, Quaternion.identity);
                instantiatedTrap.layer = slotObject.layer;
                instantiatedTrap.tag = slotObject.tag;
                instantiatedTrap.transform.parent = slotObject.transform; // Set the same parent
                instantiatedTrap.layer = 6;
                if (slot.dragObject.PrefabItem.tag != "Goal")
                {
                    instantiatedTrap.tag = "Trap";
                } else
                {
                    instantiatedTrap.tag = "Goal";
                }
                //instantiatedTrap.transform.localScale = slotObject.transform.localScale; // Set the same scale

                //Destroy the old object
                //Destroy(slotObject);
            }
        }
    }
}
