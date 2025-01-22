using UnityEngine;

public class BossHealthBar : MonoBehaviour
{
    public Transform boss; // Der Transform des Bosses
    public Vector3 offset = new Vector3(-0.5f, 2f, 0);

    private RectTransform healthBarTransform;

    void Start()
    {
        if (boss == null)
        {
            Debug.LogError("Boss Transform ist nicht zugewiesen!");
            return;
        }

        healthBarTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (boss != null && healthBarTransform != null)
        {
            // Konvertiere die Weltposition des Bosses in Bildschirmkoordinaten
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(boss.position + offset);
            healthBarTransform.position = screenPosition;
        }
    }
}