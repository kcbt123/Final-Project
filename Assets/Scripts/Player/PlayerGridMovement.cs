using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerGridMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject _mainSectionObject;

    private GameObject[] _codeBlocks;

    private List<Vector3> _runDirectives;

    [SerializeField]
    private float moveSpeed = 5f;

    [SerializeField]
    private Transform movePoint;

    [SerializeField]
    private LayerMask whatStopsMovement;

    [SerializeField]
    private Tilemap flowerTilemap;

    [SerializeField]
    private Tile replacementTile;

    // Condition Tiles

    [SerializeField]
    private Tile conditionTile1;

    [SerializeField]
    private Tile conditionTile2;

    [SerializeField]
    private Tile conditionTile3;

    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        
        if (Vector3.Distance(transform.position, movePoint.position) <= .5f)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f) 
            {
            
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, whatStopsMovement))
                {
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                }
            }
    
            if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f) 
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, whatStopsMovement))
                {
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                }
            }
        }

        // Test
        // Log ô đang đứng + Xóa ô hoa
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            RemoveFlower(); 
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("Begin to move");
            StartCoroutine(MoveUpAfter(2f));
        }
    }

    IEnumerator MoveUpAfter(float x)
    {
        yield return new WaitForSeconds(x);
        MoveUpSingle();
        Debug.Log("Done moving up");
    }
    IEnumerator MoveDownAfter(float x)
    {
        yield return new WaitForSeconds(x);
        MoveDownSingle();
        Debug.Log("Done moving down");
    }
    IEnumerator MoveLeftAfter(float x)
    {
        yield return new WaitForSeconds(x);
        MoveLeftSingle();
        Debug.Log("Done moving left");
    }
    IEnumerator MoveRightAfter(float x)
    {
        yield return new WaitForSeconds(x);
        MoveRightSingle();
        Debug.Log("Done moving right");
    }

    /** ===== MARK: - Move By Code Functions ===== */
    void MoveUpSingle()
    {
        Vector3 moveVector = new Vector3(0f, 1f, 0f);
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, movePoint.position) <= .5f)
        {
            if (!Physics2D.OverlapCircle(movePoint.position + moveVector, .2f, whatStopsMovement))
            {
                 movePoint.position += moveVector;
            }
        }
    }
    void MoveDownSingle()
    {
        Vector3 moveVector = new Vector3(0f, -1f, 0f);
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, movePoint.position) <= .5f)
        {
            if (!Physics2D.OverlapCircle(movePoint.position + moveVector, .2f, whatStopsMovement))
            {
                 movePoint.position += moveVector;
            }
        }
    }

    void MoveLeftSingle()
    {
        Vector3 moveVector = new Vector3(-1f, 0f, 0f);
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, movePoint.position) <= .5f)
        {
            if (!Physics2D.OverlapCircle(movePoint.position + moveVector, .2f, whatStopsMovement))
            {
                 movePoint.position += moveVector;
            }
        }
    }

    void MoveRightSingle()
    {
        Vector3 moveVector = new Vector3(1f, 0f, 0f);
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, movePoint.position) <= .5f)
        {
            if (!Physics2D.OverlapCircle(movePoint.position + moveVector, .2f, whatStopsMovement))
            {
                 movePoint.position += moveVector;
            }
        }
    }

    /** ===== MARK: - Remove flower by code ===== */
    void RemoveFlower() 
    {
        Debug.Log(string.Format("Current tile: [{0}, {1}]", movePoint.position.x, movePoint.position.y));
        var tilePos = flowerTilemap.WorldToCell(movePoint.position);
        flowerTilemap.SetTile(tilePos, replacementTile);
    }

    // Run code
    public void RunCodeArray()
    {
        GetAllBlock();
        StartCoroutine(AnalyzeCode());
        //Debug.Log("Move completed");
        //CheckIfStageComplete();
        //StartCoroutine(MoveTest());
    }

    IEnumerator MoveTest()
    {
        yield return MoveUpCoroutine();
        
        yield return MoveUpCoroutine();
        
        yield return MoveRightCoroutine();
    }

    IEnumerator MoveUpCoroutine()
    {
        MoveUpSingle();
        yield return new WaitForSeconds(1f);
    }

    IEnumerator MoveDownCoroutine()
    {
        MoveDownSingle();
        yield return new WaitForSeconds(1f);
    }

    IEnumerator MoveLeftCoroutine()
    {
        MoveLeftSingle();
        yield return new WaitForSeconds(1f);
    }

    IEnumerator MoveRightCoroutine()
    {
        MoveRightSingle();
        yield return new WaitForSeconds(1f);
    }

    void GetAllBlock()
    {
        GameObject containerObject = _mainSectionObject.transform.GetChild(2).GetChild(0).GetChild(0).gameObject;
        _codeBlocks = new GameObject[containerObject.transform.childCount];

        for (int i = 0; i < containerObject.transform.childCount; i++)
        {
            if (containerObject.transform.GetChild(i).gameObject.activeSelf == true)
            {
                _codeBlocks[i] = containerObject.transform.GetChild(i).gameObject;
            }
        }
    }
    IEnumerator AnalyzeCode()
    {
        int iterator = 0;
        foreach (GameObject block in _codeBlocks)
        {
            Debug.Log("Test iterator" + iterator);
            if (block.activeSelf == true)
            {
                if (block.GetComponent<ForCodeBlockController>() != null)
                {
                    if (block.GetComponent<ForCodeBlockController>()._data.blockIdentifier == MovementBlockIdentifier.FOR)
                    {
                        Debug.Log("Vao block for roi");
                        ForCodeBlockController controller = block.GetComponent<ForCodeBlockController>();
                        for (int i = 0; i < controller.loopCount; i++)
                        {
                            for (int j = 0; j < block.transform.GetChild(0).childCount; j++)
                            {
                                GameObject childBlock = block.transform.GetChild(0).GetChild(j).gameObject;
                                if (childBlock.activeSelf == true)
                                {
                                    if (childBlock.GetComponent<MovementBlockController>()._data.blockIdentifier == MovementBlockIdentifier.UP)
                                    {
                                        //StartCoroutine(MoveUpAfter(1f));
                                        yield return MoveUpCoroutine();
                                    }
                                    else if (childBlock.GetComponent<MovementBlockController>()._data.blockIdentifier == MovementBlockIdentifier.DOWN)
                                    {
                                        //StartCoroutine(MoveUpAfter(1f));
                                        yield return MoveDownCoroutine();
                                    }
                                    else if (childBlock.GetComponent<MovementBlockController>()._data.blockIdentifier == MovementBlockIdentifier.LEFT)
                                    {
                                        //StartCoroutine(MoveUpAfter(1f));
                                        yield return MoveLeftCoroutine();
                                    }
                                    else if (childBlock.GetComponent<MovementBlockController>()._data.blockIdentifier == MovementBlockIdentifier.RIGHT)
                                    {
                                        //StartCoroutine(MoveUpAfter(1f));
                                        yield return MoveRightCoroutine();
                                    }

                                    else if (childBlock.GetComponent<MovementBlockController>()._data.blockIdentifier == MovementBlockIdentifier.WATERING)
                                    {
                                        //StartCoroutine(MoveUpAfter(1f));
                                        yield return WaterPlantCoroutine();
                                    }
                                }
                            }
                        }
                    }
                }

                else if (block.GetComponent<IfCodeBlockController>() != null)
                {
                    if (block.GetComponent<IfCodeBlockController>()._data.blockIdentifier == MovementBlockIdentifier.IF)
                    {
                        IfCodeBlockController controller = block.GetComponent<IfCodeBlockController>();
                        var currentTilePos = flowerTilemap.WorldToCell(movePoint.position);

                        int currentTileChoiceID = 0;

                        if (flowerTilemap.GetTile(currentTilePos) == conditionTile1)
                        {
                            currentTileChoiceID = 0;
                        } else if (flowerTilemap.GetTile(currentTilePos) == conditionTile2)
                        {
                            currentTileChoiceID = 1;
                        } else if (flowerTilemap.GetTile(currentTilePos) == conditionTile3)
                        {
                            currentTileChoiceID = 2;
                        }

                        // Dung DK moi vao, ko thi thoi
                        if (currentTileChoiceID == controller.targetedCondition)
                        {
                            for (int j = 0; j < block.transform.GetChild(0).childCount; j++)
                            {
                                GameObject childBlock = block.transform.GetChild(0).GetChild(j).gameObject;
                                if (childBlock.activeSelf == true)
                                {
                                    if (childBlock.GetComponent<MovementBlockController>()._data.blockIdentifier == MovementBlockIdentifier.UP)
                                    {
                                        //StartCoroutine(MoveUpAfter(1f));
                                        yield return MoveUpCoroutine();
                                    }
                                    else if (childBlock.GetComponent<MovementBlockController>()._data.blockIdentifier == MovementBlockIdentifier.DOWN)
                                    {
                                        //StartCoroutine(MoveUpAfter(1f));
                                        yield return MoveDownCoroutine();
                                    }
                                    else if (childBlock.GetComponent<MovementBlockController>()._data.blockIdentifier == MovementBlockIdentifier.LEFT)
                                    {
                                        //StartCoroutine(MoveUpAfter(1f));
                                        yield return MoveLeftCoroutine();
                                    }
                                    else if (childBlock.GetComponent<MovementBlockController>()._data.blockIdentifier == MovementBlockIdentifier.RIGHT)
                                    {
                                        //StartCoroutine(MoveUpAfter(1f));
                                        yield return MoveRightCoroutine();
                                    }

                                    else if (childBlock.GetComponent<MovementBlockController>()._data.blockIdentifier == MovementBlockIdentifier.WATERING)
                                    {
                                        //StartCoroutine(MoveUpAfter(1f));
                                        yield return WaterPlantCoroutine();
                                    }
                                }
                            }
                        }
                    }
                }


                else if (block.GetComponent<MovementBlockController>()._data.blockIdentifier == MovementBlockIdentifier.UP)
                {
                    //StartCoroutine(MoveUpAfter(1f));
                    yield return MoveUpCoroutine();
                }
                else if (block.GetComponent<MovementBlockController>()._data.blockIdentifier == MovementBlockIdentifier.DOWN)
                {
                    //StartCoroutine(MoveUpAfter(1f));
                    yield return MoveDownCoroutine();
                }
                else if (block.GetComponent<MovementBlockController>()._data.blockIdentifier == MovementBlockIdentifier.LEFT)
                {
                    //StartCoroutine(MoveUpAfter(1f));
                    yield return MoveLeftCoroutine();
                }
                else if (block.GetComponent<MovementBlockController>()._data.blockIdentifier == MovementBlockIdentifier.RIGHT)
                {
                    //StartCoroutine(MoveUpAfter(1f));
                    yield return MoveRightCoroutine();
                }

                else if (block.GetComponent<MovementBlockController>()._data.blockIdentifier == MovementBlockIdentifier.WATERING)
                {
                    //StartCoroutine(MoveUpAfter(1f));
                    yield return WaterPlantCoroutine();
                }
                
                
            }
            //_runDirectives.Add(vec);
            iterator++;
        }
    }

    void CheckIfStageComplete()
    {
        string timeSpent = "01:30";
        CustomEventSystem.instance.DispatchEvent(EventCode.ON_STAGE_FINISHED, new object[] {
            timeSpent
        });
    }

    IEnumerator WaterPlantCoroutine()
    {
        WaterPlantSingle();
        yield return new WaitForSeconds(1f);
    }

    void WaterPlantSingle()
    {
        Debug.Log(string.Format("Current tile: [{0}, {1}]", movePoint.position.x, movePoint.position.y));
        var tilePos = flowerTilemap.WorldToCell(movePoint.position);
        flowerTilemap.SetTile(tilePos, replacementTile);
    }
}
