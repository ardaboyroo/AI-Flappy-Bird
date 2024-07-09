﻿using System;
using UnityEngine;

public class Bird : MonoBehaviour
{

    private const float JUMP_AMOUNT = 90f;

    private static Bird instance;

    public static Bird GetInstance()
    {
        return instance;
    }

    public event EventHandler OnDied;
    public event EventHandler OnStartedPlaying;

    private Rigidbody2D birdRigidbody2D;
    private State state;
    private bool autoStart = true;

    private enum State
    {
        WaitingToStart,
        Playing,
        Dead
    }

    private void Awake()
    {
        instance = this;
        birdRigidbody2D = GetComponent<Rigidbody2D>();
        birdRigidbody2D.bodyType = RigidbodyType2D.Static;
        state = State.WaitingToStart;
    }

    private void Update()
    {
        switch (state)
        {
            default:
            case State.WaitingToStart:
                if (autoStart)
                {
                    // Start playing
                    state = State.Playing;
                    birdRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
                    Jump();
                    if (OnStartedPlaying != null) OnStartedPlaying(this, EventArgs.Empty);
                }
                break;
            case State.Playing:

                // Rotate bird as it jumps and falls
                //transform.eulerAngles = new Vector3(0, 0, birdRigidbody2D.velocity.y * .15f);
                break;
            case State.Dead:
                break;
        }
    }

    public void Jump()
    {
        birdRigidbody2D.velocity = Vector2.up * JUMP_AMOUNT;
        SoundManager.PlaySound(SoundManager.Sound.BirdJump);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        int checkpointLayer = 7;
        if (collider.gameObject.layer == checkpointLayer)
        {
            // Touched a checkpoint
        }
        else
        {
            birdRigidbody2D.bodyType = RigidbodyType2D.Static;
            SoundManager.PlaySound(SoundManager.Sound.Lose);
            if (OnDied != null) OnDied(this, EventArgs.Empty);
        }
    }

    public void Reset()
    {
        birdRigidbody2D.velocity = Vector2.zero;
        birdRigidbody2D.bodyType = RigidbodyType2D.Static;
        transform.position = Vector3.zero;
        state = State.WaitingToStart;
    }

    public float GetVelocityY()
    {
        return birdRigidbody2D.velocity.y;
    }

}
