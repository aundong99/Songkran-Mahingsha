using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCoiny : MonoBehaviour
{
    [SerializeField] float rotateSpeed;
    [SerializeField] AudioSource pickSound;

    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotateSpeed, 0, Space.World);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            pickSound.Play();
            Destroy(gameObject);
            ControlData.Score += 10;
            Debug.Log("Score: " + ControlData.Score); //Check your score in the Console
        }
    }
}
