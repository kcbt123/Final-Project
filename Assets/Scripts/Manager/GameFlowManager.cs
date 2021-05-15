using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameFlowManager : MonoBehaviour
{
    public string GetClassName()
    {
        return this.GetType().Name;
    }

    // ========== MARK: - Singleton ==========

    /// <summary>
    /// Singleton instance
    /// </summary>
    private static GameFlowManager _instance;

    public static GameFlowManager instance
    {
        get
        {
            return _instance;
        }
    }

    // ========== MARK: - Fields and properties ==========

    [SerializeField]
    private Tilemap flowerTilemap;

    [SerializeField]
    private Tile replacementTile;

    public bool isStageObjectiveCompleted = false;

    private EventListener[] _eventListeners;

    // ========== MARK: - MonoBehaviour Methods ==========

    // Start is called before the first frame update
    private void Awake()
    {
        //AddListeners();
    }

    private void OnDestroy()
    {
        //RemoveListeners();
    }

    // Update is called once per frame
    /** ======= MARK: - Handle Events ======= */

    private void AddListeners()
    {
        _eventListeners = new EventListener[1];
        _eventListeners[0] = CustomEventSystem.instance.AddListener(EventCode.ON_STAGE_FINISHED, this, OnStageFinished);
    }

    private void RemoveListeners()
    {
        if (_eventListeners.Length != 0)
        {
            foreach (EventListener listener in _eventListeners)
                CustomEventSystem.instance.RemoveListener(listener.eventCode, listener);
        }
    }

    private void OnStageFinished(object[] eventParam)
    {
        string timeFinished = (string)eventParam[0];

    }
}
