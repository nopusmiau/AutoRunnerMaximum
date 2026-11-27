using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public Animator _animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartRunning();
    }

    // Update is called once per frame
    void Update(){
    }

    private void StartRunning()
    {
        _animator.SetBool("isRunning",true);
    }

}
