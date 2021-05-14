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
        foreach (GameObject block in _codeBlocks)
        {
            if (block.GetComponent<MovementBlockController>()._data.blockIdentifier == MovementBlockIdentifier.UP)
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

            //_runDirectives.Add(vec);
        }
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
