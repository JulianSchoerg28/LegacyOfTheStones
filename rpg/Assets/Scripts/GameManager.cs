using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = System.Object;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private bool playerCanMove = true;
    private PlayerMovement playerMovement;
    public int hearts = 100;
    public Text heartsText;
    
    public Armor equippedArmor;
    public Image ArmorUImage;
    
    private Sword equippedSword { get; set; }
    public Image SwordUImage;

    public bool isBossKilled = false;

    private float normalPlayerAttackDamage;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {
        updateArmorUI();
        updateSwordUI();
        playerMovement = FindObjectOfType<PlayerMovement>();
        normalPlayerAttackDamage = playerMovement.playerAttackDamage;
        Debug.Log("normal player attack: " + normalPlayerAttackDamage);
    }

    public bool PlayerCanMove
    {
        get => playerCanMove;
        set => playerCanMove = value;
    }

    private void Update()
    {
        updateHearts();
    }
    
    public void StartSpeedBoost(float boostMultiplier, int boostDuration)
    {
        if (playerMovement != null)
        {
            StartCoroutine(ApplySpeedBoost(boostMultiplier, boostDuration));
        }
        else
        {
            Debug.LogWarning("PlayerMovement not found!");
        }
    }

    private IEnumerator ApplySpeedBoost(float boostMultiplier, int boostDuration)
    {
        Debug.Log("hier" + boostMultiplier);
        playerMovement.ApplySpeedBoost(boostMultiplier); 

        yield return new WaitForSeconds(boostDuration);

        playerMovement.ApplySpeedBoost(1 / boostMultiplier);
    }
    private void updateHearts()
    {
        heartsText.text = hearts.ToString();
    }
    public void TakeDamage(int damage)
    {
        float defenseFactor = 0f;

        if (equippedArmor != null)
        {
            defenseFactor = equippedArmor.Defense;
            equippedArmor.Durability--;
            equippedArmor.updateDuribilityInDescription();
            
            if (equippedArmor.Durability <= 0)
            {
                equippedArmor = null;
                updateArmorUI();
            }
        }
        hearts -= Mathf.RoundToInt(damage * (1 - defenseFactor));        
        
        
        if (hearts <= 0)
        {
            ClearAllDontDestroyOnLoad();
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void takeHeal(int heal)
    {
        hearts += heal;
        if (hearts >= 100)
        {
            hearts = 100;
        }
    }
    
    public void EquipArmor(Armor armor)
    {
        if (equippedArmor != null)
        {
            Inventory.Instance.AddToInventory(equippedArmor);
        }
        
        equippedArmor = armor;
        updateArmorUI();

    }

    public void updateArmorUI()
    {
        if (ArmorUImage)
        {
            if (equippedArmor != null)
            {
                if (equippedArmor.ItemSprite != null)
                {
                    ArmorUImage.sprite = equippedArmor.ItemSprite;
                    //idk why this isn´t working but okk i fixed it cheap with setting image to null :D
                    ArmorUImage.enabled = true;
                }
                else
                {
                    Debug.Log("Item has no sprite :(");
                }
            }
            else
            {
                //i mean this doesn´t work
                ArmorUImage.enabled = false;  
                //so use this:
                ArmorUImage.sprite = null;

            }
        }
        else
        {
            Debug.Log("No armor UI");
        }
    }
    
    public void ClearAllDontDestroyOnLoad()
    {
        GameObject[] dontDestroyOnLoadObjects = FindObjectsOfType<GameObject>();
        List<GameObject> excludedObjects = new List<GameObject>
        {
            GameObject.Find("Manager"),
            GameObject.Find("SettingsManager"),
            GameObject.Find("Audio"),
            GameObject.Find("backgroundMusic")
        };

        foreach (GameObject obj in dontDestroyOnLoadObjects)
        {
            if (excludedObjects.Contains(obj))
            {
                Debug.Log($"{obj.name} bleibt erhalten.");
                continue;
            }

            Destroy(obj);
        }
    }
    
    public void EquipSword(Sword sword)
    {
        if (equippedSword != null)
        {
            Inventory.Instance.AddToInventory(equippedSword);
        }

        equippedSword = sword;
        
        if (equippedSword != null)
        {
            playerMovement.playerAttackDamage = sword.damage;
        }
        else
        {
            playerMovement.playerAttackDamage = normalPlayerAttackDamage;
        }
        
        Debug.Log(playerMovement.playerAttackDamage);
                
        updateSwordUI();
    }

    public void updateSwordUI()
    {
        if (SwordUImage)
        {
            if (equippedSword != null)
            {
                if (equippedSword.ItemSprite != null)
                {
                    SwordUImage.sprite = equippedSword.ItemSprite;
                    SwordUImage.enabled = true;
                }
                else
                {
                    Debug.Log("Das Schwert hat kein Sprite.");
                }
            }
            else
            {
                SwordUImage.sprite = null;
                SwordUImage.enabled = false;
            }
        }
        else
        {
            SwordUImage.sprite = null;
            Debug.Log("Kein Schwert UI gefunden.");
        }
    }


    
    


}
