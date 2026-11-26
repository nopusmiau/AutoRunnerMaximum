using UnityEngine;
using UnityEngine.InputSystem;


public class LoopBG : MonoBehaviour
{
    
    public float loopSpeed;
    public Renderer renderer;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            StartRunning();
    }

    private void StartRunning()
    {
        renderer.material.mainTextureOffset += new Vector2(loopSpeed * Time.deltaTime, 0f);
    }
}
