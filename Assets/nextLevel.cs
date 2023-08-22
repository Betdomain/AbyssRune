using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextLevel : MonoBehaviour
{
    private void Start()
    {
        
        Debug.Log(CurrentLevel.currentLevel);
    }
    

        private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            CurrentLevel.currentLevel = CurrentLevel.currentLevel + 1;
            SceneManager.LoadScene(CurrentLevel.currentLevel);
        }
    }
}
