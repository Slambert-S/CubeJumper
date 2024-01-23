using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paralaxScript : MonoBehaviour
{
    [SerializeField] private float _scrollSpeed = 5f;
    private Vector3 _startPos;
    private float reapeatHeight;

    // Start is called before the first frame update
    void Start()
    {
        _startPos = transform.position;
        reapeatHeight = GetComponent<BoxCollider>().size.z* this.transform.localScale.z / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.z < _startPos.z - reapeatHeight)
        {
            transform.position = _startPos;
        }
        
        transform.Translate(Vector3.back * _scrollSpeed * Time.deltaTime);
    }
}
