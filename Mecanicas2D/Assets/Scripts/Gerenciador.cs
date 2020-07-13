using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gerenciador : MonoBehaviour
{

    public AudioSource sons;
    public static Gerenciador inst = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void Awake()
    {
        if (inst==null)
        {
            inst = this;
        }   else if (inst!=this)
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }




    public void PlayAudio(AudioClip audioClip)
    {

        sons.clip = audioClip;
        sons.Play();
    }
}
