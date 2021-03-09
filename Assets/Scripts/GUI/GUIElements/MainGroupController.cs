using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGroupController : MonoBehaviour
{
    public string GetClassName()
    {
        return this.GetType().Name;
    }

    // ========== MARK: Fields and Properties ==========
    [SerializeField]
    private Button _deleteAllButton;

    [SerializeField]
    private View _mainGroupView;

    [SerializeField]
    private List<CodeBlock> _listCodeBlocks;

    private List<EventListener> _listEventListeners; 

    // ========== MARK: MonoBehaviour methods ==========
    private void Awake()
    {
        AddListeners();
    }

    private void OnDestroy() 
    {
        RemoveListeners();
    }

    // ========== MARK: Listener Controls ==========
    private void AddListeners()
    {
        _listEventListeners = new List<EventListener>();
        _listEventListeners.Add(CustomEventSystem.instance.AddListener(EventCode.ON_BLOCK_ADDED, this, OnBlockAdded));
        _listEventListeners.Add(CustomEventSystem.instance.AddListener(EventCode.ON_BLOCK_SHIFTED, this, OnBlockShifted));
        _listEventListeners.Add(CustomEventSystem.instance.AddListener(EventCode.ON_BLOCK_REMOVED, this, OnBlockRemoved));
        _listEventListeners.Add(CustomEventSystem.instance.AddListener(EventCode.ON_BLOCK_REMOVE_ALL, this, OnBlockRemoveAll));
        _listEventListeners.Add(CustomEventSystem.instance.AddListener(EventCode.ON_EXPAND_TOGGLE, this, OnExpandToggle));

        _listEventListeners.Add(CustomEventSystem.instance.AddListener(EventCode.ON_RESET_GAME, this, OnResetGame));
        _listEventListeners.Add(CustomEventSystem.instance.AddListener(EventCode.ON_MAIN_MENU, this, OnMainMenu));
    }

    private void RemoveListeners()
    {
        foreach (EventListener listener in _eventListeners)
            CustomEventSystem.instance.RemoveListener(listener.eventCode, listener);
    }

    // ========== MARK: Listeners ==========

    private void OnResetGame(object[] eventParam)
    {
        ResetUI();
        gameObject.SetActive(true);
    }

    private void OnMainMenu(object[] eventParam)
    {
        gameObject.SetActive(false);
    }

    private void OnBlockAdded(object[] eventParam)
    {

    }

    private void OnBlockRemoved(object[] eventParam)
    {

    }

    private void OnBlockRemoveAll(object[] eventParam)
    {

    }

    private void OnBlockRemoved(object[] eventParam)
    {

    }

    private void OnExpandToggle(object[] eventParam)
    {
        Sequence moveSequence = DOTween.Sequence();
        moveSequence.Append(transform.DOMoveX(150f, 0.5f));
        moveSequence.SetLoops(-1);

        Sequence collapseSequence = DOTween.Sequence();
        collapseSequence.Append(moveSequence);
        collapseSequence.SetLoops(-1);
        collapseSequence.SetID(DOTweenIdList.GROUP_COLLAPSE);
        collapseSequence.Play();
    }

    // ========== MARK: Other Public Methods ==========
    // ========== MARK: Other Private Methods ==========
    
    private void ResetUI()
    {
        _listCodeBlocks.Clear();
    }
}
