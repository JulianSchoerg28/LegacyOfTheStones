using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public bool IsLocked = true;
    public string sceneName;

    public bool Unlock()
    {
        if (IsLocked)
        {
            IsLocked = false;
            Debug.Log("Door unlocked!");
            return true;

        }
        else
        {
            Debug.Log("Door is already unlocked.");
            return false;
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (IsLocked)
            {
                //TODO: Das als UI :D
                Debug.Log("Door locked! Find or use Key to unlock!");
            }
            else
            {
                Debug.Log("Welcome :D");
                if (sceneName != null)
                {
                    SceneLoader.Instance.LoadScene(sceneName);
                }
            }
        }
    }
}