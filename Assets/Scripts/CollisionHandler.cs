
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float LoadingDelay = 1f;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] AudioClip successSFX;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;

    bool isControllable = true;
    bool isCollidable = true;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            LoadNextLevel();
        }
        else if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            isCollidable = !isCollidable;
        }
    }

    //Handling Collisions with switch

    void OnCollisionEnter(Collision other)
    {
        if (!isControllable || !isCollidable) {return;}
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
        audioSource.Stop();
        crashParticles.Play();
        isControllable = false;
        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(crashSFX, .3f);
        Invoke("ReloadLevel",LoadingDelay);
        Debug.Log("You Crashed, Reloading...");
    }

   void NextLevelSequence()
    {
        audioSource.Stop();
        successParticles.Play();
        isControllable = false;
        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(successSFX, .3f );
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
