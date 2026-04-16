using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonWorks : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnResumeClicked()
    {
        // Talk directly to the global Instance
        if (GameStateController.Instance != null)
        {
            GameStateController.Instance.ResumeGame();
        }
    }



    public void OnSaveClicked()
    {
        // Talk directly to the global Instance
        if (GameStateController.Instance != null)
        {
            GameStateController.Instance.SaveGame();
        }
    }


    public void OnLoadClicked()
    {
        // Talk directly to the global Instance
        if (GameStateController.Instance != null)
        {
            GameStateController.Instance.LoadGame();
        }
    }






    public void QuitGame()
    {
        Debug.Log("Quit Button Pressed!");

        // This tells Unity to stop the 'Play' mode in the Editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                // This closes the actual .exe or .app file
                Application.Quit();
#endif
    }
}