using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Stage1GUIManager : MonoBehaviour
{
    public GameObject fullCanvas;

    public GameObject tutorialFullCanvas;
    public RectTransform tutorialContentPanel;

    // ========== Singleton instance ==========
    private static Stage1GUIManager _instance;
    public static Stage1GUIManager instance
    {
        get
        {
            return _instance;
        }
    }
    private Stage1GUIManager()
    {
        if (_instance == null)
            _instance = this;
    }


    private void Awake()
    {
        //if (fullCanvas == null)
        //{
        //    DontDestroyOnLoad(gameObject);
        //    //fullCanvas = this;
        //}
        //else if (fullCanvas != this)
        //{
        //    Destroy(gameObject);
        //}
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int currentStep = 1;
    public void OnNextTutorialStep()
    {
        if (currentStep < 4)
        {
            int xCoordinate = -1 * 450 * currentStep;
            tutorialContentPanel.DOAnchorPos(new Vector2(xCoordinate, 0), 0.25f);
            currentStep++;
        } else
        {
            tutorialFullCanvas.SetActive(false);
        }
        
    }
}
