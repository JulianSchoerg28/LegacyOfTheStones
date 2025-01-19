using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    private Transform player;
    private Vector3 offset = new Vector3(0, 0, -10); 

    void Start() {
        
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogWarning("No object with tag 'Player' found!");
        }
        
        
        if (offset == Vector3.zero) {
            offset = new Vector3(0, 0, -10);
        }
    }


    void LateUpdate() {
        if (player != null) {
            transform.position = player.position + offset;
        }
    }
}
