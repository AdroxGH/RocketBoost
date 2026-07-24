using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float LoadingDelay = 1f;
    [SerializeField] AudioClip crashed;
    [SerializeField] AudioClip levelComplete;

    AudioSource audioSource;

    bool isControllable = true;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    //Handling Collisions with switch

    void OnCollisionEnter(Collision other)
    {
        if (!isControllable) {return;}
        {
            switch (other.gameObject.tag)
            {
            case "Friendly":
                Debug.Log("Im all good");
                break;
            case "Finish":
                NextLevelSequence();
                break;
            default:
                StartCrashSequence();
                
                break;
            
            }
        }
        
    }

    //To reset the Level after a delay
    void StartCrashSequence()
    {
        //todo add sfx and particles
        audioSource.Stop();
        isControllable = false;
        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(crashed, .3f);
        Invoke("ReloadLevel",LoadingDelay);
        Debug.Log("You Crashed, Reloading...");
    }

   void NextLevelSequence()
    {
        //todo add sfx and particles
        audioSource.Stop();
        isControllable = false;
        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(levelComplete, .3f );
        Invoke("LoadNextLevel",LoadingDelay);
        Debug.Log("YOU MADE IT, LOADING NEXT LEVEL...");
    }
   
   
   
   
   
    //Method to Load Next Level after reaching finish
    void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;
        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        } 
         SceneManager.LoadScene(nextScene);

     }
    

        //Reload Level Method
    void ReloadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentScene);
    }
       
     

}
