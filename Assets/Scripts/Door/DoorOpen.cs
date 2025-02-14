using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{

    public bool _isDoorOpen = false;
    Vector3 _doorClosedpos;
    Vector3 _doorOpenPos;
    float _doorSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        _doorClosedpos = transform.position;
        _doorOpenPos = new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (_isDoorOpen)
        {
            OpenDoor();
        }
        else if (_isDoorOpen)
        {
            CloseDoor();
        }
    }
    void OpenDoor()
    {
        if(transform.position !=_doorOpenPos)
        {
            transform.position = Vector3.MoveTowards(transform.position,_doorOpenPos,_doorSpeed * Time.deltaTime);
        }
    }

    void CloseDoor()
    {
        if (transform.position !=_doorClosedpos)
        {
            transform.position = Vector3.MoveTowards(transform.position, _doorClosedpos, _doorSpeed * Time.deltaTime);
        }
    }
}
