using UnityEngine;
using UnityEngine.InputSystem;

public class QuitApplication : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.IsPressed())
        {
            Debug.Log("Quit The Application");
            Application.Quit();
        }
    }
}
