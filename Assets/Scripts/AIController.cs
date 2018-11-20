using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour {
    Vector2 startPos;

	void Start()
    {
        startPos = transform.position;
    }
	
	// Update is called once per frame
	void Update()
    {
		
	}

    public void ResetPosition()
    {
        transform.position = startPos;
    }
}
