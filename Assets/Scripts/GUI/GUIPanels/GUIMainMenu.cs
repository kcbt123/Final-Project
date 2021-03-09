using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIMainMenu : MonoBehaviour
{
    public string GetClassName()
    {
        return this.GetType().Name;
    }

    // ========== MARK: Fields and Properties ==========
    [SerializeField]
    private GameObject _titleObject;

    private Vector3[] _titleTextPosition;
    private List<EventListener> _listListeners; 

    // ========== MARK: MonoBehaviour methods ==========
    protected void Awake()
    {
        Transform titleTransform = _titleObject.transform;
        _titleTextPosition = new Vector3[titleTransform.childCount];
        for (int i = 0; i < titleTransform.childCount; i++)
            _titleTextPosition[i] = titleTransform.GetChild(i).position;

        AddListeners();
        PrepareDoEffectIn();    
    }

    private void Start()
    {
        DoEffectIn();
    }

    protected void OnDestroy()
    {
        RemoveListeners();
    }

    // ========== MARK: On Button Click methods ==========
    public void OnStartClick()
    {
        float duration = 0.3f;
        float titleDuration = 0.8f;
        DoEffectOut(titleDuration, duration);
        Sequence onRestartSequence = DOTween.Sequence();
        onRestartSequence.AppendInterval(titleDuration + 0.1f);
        onRestartSequence.AppendCallback(() =>
        {
            GameFlowManager.instance.OnStartGame();
        });
        onRestartSequence.Play();
    }


    // ========== MARK: Event Listener methods ==========
    protected void AddListeners()
    {
        _listeners = new List<EventListener>();
        _listeners.Add(CustomEventSystem.instance.AddListener(EventCode.ON_MAIN_MENU, this, OnMainMenu, false));
    }

    protected void RemoveListeners()
    {
        foreach (EventListener listener in _listeners)
            CustomEventSystem.instance.RemoveListener(listener.eventCode, listener);
    }

    // 
    
    protected void OnMainMenu(object[] eventParam)
    {
        DoEffectIn();
    }

    protected void DoEffectIn(float titleDuration = 0.8f, float duration = 0.3f)
    {
        PrepareDoEffectIn();


    }

    protected void DoEffectOut(float titleDuration = 0.8f, float duration = 0.3f)
    {

    }

    protected void PrepareDoEffectTitleIn()
    {

    }
}
