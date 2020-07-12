﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    public Transform backGround;
    public float speed;
    private Transform cam;
    private Vector3 previewCamPosition;



    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
        previewCamPosition = cam.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        float parallaxX = previewCamPosition.x - cam.position.x;
        float bgTargetX = backGround.position.x + parallaxX;
        Vector3 bgPosition = new Vector3(bgTargetX,backGround.position.y,backGround.position.z);
        backGround.position = Vector3.Lerp(backGround.position,bgPosition,speed *Time.deltaTime);
        previewCamPosition = cam.position;

    }

}
