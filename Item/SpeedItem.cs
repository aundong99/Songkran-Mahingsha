using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedItem : MonoBehaviour
{
    public static event Action<float> OnSpeedCollected;
    public float speedMultiplier = 1.5f;
    [SerializeField] AudioSource pickSound;

    public void Collect()
    {
   
        Debug.Log("SpeedItem Collected!");
        OnSpeedCollected.Invoke(speedMultiplier);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            pickSound.Play();       
        }
    }
}

