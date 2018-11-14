using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goals : MonoBehaviour {
    public Vector2 centreCircle;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Ball")
        {
            Debug.Log("Goal!");
            collider.transform.position = centreCircle;
            collider.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f,0.0f);
        }
    }
}
