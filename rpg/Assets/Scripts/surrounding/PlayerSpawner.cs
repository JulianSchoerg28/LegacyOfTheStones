using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public Transform spawnPoint;
    
    private void Start()
    {
        string selectedAvatar = PlayerPrefs.GetString("SelectedAvatar", "DefaultAvatar");
        GameObject avatarPrefab = Resources.Load<GameObject>($"Avatars/{selectedAvatar}");

        if (avatarPrefab != null && spawnPoint != null)
        {
            Instantiate(avatarPrefab, spawnPoint.position, Quaternion.identity);
            Debug.Log($"Spawned avatar: {selectedAvatar} at {spawnPoint.position}");
        }
        else
        {
            Debug.LogError("Avatar prefab or spawn point not found!");
        }
    }
}