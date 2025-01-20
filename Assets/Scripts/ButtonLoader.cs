using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLoader : MonoBehaviour
{
    public void LoadScene()
    {
        SceneManager.LoadScene("Day1");
    }
    private void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
