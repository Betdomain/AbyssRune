using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectionZone : MonoBehaviour
{

    public UnityEvent noCollidersRemain;
    
    
    public List<Collider2D> detectColliders = new List<Collider2D>();
    Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        detectColliders.Add(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        detectColliders.Remove(collision);

        if (detectColliders.Count <= 0)
        {
            noCollidersRemain.Invoke();
        }

    }

}
