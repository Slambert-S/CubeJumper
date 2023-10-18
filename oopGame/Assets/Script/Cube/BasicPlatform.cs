using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlatform : MonoBehaviour
{
    [SerializeField]
    private List<BasicPlatform> neighbourList;
    [SerializeField]
    private Transform PositionReference;

    private bool CanBeWalkedON = true;
    // Start is called before the first frame update
    void Start()
    {
        PositionReference = transform.parent.GetComponentInChildren<REF_centerPoint>().gameObject.transform;
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void getNeighbour()
    {
        //Code that will save a reference to all neighbour
    }

    public  bool CanGoOnTop()
    {
        return CanBeWalkedON;
    }

    public Transform GetPositionOnTop()
    {
        return PositionReference;
    }
}
