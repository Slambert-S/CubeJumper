using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    private int mouvmentSpeed = 1;  //need to encapsulate

    [Header("Debug Section")]
    public BasicPlatform blockToMoveTO;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            MoveTo(blockToMoveTO);
        }
    }

    public virtual void MoveTo(BasicPlatform platform) {

        if (platform.CanGoOnTop())
        {
            this.transform.position = platform.GetPositionOnTop().position;
        }
    
    }

}
