using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunButtonController : MonoBehaviour
{
    [SerializeField]
    private GameObject _mainSectionObject;

    private GameObject[] _codeBlocks;

    private List<Vector2> _runDirectives;

    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetAllBlock()
    {
        _codeBlocks = new GameObject[_mainSectionObject.transform.childCount];

        for (int i = 0; i < _mainSectionObject.transform.childCount; i++)
        {
            _codeBlocks[i] = _mainSectionObject.transform.GetChild(i).gameObject;
        }
    }
    void AnalyzeCode()
    {
        foreach (GameObject block in _codeBlocks)
        {
            Vector2 vec = new Vector2(0f, 0f);
            if (block.GetComponent<MovementBlockController>()._data.blockIdentifier == MovementBlockIdentifier.UP)
            {
                vec = new Vector2(0f, 1f);
            } else if (block.GetComponent<MovementBlockController>()._data.blockIdentifier == MovementBlockIdentifier.DOWN)
            {
                vec = new Vector2(0f, -1f);
            }
            else if (block.GetComponent<MovementBlockController>()._data.blockIdentifier == MovementBlockIdentifier.LEFT)
            {
                vec = new Vector2(-1f, 0f);
            }
            else if (block.GetComponent<MovementBlockController>()._data.blockIdentifier == MovementBlockIdentifier.RIGHT)
            {
                vec = new Vector2(1f, 0f);
            }

            _runDirectives.Add(vec);
        }
    }

    public void RunCodeArray()
    {
        GetAllBlock();
        AnalyzeCode();
    }
}
