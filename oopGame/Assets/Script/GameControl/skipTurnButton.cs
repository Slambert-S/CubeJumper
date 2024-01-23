using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skipTurnButton : MonoBehaviour
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

    public void skipPlayerTurn()
    {
        if(GameManager.Instance.State == GameManager.GameState.PlayerTurn)
        {
            SoundFXManager.instance.PlaySoundFXClip(clickAudio, this.transform, 0.75f);
            GameManager.Instance.UpdateGameState(GameManager.GameState.EnvironmentTurn);
            GameManager.Instance.GetComponent<SkinLoadingManager>().playerRef.GetComponent<BaseUnit>().changeHpValue(-1);
        }
    }
}
