using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgAnimatorUpdater : MonoBehaviour {

	public static ProgAnimatorUpdater Instance { get; private set; }

    public delegate void ProgUpdateDelegate(float dt);

    public ProgUpdateDelegate ProgUpdate;



    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if(ProgUpdate != null)
        {
            ProgUpdate(Time.deltaTime);
        }
    }

}
