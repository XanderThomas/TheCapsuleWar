using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttackAnim : ProgAnimatorAbstract {

    [SerializeField]
    private Transform sword;
    [SerializeField]
    private Transform handlePoint;
    [SerializeField]
    private float swingSpeed;
    [SerializeField]
    private Vector3 swingAxis;
    [SerializeField]
    private float swingAngle;

    private Vector3 initialAngle;
    private bool swingBack;
    private float swingDir;
    private float angleAccumulator;



    private void OnValidate()
    {
        animationLength = (swingAngle * 2.1f) / swingSpeed;
    }

    public override void Play()
    {
        if (!Playing)
        {
            initialAngle = sword.localEulerAngles;
            swingBack = false;
            angleAccumulator = 0f;
            swingDir = 1f;
        }

        base.Play();

    }

    public override void ProgUpdate(float dt)
    {
        base.ProgUpdate(dt);

        float angleAdd = swingSpeed * dt;
        angleAccumulator += angleAdd;

        sword.RotateAround(handlePoint.position, transform.TransformDirection(swingAxis), angleAdd * swingDir);

        if(angleAccumulator >= swingAngle)
        {
            if(!swingBack)
            {
                swingBack = true;
                angleAccumulator = 0f;
                swingDir = -1f;
            }
            else
            {
                sword.localEulerAngles = initialAngle;
                Stop();
            }
        }
    }

}
