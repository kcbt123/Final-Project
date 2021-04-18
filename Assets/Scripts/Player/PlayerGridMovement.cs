using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerGridMovement : MonoBehaviour
{

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
        MoveLeftSingle();
        Debug.Log("Done moving");
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
}
