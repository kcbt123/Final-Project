using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GUIManager : MonoBehaviour
{

    public RectTransform panelMainMenu;
    public RectTransform panelLevelSelect;
    public RectTransform backgroundImage;

    public GameObject loadingCanvas;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ScrollingBackgroundImageForward());
    }

    // Update is called once per frame

    IEnumerator ScrollingBackgroundImageForward()
    {
        yield return backgroundImage.DOAnchorPos(new Vector2(-5f, 0), 40f);
        yield return new WaitForSeconds(40f);
        StartCoroutine(ScrollingBackgroundImageBackward());
    }

    IEnumerator ScrollingBackgroundImageBackward()
    {
        yield return backgroundImage.DOAnchorPos(Vector2.zero, 40f);
        yield return new WaitForSeconds(40f);
        StartCoroutine(ScrollingBackgroundImageForward());
    }

    public void OnPressStart()
    {
        panelMainMenu.DOAnchorPos(new Vector2(-6f,0), 0.25f);
        panelLevelSelect.DOAnchorPos(Vector2.zero, 0.25f);
    }

    public void OnPressCancelLevelSelect()
    {
        panelMainMenu.DOAnchorPos(Vector2.zero, 0.25f);
        panelLevelSelect.DOAnchorPos(new Vector2(6f, 0), 0.25f);
    }

    public void OnClickNewGame()
    {
        loadingCanvas.SetActive(true);
        StartCoroutine(MoveToMainGameScene());
        
    }

    IEnumerator MoveToMainGameScene()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(1);
    }
}
