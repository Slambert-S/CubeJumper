using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerSetUp : MonoBehaviour
{
    // Start is called before the first frame update
    public static AudioManagerSetUp Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
   
}
