using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwervingController : MonoBehaviour
{   
    public static SwervingController instance;

    [SerializeField] Camera _camera;
    [SerializeField] float _swipeSpeed = 100f;
    [SerializeField] Transform _gameObject;
    public Transform _target;

    private Vector3 _lastMousePosition;
    private Vector3 _mousePos;
    private Vector3 _newPosForTrans;
    public bool _isTouch;
   
    

    void Start()
    {
        instance = this;
    }

    
    void Update()
    {
        if (Follower.instance.knockBackCounter <= 0)
        {
            if (Input.GetMouseButton(0))
            {
            
                _mousePos = _camera.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, 0, Input.mousePosition.y));
                float xDiff = _mousePos.x - _lastMousePosition.x;
                _newPosForTrans.x = _target.position.x + xDiff * Time.deltaTime * _swipeSpeed;
                _newPosForTrans.y = _gameObject.position.y;
                _newPosForTrans.z = _gameObject.position.z;
                _target.position = _newPosForTrans;
                _lastMousePosition = _mousePos;
            }
            
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            _isTouch = true;
            
            
        }
    }

    [System.Obsolete]
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            _isTouch = false;
            DestroyObject(collision.gameObject);

        }
    }

}
