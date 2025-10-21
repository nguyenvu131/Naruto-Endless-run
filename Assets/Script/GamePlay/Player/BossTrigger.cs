using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            PlayerController.Instance.StopRunning();
            Debug.Log("Player stopped by Boss trigger!");
            
        }
    }
}
