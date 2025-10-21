using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnWall : MonoBehaviour {

	private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
