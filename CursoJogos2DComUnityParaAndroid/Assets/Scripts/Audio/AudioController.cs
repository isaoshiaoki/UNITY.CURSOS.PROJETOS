using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource sons;
    public static AudioController inst = null;


    void Awake()
    {
        if (inst==null)
        {
            inst = this;
        } else if (inst != this)
        {
            Destroy(gameObject);
        }

        print(this);

        //print("Awake");
    }

      public void PlayAudio(AudioClip clipAudio)
    {
        sons.clip = clipAudio;
        sons.Play();
    }



    void Start()
    {
        print("Start");
    }


}
