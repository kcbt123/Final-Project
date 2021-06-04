using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    /** ======= MARK: - Fields and Properties ======= */

    // Character Move Point

    [SerializeField]
    private float moveSpeed = 5f;

    [SerializeField]
    private Transform movePoint;

    // External Link

    [SerializeField]
    private GameObject _mainSectionObject;

    [SerializeField]
    private LayerMask _borderLayer;

    // Input Blocks 

    private GameObject[] inputCodeBlocks;

    // Condition Tiles

    [SerializeField]
    private Tilemap flowerTilemap;
    [SerializeField]
    private Tile conditionTile1;
    [SerializeField]
    private Tile conditionTile2;
    [SerializeField]
    private Tile conditionTile3;
    [SerializeField]
    private Tile yellowFlowerTile;
    [SerializeField]
    private Tile whiteFlowerTile;
    [SerializeField]
    private Tile redFlowerTile;

    /** ======= MARK: - MonoBehaviour Methods ======= */

    private void Start()
    {
        movePoint.parent = null;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= .5f)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {

                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, _borderLayer))
                {
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                }
            }

            if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, _borderLayer))
                {
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                }
            }
        }
    }

    /** ======= MARK: - Actions ======= */

    public void RunCodeArray()
    {
        GetAllBlock();
        StartCoroutine(AnalyzeCode());
        //Debug.Log("Move completed");
        CheckIfStageComplete();
        //StartCoroutine(MoveTest());
    }

    void GetAllBlock()
    {
        GameObject containerObject = _mainSectionObject.transform.GetChild(2).GetChild(0).GetChild(0).gameObject;
        inputCodeBlocks = new GameObject[containerObject.transform.childCount];

        for (int i = 0; i < containerObject.transform.childCount; i++)
        {
            if (containerObject.transform.GetChild(i).gameObject.activeSelf == true)
            {
                inputCodeBlocks[i] = containerObject.transform.GetChild(i).gameObject;
            }
        }
    }

    IEnumerator AnalyzeCode()
    {
        foreach (GameObject block in inputCodeBlocks)
        {
            if (block != null)
            {
                if (block.GetComponent<ForCodeBlockController>() != null)
                {
                    ForCodeBlockController controller = block.GetComponent<ForCodeBlockController>();
                    if (controller.data.blockIdentifier == BlockItemIdentifier.SPECIAL_FOR)
                    {
                        for (int i = 0; i < controller.loopCount; i++)
                        {
                            for (int j = 0; j < block.transform.GetChild(0).childCount; j++)
                            {
                                GameObject childBlock = block.transform.GetChild(0).GetChild(j).gameObject;
                                if (childBlock.activeSelf == true)
                                {
                                    switch (childBlock.GetComponent<MovementBlockController>().data.blockIdentifier)
                                    {
                                        case BlockItemIdentifier.MOVEMENT_UP:
                                            yield return MoveUp();
                                            break;
                                        case BlockItemIdentifier.MOVEMENT_DOWN:
                                            yield return MoveDown();
                                            break;
                                        case BlockItemIdentifier.MOVEMENT_LEFT:
                                            yield return MoveLeft();
                                            break;
                                        case BlockItemIdentifier.MOVEMENT_RIGHT:
                                            yield return MoveRight();
                                            break;


                                        case BlockItemIdentifier.ACTION_WATERING_YELLOW:
                                            yield return WateringYellow();
                                            break;
                                        case BlockItemIdentifier.ACTION_WATERING_WHITE:
                                            yield return WateringWhite();
                                            break;
                                        case BlockItemIdentifier.ACTION_WATERING_RED:
                                            yield return WateringRed();
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }

                else if (block.GetComponent<IfCodeBlockController>() != null)
                {
                    IfCodeBlockController controller = block.GetComponent<IfCodeBlockController>();
                    if (controller.data.blockIdentifier == BlockItemIdentifier.SPECIAL_IF)
                    {
                        var currentTilePos = flowerTilemap.WorldToCell(movePoint.position);

                        // Check Current Standing Tile
                        int currentTileChoiceID = 0;

                        if (flowerTilemap.GetTile(currentTilePos) == conditionTile1)
                        {
                            currentTileChoiceID = 0;
                        }
                        else if (flowerTilemap.GetTile(currentTilePos) == conditionTile2)
                        {
                            currentTileChoiceID = 1;
                        }
                        else if (flowerTilemap.GetTile(currentTilePos) == conditionTile3)
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
                                    switch (childBlock.GetComponent<MovementBlockController>().data.blockIdentifier)
                                    {
                                        case BlockItemIdentifier.MOVEMENT_UP:
                                            yield return MoveUp();
                                            break;
                                        case BlockItemIdentifier.MOVEMENT_DOWN:
                                            yield return MoveDown();
                                            break;
                                        case BlockItemIdentifier.MOVEMENT_LEFT:
                                            yield return MoveLeft();
                                            break;
                                        case BlockItemIdentifier.MOVEMENT_RIGHT:
                                            yield return MoveRight();
                                            break;


                                        case BlockItemIdentifier.ACTION_WATERING_YELLOW:
                                            yield return WateringYellow();
                                            break;
                                        case BlockItemIdentifier.ACTION_WATERING_WHITE:
                                            yield return WateringWhite();
                                            break;
                                        case BlockItemIdentifier.ACTION_WATERING_RED:
                                            yield return WateringRed();
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }

                else if (block.GetComponent<WateringBlockController>() != null)
                {
                    switch (block.GetComponent<WateringBlockController>().data.blockIdentifier)
                    {
                        case BlockItemIdentifier.MOVEMENT_UP:
                            yield return MoveUp();
                            break;
                        case BlockItemIdentifier.MOVEMENT_DOWN:
                            yield return MoveDown();
                            break;
                        case BlockItemIdentifier.MOVEMENT_LEFT:
                            yield return MoveLeft();
                            break;
                        case BlockItemIdentifier.MOVEMENT_RIGHT:
                            yield return MoveRight();
                            break;


                        case BlockItemIdentifier.ACTION_WATERING_YELLOW:
                            yield return WateringYellow();
                            break;
                        case BlockItemIdentifier.ACTION_WATERING_WHITE:
                            yield return WateringWhite();
                            break;
                        case BlockItemIdentifier.ACTION_WATERING_RED:
                            yield return WateringRed();
                            break;
                    }
                }

                else if (block.GetComponent<MovementBlockController>() != null)
                {
                    switch (block.GetComponent<MovementBlockController>().data.blockIdentifier)
                    {
                        case BlockItemIdentifier.MOVEMENT_UP:
                            yield return MoveUp();
                            break;
                        case BlockItemIdentifier.MOVEMENT_DOWN:
                            yield return MoveDown();
                            break;
                        case BlockItemIdentifier.MOVEMENT_LEFT:
                            yield return MoveLeft();
                            break;
                        case BlockItemIdentifier.MOVEMENT_RIGHT:
                            yield return MoveRight();
                            break;


                        case BlockItemIdentifier.ACTION_WATERING_YELLOW:
                            yield return WateringYellow();
                            break;
                        case BlockItemIdentifier.ACTION_WATERING_WHITE:
                            yield return WateringWhite();
                            break;
                        case BlockItemIdentifier.ACTION_WATERING_RED:
                            yield return WateringRed();
                            break;
                    }
                }

                else
                {
                    yield return null;
                }
            }
        }
    }

    /** ======= MARK: - Stage Complete ======= */
    void CheckIfStageComplete()
    {
        string timeSpent = "01:30";
        CustomEventSystem.instance.DispatchEvent(EventCode.ON_STAGE_FINISHED, new object[] {
            timeSpent
        });
    }

    /** ======= MARK: - Move Functions ======= */

    void MoveDirectional(float x, float y)
    {
        Vector3 moveVector = new Vector3(x, y, 0f);
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, movePoint.position) <= .5f)
        {
            if (!Physics2D.OverlapCircle(movePoint.position + moveVector, .2f, _borderLayer))
            {
                movePoint.position += moveVector;
            }
        }
    }

    IEnumerator MoveUp()
    {
        MoveDirectional(0f, 1f);
        yield return new WaitForSeconds(1f);
    }
    IEnumerator MoveDown()
    {
        MoveDirectional(0f, -1f);
        yield return new WaitForSeconds(1f);
    }

    IEnumerator MoveLeft()
    {
        MoveDirectional(-1f, 0f);
        yield return new WaitForSeconds(1f);
    }

    IEnumerator MoveRight()
    {
        MoveDirectional(1f, 0f);
        yield return new WaitForSeconds(1f);
    }

    /** ======= MARK: - Watering Functions ======= */

    void WateringAction(Tile tile)
    {
        Debug.Log(string.Format("Current tile: [{0}, {1}]", movePoint.position.x, movePoint.position.y));
        var tilePos = flowerTilemap.WorldToCell(movePoint.position);
        flowerTilemap.SetTile(tilePos, tile);
    }

    IEnumerator WateringYellow()
    {
        WateringAction(yellowFlowerTile);
        yield return new WaitForSeconds(1f);
    }

    IEnumerator WateringWhite()
    {
        WateringAction(whiteFlowerTile);
        yield return new WaitForSeconds(1f);
    }

    IEnumerator WateringRed()
    {
        WateringAction(redFlowerTile);
        yield return new WaitForSeconds(1f);
    }
}
