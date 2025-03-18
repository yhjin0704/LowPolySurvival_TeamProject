using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    
    private Rigidbody _rigidbody;
    public float footstepThreshold;
    public float footstepRate;
    private float footStepTime;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Mathf.Abs(_rigidbody.velocity.y) < 0.1f)
        {
            if (_rigidbody.velocity.magnitude > footstepThreshold)
            {
                if (Time.time - footStepTime > footstepRate)
                {
                    footStepTime = Time.time;
                    AudioManager.Instance.PlayFootSteps();
                }
            }
        }
    }
}
