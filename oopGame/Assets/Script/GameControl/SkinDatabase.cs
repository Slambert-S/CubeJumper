using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinDatabase : MonoBehaviour
{
    // Start is called before the first frame update
    public static SkinDatabase Instance;
    public PlayerSkin[] skinDatabase;
    [SerializeField]
    private int playerSkin = -1;
    [SerializeField]
    private int partnerSkin = -1;
       
    
    [SerializeField]
    private Dictionary<string, int> indexDictionary = new Dictionary<string, int>();

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        if(SkinDatabase.Instance != null)
        {
            //Execute code here to setUp the data
        }
        int size = skinDatabase.Length;
        for(int i = 0; i < size; i++)
        {
            indexDictionary.Add(skinDatabase[i].name, i);
        }
    }

    public void setPlayerSkin(int skin)
    {
        playerSkin = skin;
    }
    public int getPlayerSkin()
    {
        return playerSkin;
    }


    public void setPartnerSkin(int skin)
    {
        partnerSkin = skin;
    }

    public int getPartnerSkin()
    {
        return partnerSkin;
    }
    public int ConvertNameToIndex(string keyName)
    {
        if (indexDictionary.TryGetValue(keyName, out int value))
        {
            return value;
        }
        else
        {
            return -1;
        }
        
    }
}
