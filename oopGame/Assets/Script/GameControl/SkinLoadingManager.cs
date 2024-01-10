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
            GameObject playerModel =Instantiate(SkinDatabase.Instance.skinDatabase[SkinDatabase.Instance.getPlayerSkin()].model, currentSkinRef.transform.position, currentSkinRef.transform.rotation, currentSkinRef.transform.parent);
            Destroy(currentSkinRef);
            Rigidbody playerRB = playerModel.AddComponent<Rigidbody>();
            BoxCollider playerBoxColider = playerModel.AddComponent<BoxCollider>();
            playerBoxColider.center = new Vector3(0, 1.5f, 0);
            playerBoxColider.size = new Vector3(1.5f, 3.0f, 1.5f);
            playerRB.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            playerModel.layer = 3;
            // playerModel.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationY;
            playerModel.AddComponent<unitModelColision>();
        }

        if (SkinDatabase.Instance.getPartnerSkin() >= 0)
        {
            GameObject currentSkinRef = partnerRef.transform.GetChild(0).gameObject;
            Instantiate(SkinDatabase.Instance.skinDatabase[SkinDatabase.Instance.getPartnerSkin()].model, currentSkinRef.transform.position, currentSkinRef.transform.rotation, currentSkinRef.transform.parent);
            Destroy(currentSkinRef);
        }
    }

}
