using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSophie : MonoBehaviour
{
    public GameObject ShopPanel;

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            ToggleShopPanel();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShopPanel.SetActive(false);
            GameManager.Instance.PlayerCanMove = true;

        }
    }

    public void ToggleShopPanel()
    {
        ShopPanel.SetActive(!ShopPanel.activeSelf);
        GameManager.Instance.PlayerCanMove = !ShopPanel.activeSelf;
    }
}
