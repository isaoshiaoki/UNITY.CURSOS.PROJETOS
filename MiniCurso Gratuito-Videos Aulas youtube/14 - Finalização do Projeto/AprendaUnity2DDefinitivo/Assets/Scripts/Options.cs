using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Options : MonoBehaviour
{
    public Slider volumeMusica;
    public Slider volumeFx;
    private AudioController audioController;
    // Start is called before the first frame update
    void Start()
    {
        audioController = FindObjectOfType(typeof(AudioController)) as AudioController;
        volumeMusica.value = audioController.volumeMaximoMusica;
        volumeFx.value = audioController.volumeMaximoFx;

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void alterarValorMusica() {

        float tempVolumeMusica = volumeMusica.value;
        audioController.volumeMaximoMusica = tempVolumeMusica;
        audioController.sMusic.volume = tempVolumeMusica;


        PlayerPrefs.SetFloat("volumeMaximoMusica", tempVolumeMusica);
       

    }

    public void alterarVolumeFx()
    {
        float tempVolumeFx = volumeFx.value;
        audioController.volumeMaximoFx = tempVolumeFx;
        
        PlayerPrefs.SetFloat("volumeMaximoFx", tempVolumeFx);
    }

}
