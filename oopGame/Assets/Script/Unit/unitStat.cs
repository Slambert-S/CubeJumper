using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unitStat : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private int maxHP = 10;
    private int _currentHP;
    public int CurrentHP
    {
        get
        {
            return _currentHP;
        }

        
        private set
        {
            _currentHP = value;
            if(textUiManager.Instance != null)
            {
                textUiManager.Instance.updatePlayerHpUi(_currentHP);
            }
            
        }
    }

    public int ChangeHP
    {
        set
        {
            _currentHP += value;
            if(_currentHP > maxHP)
            {
                _currentHP = maxHP;
            }
            if (textUiManager.Instance != null)
            {
                textUiManager.Instance.updatePlayerHpUi(_currentHP);
            }
            if(value < 0)
            {
                StartCoroutine("PlayerLostlifeVisual");
            }
        }
    }
    IEnumerator PlayerLostlifeVisual()
    {
        this.GetComponent<unitMaterielManager>().ActivateRedShader();
        yield return new WaitForSeconds(1);
        this.GetComponent<unitMaterielManager>().DeactivateRedShader();
    }

    private void OnEnable()
    {
        GameManager.OnGameStateChange += checkIfDead;
    }
    private void OnDisable()
    {
        GameManager.OnGameStateChange -= checkIfDead;
    }
    void Start()
    {
        CurrentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
       // checkIfDead();

    }

    private void checkIfDead()
    {
        if(_currentHP <= 0)
        {
            //Debug.Log("DEBUG : You dead");
        }
    }

    private void checkIfDead(GameManager.GameState state)
    {
        if (_currentHP <= 0)
        {
            //Debug.Log("DEBUG : You dead");
            this.GetComponent<BaseUnit>().animationManager.SetDeathAnimation();
            GameManager.Instance.endOfGameManager.endReason = GM_endOfGame.GameEndedState.PlayerDeath;
            GameManager.Instance.UpdateGameState(GameManager.GameState.GameEnded);
            
        }
    }
}


