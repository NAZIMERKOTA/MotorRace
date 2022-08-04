using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using System;

public class Follower : MonoBehaviour
{

    public static Follower instance;

    [SerializeField] PathCreator pathCreator;
    [SerializeField] float speed = 0.001f;
    [SerializeField] float knockBackTime = 1f;
    public float knockBackCounter = 0f;
    float distanceTraveled;


    [SerializeField]  Transform backWheel;
    [SerializeField] Transform frontWheel;
    [SerializeField] GUIStyle speedStyle;

    [Range(0, 20)]
    [SerializeField] float _time = 0;
    [SerializeField] Rigidbody rb;






    private void Update()
    {
        instance = this;
        Move();
        Rigidbody rb = GetComponent<Rigidbody>();  
    }


    private void Move()
    {

        if (SwervingController.instance._isTouch == true)
        {
            _time = 0.1f;

            rb.AddForce(new Vector3(0, 5, -5f) * _time * 10, ForceMode.Impulse);
            knockBackCounter = knockBackTime;


        }

        knockBackCounter -= Time.deltaTime;

        if (knockBackCounter <= 0)
        {
            if (Input.GetMouseButton(0))
            {

                _time += Time.deltaTime;
                distanceTraveled += speed * _time * _time;
                //backWheel.Rotate(600f * Time.deltaTime, 0.0f, 0.0f);
                //frontWheel.Rotate(600 * Time.deltaTime, 0.0f, 0.0f);

            }
            else
            {
                _time -= Time.deltaTime;
                distanceTraveled += speed * _time * _time;

            }

            transform.position = pathCreator.path.GetPointAtDistance(distanceTraveled);
            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTraveled);

            if (_time >= 20)
            {
                _time = 20;
            }

            if (_time <= 0)
            {
                _time = 0;
            }
        }

    }


    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, Screen.width, 50), Convert.ToInt32(_time) * 6 + "", speedStyle);
    }



}
