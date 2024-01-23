using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class menuManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip clickAudio;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void GoToGameSceneDebug()
    {
        SoundFXManager.instance.PlaySoundFXClip(clickAudio, this.transform, 0.75f);
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void GoToMenuSceneDebug()
    {
        SoundFXManager.instance.PlaySoundFXClip(clickAudio, this.transform, 0.75f);
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    
    public void debugging()
    {
        Debug.Log("Click on selected");
    }

    public void QuitApplication()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

