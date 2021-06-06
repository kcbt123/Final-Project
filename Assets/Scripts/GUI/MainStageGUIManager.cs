using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainStageGUIManager : MonoBehaviour
{
    public GameObject fullCanvas;

    public GameObject tutorialFullCanvas;
    public RectTransform tutorialContentPanel;

    //private StageData stageData;
    //public List<BlockItemIdentifier> stageDataIdents;

    // ========== Singleton instance ==========
    private static MainStageGUIManager _instance;
    public static MainStageGUIManager instance
    {
        get
        {
            return _instance;
        }
    }
    private MainStageGUIManager()
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
        //stageData = JSONUtil.LoadDataFromJson<StageData>("Stage1Data");

        //for (int i = 0; i < stageData.itemData.Count; i++)
        //{
        //    if (stageData.itemData[i] == "MOVEMENT_UP") { stageDataIdents.Add(BlockItemIdentifier.MOVEMENT_UP); }
        //    else if (stageData.itemData[i] == "MOVEMENT_DOWN") { stageDataIdents.Add(BlockItemIdentifier.MOVEMENT_DOWN); }
        //    else if (stageData.itemData[i] == "MOVEMENT_LEFT") { stageDataIdents.Add(BlockItemIdentifier.MOVEMENT_LEFT); }
        //    else if (stageData.itemData[i] == "MOVEMENT_RIGHT") { stageDataIdents.Add(BlockItemIdentifier.MOVEMENT_RIGHT); }

        //    else if (stageData.itemData[i] == "ACTION_WATERING_YELLOW") { stageDataIdents.Add(BlockItemIdentifier.ACTION_WATERING_YELLOW); }
        //    else if (stageData.itemData[i] == "ACTION_WATERING_WHITE") { stageDataIdents.Add(BlockItemIdentifier.ACTION_WATERING_WHITE); }
        //    else if (stageData.itemData[i] == "ACTION_WATERING_RED") { stageDataIdents.Add(BlockItemIdentifier.ACTION_WATERING_RED); }

        //    else if (stageData.itemData[i] == "SPECIAL_FOR") { stageDataIdents.Add(BlockItemIdentifier.SPECIAL_FOR); }
        //    else if (stageData.itemData[i] == "SPECIAL_IF") { stageDataIdents.Add(BlockItemIdentifier.SPECIAL_IF); }
        //    else if (stageData.itemData[i] == "SPECIAL_FUNCTION") { stageDataIdents.Add(BlockItemIdentifier.SPECIAL_FUNCTION); }
        //}

        //CustomEventSystem.instance.DispatchEvent(EventCode.ON_LOAD_STAGE_DATA_DONE, new object[] {}) ;
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
