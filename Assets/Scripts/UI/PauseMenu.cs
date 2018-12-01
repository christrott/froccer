using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {
    private GameObject menuContainer;

	// Use this for initialization
	void Start () {
        Time.timeScale = 1.0f;
        menuContainer = transform.GetChild(0).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Escape))
        {
            Time.timeScale = 0.0f;
            menuContainer.SetActive(true);
        }
	}

    public void Resume()
    {
        Time.timeScale = 1.0f;
        menuContainer.SetActive(false);
    }
}
