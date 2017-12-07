using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowFollow : MonoBehaviour {

    public Transform target;
    public float height = -490.0f;

	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        if (target != null)
        {
            Vector3 follow = new Vector3(target.position.x, height, target.position.z);
            transform.position = follow;
        }
        else
        {
            Destroy(this);
        }
	}
}
