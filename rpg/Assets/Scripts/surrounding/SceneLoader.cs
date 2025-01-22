using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }

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

    public void LoadScene(string sceneName)
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(sceneName);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject spawnPoint = GameObject.FindWithTag("SpawnPoint");
        GameObject player = GameObject.FindWithTag("Player");

        if (spawnPoint == null)
        {
            Debug.LogError("Kein SpawnPoint in der Szene gefunden!");
        }
        else
        {
            Debug.Log($"SpawnPoint gefunden an Position: {spawnPoint.transform.position}");
        }

        if (player != null && spawnPoint != null)
        {
            player.transform.position = spawnPoint.transform.position;
            Debug.Log("Spieler wurde an den SpawnPoint gesetzt.");
        }
        else
        {
            if (player == null)
                Debug.LogError("Kein Spieler in der Szene gefunden!");

            if (spawnPoint == null)
                Debug.LogError("Kein SpawnPoint in der Szene gefunden!");
        }
        
        // Debug.Log($"Aktive Main Quest: {QuestManager.Instance.ActiveMainQuest}, ID: {QuestManager.Instance.currentMainQuestID}");
        // QuestManager.Instance.UpdateQuestUI();
        QuestManager.Instance.FindAndAssignUIElements();
        QuestManager.Instance.UpdateQuestUI();
        
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}

