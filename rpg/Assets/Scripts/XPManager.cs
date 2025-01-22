using UnityEngine;
using UnityEngine.UI; // Wichtig f�r Slider und Text-Komponenten

public class XPManager : MonoBehaviour
{
    public static XPManager instance;

    public Slider xpSlider; // Slider f�r die XP-Anzeige
    public Text levelText;  // Text f�r die Level-Anzeige

    public int currentXP = 0; // Aktuelle XP des Spielers
    public int maxXP = 100;   // Maximale XP f�r das Level
    public int currentLevel = 1; // Startlevel des Spielers

    void Awake()
    {
        // Singleton-Logik: Nur eine Instanz von XPManager erlauben
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Automatische Verkn�pfung von XPBar und LevelText
        if (xpSlider == null)
        {
            xpSlider = GameObject.Find("XPbar")?.GetComponent<Slider>();
            if (xpSlider == null)
            {
                Debug.LogError("XPbar Slider nicht gefunden! Stelle sicher, dass der Name 'XPText' korrekt ist.");
            }
        }

        if (levelText == null)
        {
            levelText = GameObject.Find("Leveltext")?.GetComponent<Text>();
            if (levelText == null)
            {
                Debug.LogError("Leveltext nicht gefunden! Stelle sicher, dass der Name 'Leveltext' korrekt ist.");
            }
        }

        // Slider initialisieren
        if (xpSlider != null)
        {
            xpSlider.maxValue = maxXP;
            xpSlider.value = currentXP;
            xpSlider.interactable = false; // Slider nicht interaktiv machen
        }

        UpdateLevelText(); // Aktuelles Level im UI anzeigen
        
        Image background = xpSlider.transform.Find("Background")?.GetComponent<Image>();
        if (background != null)
        {
            background.color = Color.gray;
        }

    
        Image fill = xpSlider.fillRect.GetComponent<Image>();
        if (fill != null)
        {
            fill.color = Color.cyan; 
        }
    }

    void Update()
    {
        // Dr�cke X, um XP zu testen
        if (Input.GetKeyDown(KeyCode.X))
        {
            AddXP(10); // 10 XP hinzuf�gen
        }
    }

    // XP hinzuf�gen
    public void AddXP(int xp)
    {
        currentXP += xp;

        // �berpr�fe, ob der Spieler ein Level-Up erreicht
        if (currentXP >= maxXP)
        {
            LevelUp();
        }

        // Slider aktualisieren
        if (xpSlider != null)
        {
            xpSlider.value = currentXP;
        }
    }

    // Logik f�r ein Level-Up
    private void LevelUp()
    {
        currentXP -= maxXP; // �bersch�ssige XP ins n�chste Level �bernehmen
        currentLevel++;     // Spieler-Level erh�hen
        maxXP += 50;        // Optional: XP-Maximum f�r das n�chste Level erh�hen

        // Slider aktualisieren
        if (xpSlider != null)
        {
            xpSlider.maxValue = maxXP;
            xpSlider.value = currentXP;
        }

        UpdateLevelText(); // Level-Text aktualisieren

        Debug.Log("Level Up! Aktuelles Level: " + currentLevel);
    }

    // Level-Text aktualisieren
    private void UpdateLevelText()
    {
        if (levelText != null)
        {
            levelText.text = "Level: " + currentLevel;
        }
    }
}
