using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class menuManager : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void GoToGameSceneDebug()
    {
        SceneManager.LoadScene(1);
    }

    public void GoToMenuSceneDebug()
    {
        SceneManager.LoadScene(0);
    }
}

