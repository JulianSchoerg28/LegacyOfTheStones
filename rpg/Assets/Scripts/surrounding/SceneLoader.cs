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
        
        Debug.Log(spawnPoint.transform.position);
        
        if (player != null && spawnPoint != null)
        {
            player.transform.position = spawnPoint.transform.position; 
            Debug.Log("Hier");
        }

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
