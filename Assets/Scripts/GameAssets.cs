﻿using System;
using UnityEngine;

public class GameAssets : MonoBehaviour
{

    private static GameAssets instance;

    public static GameAssets GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }


    public Sprite pipeHeadSprite;
    public Transform pfPipeHead;
    public Transform pfPipeBody;
    public Transform pfPipeCheckpoint;
    public Transform pfGround;
    public Transform pfCloud_1;
    public Transform pfCloud_2;
    public Transform pfCloud_3;

    public SoundAudioClip[] soundAudioClipArray;

    [Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }
}
