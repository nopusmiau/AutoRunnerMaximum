using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public Animator _animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // start running when space is pressed (the same key that starts the background and the obstacles)
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
            StartRunning();
    }

    private void StartRunning()
    {
        _animator.SetBool("isRunning",true);
    }
}
