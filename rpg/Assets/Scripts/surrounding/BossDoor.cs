using UnityEngine;

public class DungeonDoorCondition : MonoBehaviour
{
    // ID der Steine und benötigte Anzahl
    public int stoneId = 7;
    public int requiredStoneCount = 4;

    private Door door; // Referenz auf das vorhandene Door-Skript

    void Start()
    {
        // Hole die Door-Komponente von diesem GameObject
        door = GetComponent<Door>();
        if (door == null)
        {
            Debug.LogError("Kein Door-Skript auf diesem Objekt gefunden!");
        }
    }

    public bool CanUnlockDoor()
    {
        // Prüfe, ob genügend Steine im Inventar sind
        int totalStones = Inventory.Instance.HowManyItemsOfThisTypeTF(stoneId);
        Debug.Log($"Anzahl der Steine im Inventar: {totalStones}");
        return totalStones >= requiredStoneCount;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && door != null)
        {
            if (door.IsLocked)
            {
                if (CanUnlockDoor())
                {
                    door.Unlock(); // Entsperre die Tür über das Door-Skript
                    Debug.Log("Dungeon-Tür entsperrt!");
                }
                else
                {
                    Debug.Log("Du brauchst mindestens 4 Steine, um diese Tür zu entsperren.");
                }
            }
        }
    }
}