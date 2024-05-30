using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cam;

    [SerializeField] private float parallaxEffect;

    private float xPosition;
    private float yPosition;

    private void Start()
    {
        xPosition = transform.position.x;
        yPosition = transform.position.y;
    }

    private void Update()
    {
        float distanceToMoveX = cam.transform.position.x * parallaxEffect;
        float distanceToMoveY = cam.transform.position.y * parallaxEffect;


        transform.position = new Vector3(xPosition + distanceToMoveX, yPosition + distanceToMoveY);
    }
}
