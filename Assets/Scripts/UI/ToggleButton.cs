using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour {
    public AudioMixer mixer;

    private bool muted;
    private Image mutedImage;

	// Use this for initialization
	void Start () {
        muted = false;
        mutedImage = transform.GetChild(0).GetComponent<Image>();
        mutedImage.enabled = false;
    }

    public void ToggleAudioGroup(string paramName)
    {
        Debug.Log(paramName + ": " + muted);
        if (muted)
        {
            mixer.SetFloat(paramName, 0.0f);
            muted = false;
            mutedImage.enabled = false;
        }
        else
        {
            mixer.SetFloat(paramName, -80.0f);
            muted = true;
            mutedImage.enabled = true;
        }
    }
}
