using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AudioController : MonoBehaviour
{

    [Header("AudioSource")]
    public AudioSource sMusic;  //fonte de musica
    public AudioSource sFx;  //fonte de efeitos sonoros

    [Header("Musicas")]
    public AudioClip musicaTitulo;
    public AudioClip musicaFase1;

    [Header("Fx")]
    public AudioClip fxClick;
    public AudioClip fxSword;
    public AudioClip fxAxe;
    public AudioClip fxBow;
    public AudioClip fxStaff;


    //configuracao dos audios
    public float volumeMaximoMusica;
    public float volumeMaximoFx;

    //configuracoes da troca de musica
    private AudioClip novaMusica;
    private string novaCena;
    private bool trocarCena;



    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        if (PlayerPrefs.GetInt("valoresInciais")==0) {

            PlayerPrefs.SetInt("valoresIniciais",1);
            PlayerPrefs.SetFloat("volumeMaximoMusica", 1);
            PlayerPrefs.SetFloat("volumeMaximoFx", 1);
        } 



        //carrega as configuracoes de audio do aparelho 
        
        volumeMaximoMusica = PlayerPrefs.GetFloat("volumeMaximoMusica");
        volumeMaximoFx = PlayerPrefs.GetFloat("volumeMaximoFx");
        /*
         sMusic.clip = musicaTitulo;
         sMusic.volume = volumeMaximoMusica;
         sMusic.Play();
         */

        trocarMusica(musicaTitulo,"Titulo",true);
    }

    public void trocarMusica(AudioClip clip,string nomeCena,bool mudarCena)
    {
        novaMusica = clip;
        novaCena = nomeCena;
        trocarCena = mudarCena;

        StartCoroutine("changeMusic");
    }


    IEnumerator changeMusic()
    {
        for (float volume=volumeMaximoMusica; volume >=0; volume -=0.1f)
        {
            yield return new WaitForSecondsRealtime(0.1f);
            sMusic.volume = volume;

        }
        sMusic.volume = 0;
        sMusic.clip = novaMusica;
        sMusic.Play();


        for (float volume = 0; volume < volumeMaximoMusica; volume += 0.1f)
        {
            yield return new WaitForSecondsRealtime(0.1f);
            sMusic.volume = volume;

        }

        sMusic.volume = volumeMaximoMusica;


        if (trocarCena ==true)
        {
           
            SceneManager.LoadScene(novaCena);
        }
    }

    public void tocarFx(AudioClip fx,float volume)
    {
        float tempVolume=volume;
        if (volume > volumeMaximoFx)
        {
            tempVolume = volumeMaximoFx;
        }
        sFx.volume = tempVolume;
        sFx.PlayOneShot(fx);
    }

}
