using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFX : MonoBehaviour
{

    public AudioSource buttonFX;
    public AudioClip clickSound;

    // Start is called before the first frame update
    public void ClickSound()
    {
        buttonFX.PlayOneShot(clickSound);
    }
}
