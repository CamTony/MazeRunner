using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class volume_script : MonoBehaviour
{

    public AudioMixer mixer;


    // Start is called before the first frame update
    public void setVolume(float volume)
    {
        mixer.SetFloat("Volume", volume);
    }
}
