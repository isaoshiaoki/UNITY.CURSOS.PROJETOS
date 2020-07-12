using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private Camera cam;  
    public float speedCam;
    public Transform playerTransform;

    public Transform limiteCamEsc,limiteCamDir,limiteCamSup,limiteCamBaixo;

    [Header("Audio")]
    public AudioSource sfxSource;
    public AudioSource musicSource;
    public AudioClip sfxJump;
    public AudioClip sfxAtack;
    public AudioClip[] sfxStep;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     //no mesmo frame
    private void LateUpdate()
    {
        //  Vector3 posCam = new Vector3(playerTransform.position.x,playerTransform.position.y,cam.transform.position.z);
        //cam.transform.position = posCam; 

      camController();


    }


    void camController()
    {

        float posCamX = playerTransform.position.x;
        float posCamY = playerTransform.position.y;

        if (cam.transform.position.x < limiteCamEsc.position.x && playerTransform.position.x < limiteCamEsc.position.x)
        {
            posCamX = limiteCamEsc.position.x;
        }
        else if (cam.transform.position.x > limiteCamDir.position.x && playerTransform.position.x < limiteCamDir.position.x)
        {
            posCamX = limiteCamDir.position.x;
        }

        if (cam.transform.position.y < limiteCamBaixo.position.y && playerTransform.position.y < limiteCamBaixo.position.y)
        {
            posCamY = limiteCamBaixo.position.y;
        }
        else if (cam.transform.position.y > limiteCamSup.position.y && playerTransform.position.y > limiteCamSup.position.y)
        {
            posCamY = limiteCamSup.position.y;
        }




        Vector3 posCam = new Vector3(posCamX, posCamY, cam.transform.position.z);
        cam.transform.position = Vector3.Lerp(cam.transform.position, posCam, speedCam * Time.deltaTime);

    }

   public void playSFX(AudioClip sfxClip,float volume)
    {
        sfxSource.PlayOneShot(sfxClip,volume);
    }

}
