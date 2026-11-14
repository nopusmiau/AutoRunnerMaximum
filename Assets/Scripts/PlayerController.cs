using UnityEngine;
using UnityEngine.InputSystem;

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
        //if(Input.GetKey(KeyCode.Space))
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
            StartRunning();
    }

    private void StartRunning()
    {
        _animator.SetBool("isRunning",true);
    }
}
