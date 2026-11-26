using UnityEngine;

// The one place where the game ends.
// Every obstacle calls GameOver.EndGame() when the player hits it in a wrong way.
// Later the Game Over screen can be shown from here instead of stopping the game.
public static class GameOver
{
    public static void EndGame()
    {
#if UNITY_EDITOR
        // Application.Quit() does nothing inside the editor, so stop Play mode instead
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
