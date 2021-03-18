using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUIMainMenu : MonoBehaviour
{
    // ========== MARK: - Class Name Define ==========

    public string GetClassName()
    {
        return this.GetType().Name;
    }

    // ========== MARK: - Fields and Properties ==========

    // ========== MARK: - MonoBehaviour methods ==========

    protected void Awake()
    {
    
    }

    // ========== MARK: On Button Click methods ==========

    public void OnClickNewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OnClickContinue()
    {
        Debug.Log("Continue Pressed");
    }

    public void OnClickExit()
    {
        Debug.Log("Exit Pressed");
    }
}
