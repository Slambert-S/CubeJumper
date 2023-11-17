using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinDatabase : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerSkin[] skinDatabase;
    public PlayerSkin playerSkin
    {
        get
        {
            return playerSkin;
        }
    }

    private Dictionary<string, int> indexDictionary = new Dictionary<string, int>();

    void Start()
    {
        int size = skinDatabase.Length;
        for(int i = 0; i < size; i++)
        {
            indexDictionary.Add(skinDatabase[i].name, i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
