using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    private Vector3 previewCamPosition;

    [Header("Configuração de Background")]
    public Transform background;

    [Header("Configuração do Paralax")]
    public float parallaxScale;
    public float velocidade;
    public Transform cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
        previewCamPosition = cam.position;
        

    }

    // Update is called once per frame
    void LateUpdate()
    {

        float parallaxX = (previewCamPosition.x - cam.position.x) * parallaxScale;
        float bgTargetX = background.position.x + parallaxX;

        Vector3 bgPos = new Vector3(bgTargetX, background.position.y, background.position.x);
        background.position = Vector3.Lerp(background.position,bgPos,velocidade * Time.deltaTime);
        previewCamPosition = cam.position;
    }




}
