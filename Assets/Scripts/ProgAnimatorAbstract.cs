using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProgAnimatorAbstract : MonoBehaviour {

    public bool looped;

    [SerializeField]
    protected float animationLength;
    
    protected float animationTime;

    public float AnimationLength { get { return animationLength; } }
    public bool Playing { get; private set; }



    public virtual void Play()
    {
        if(!Playing)
        {
            Playing = true;
            animationTime = 0f;

            if(ProgAnimatorUpdater.Instance.ProgUpdate == null)
            {
                ProgAnimatorUpdater.Instance.ProgUpdate = ProgUpdate;
            }
            else
            {
                ProgAnimatorUpdater.Instance.ProgUpdate += ProgUpdate;
            }
        }
    }

    public virtual void ProgUpdate(float dt)
    {
        animationTime += dt;

        if (animationTime >= animationLength)
            Stop();
    }

    public virtual void Stop()
    {
        if(Playing)
        {
            Playing = false;
            ProgAnimatorUpdater.Instance.ProgUpdate -= ProgUpdate;
        }
    }

}
