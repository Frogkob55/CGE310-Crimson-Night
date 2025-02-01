using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keybutton : MonoBehaviour
{
    [SerializeField] DoorOpen _doorOpen;
    
    [SerializeField] bool _isDoorOpen;
    [SerializeField] bool _isDoorClosed;

    float _switchSizeY;
    Vector3 _switchUpPos;
    Vector3 _switchDownPos;
    float _switchSpeed = 1f;
    float _swirchDelay = 0.2f;
    bool _isPressingSwitch = false;

    bool _isDoorLocked = true;

    [SerializeField] InventoryManger.AllTiems _requiredItem;

    // Start is called before the first frame update
    void Start()
    {
        _switchSizeY = transform.localScale.y / 10;

        _switchUpPos = transform.position;
        _switchDownPos = new Vector3(transform.position.x, transform.position.y - _switchSizeY, transform.position.z);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_isPressingSwitch)
        {
            MoveSwitchDown();
        }
        else if (!_isPressingSwitch)
        {
            MoveSwitchUp();
        }
    }
    void MoveSwitchDown()
    {
        if (transform.position != _switchDownPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, _switchDownPos, _switchSpeed * Time.deltaTime);
        }
    }
    void MoveSwitchUp()
    {
        if (transform.position != _switchUpPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, _switchUpPos, _switchSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag ("Player"))
        {
            _isPressingSwitch = ! _isPressingSwitch;
            if (!_isDoorLocked)
            {
                if (_isDoorOpen && !_doorOpen._isDoorOpen)
                {
                    _doorOpen._isDoorOpen = !_doorOpen._isDoorOpen;
                }
                else if (_isDoorClosed && !_doorOpen._isDoorOpen)
                {
                    _doorOpen._isDoorOpen = !_doorOpen._isDoorOpen;
                }

            }
          
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            StartCoroutine(SwitchUpDelay(_swirchDelay));
        }
    }

    IEnumerator SwitchUpDelay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _isPressingSwitch = false;
    }

    public void DoorLockedStatus()
    {
        _isDoorLocked = !_isDoorLocked;
    }
}
