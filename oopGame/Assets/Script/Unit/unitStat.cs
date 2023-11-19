using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unitStat : MonoBehaviour
{
    // Start is called before the first frame update
    private int maxHP = 8;
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
        }
    }
    void Start()
    {
        CurrentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
