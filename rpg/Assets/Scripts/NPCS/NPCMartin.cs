using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class NPCMartin : MonoBehaviour
{
    
    private bool isInteracting = false;
    
    public GameObject itemPrefab;
    public Transform npcTransform; 
    public Vector3 spawnOffset = new Vector3(10f, 0f, 0f); 
  
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        { 
            isInteracting = true;
            StartCoroutine(WaitAndGiveReward());
        }
    }
    
    private IEnumerator WaitAndGiveReward()
    {
        Debug.Log("Interaction started. Waiting 10 seconds...");
        yield return new WaitForSeconds(1f);

        GiveReward();

        isInteracting = false;
    }

    private void GiveReward()
    {
        if (itemPrefab != null && npcTransform != null)
        {
            Vector3 spawnPosition = npcTransform.position + spawnOffset;
            Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogError("ItemPrefab or NPCTransform is not assigned!");
        }
    }
    
}
