using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinLoadingManager : MonoBehaviour
{
    public GameObject playerRef;
    public GameObject partnerRef;
    // Start is called before the first frame update
    void Start()
    {
        if (SkinDatabase.Instance != null)
        {
            loadSkin();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void loadSkin()
    {
        if(SkinDatabase.Instance.getPlayerSkin() >= 0)
        {
            GameObject currentSkinRef = playerRef.transform.GetChild(0).gameObject;
            Instantiate(SkinDatabase.Instance.skinDatabase[SkinDatabase.Instance.getPlayerSkin()].model, currentSkinRef.transform.position, currentSkinRef.transform.rotation, currentSkinRef.transform.parent);
            Destroy(currentSkinRef);
        }

        if (SkinDatabase.Instance.getPartnerSkin() >= 0)
        {
            GameObject currentSkinRef = partnerRef.transform.GetChild(0).gameObject;
            Instantiate(SkinDatabase.Instance.skinDatabase[SkinDatabase.Instance.getPartnerSkin()].model, currentSkinRef.transform.position, currentSkinRef.transform.rotation, currentSkinRef.transform.parent);
            Destroy(currentSkinRef);
        }
    }

}
