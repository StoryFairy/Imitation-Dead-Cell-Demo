using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Boss_AnimationTriggers : MonoBehaviour
{
    private SpriteRenderer sr => GetComponent<SpriteRenderer>();
    private Enemy_Boss enemy => GetComponent<Enemy_Boss>();

    private void Relocate() => enemy.FindPosition();

    private void MakeInvisible() => MakeTransprent(true);
    
    private void MakeVisible() => MakeTransprent(false);


    private void MakeTransprent(bool transprent)
    {
        if (transprent)
            sr.color = Color.clear;
        else 
            sr.color = Color.white;
    }

}
