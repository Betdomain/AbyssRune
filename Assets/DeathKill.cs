using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillPlayer : MonoBehaviour
{
    public int Respawn;

    // Update is called once per frame
    void Start()
    {
        Respawn = CurrentLevel.currentLevel;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            {
            SceneManager.LoadScene(Respawn);
        }
    }
}
