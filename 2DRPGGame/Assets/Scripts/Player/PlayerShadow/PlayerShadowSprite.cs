using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShadowSprite : MonoBehaviour
{
    private Transform player;

    private SpriteRenderer thisSprite;
    private SpriteRenderer playerSprite;

    private Color color;

    public float activeTime;
    public float activeStart;

    private float alpha;
    public float alphaSet;
    public float alphaMultiplier;

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        thisSprite = GetComponent<SpriteRenderer>();
        playerSprite = player.GetComponent<SpriteRenderer>();

        alpha = alphaSet;
        thisSprite.sprite = playerSprite.sprite;
        transform.position = player.position;
        transform.rotation = player.rotation;

        activeStart = Time.time;
    }

    private void FixedUpdate()
    {
        alpha *= alphaMultiplier;

        color = new Color(1, 1, 1, alpha);
        thisSprite.color = color;

        if (Time.time >= activeStart + activeTime)
        {
            PlayerShadowPool.Instance.ReturnPool(this.gameObject);
        }
    }
}
