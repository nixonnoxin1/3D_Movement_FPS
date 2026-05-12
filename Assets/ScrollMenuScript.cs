using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollMenuScript : MonoBehaviour
{
    public GameObject ScrollMenu;

    bool ScrollCanvasActive;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !ScrollCanvasActive)
        {
            ScrollCanvasActive = true;
            ScrollMenu.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }else if (Input.GetKeyDown(KeyCode.P) && ScrollCanvasActive)
        {
            ScrollCanvasActive = false;
            ScrollMenu.SetActive(false);
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
