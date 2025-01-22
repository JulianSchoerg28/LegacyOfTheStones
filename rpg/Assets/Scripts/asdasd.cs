
using UnityEngine;
using UnityEngine.SceneManagement;

public class asdasd : MonoBehaviour
{
    public GameObject boss;

    private void Start()
    {
        Debug.Log(GameManager.Instance.isBossKilled);
        if (GameManager.Instance.isBossKilled)
        {
           Destroy(boss.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        { 
            if(boss == null)
            {
                Application.Quit();
                GameManager.Instance.ClearAllDontDestroyOnLoad();
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}
