using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridController : MonoBehaviour
{

    [SerializeField]
    private Grid gridObject;

    private Tilemap flowerTilemap;


    // Start is called before the first frame update
    void Start()
    {
        flowerTilemap = gridObject.transform.GetChild(1).GetComponent<Tilemap>();

        Vector3 tilePosition;
        Vector3Int coordinate = new Vector3Int(0, 0, 0);
        for (int i = 0; i < flowerTilemap.size.x; i++) {
            for (int j = 0; j < flowerTilemap.size.y; j++) {
                coordinate.x = i; coordinate.y = j;
                tilePosition = flowerTilemap.CellToWorld(coordinate);
                Debug.Log(string.Format("Position of tile [{0}, {1}] = ({2}, {3}), empty status: {4}", coordinate.x, coordinate.y, tilePosition.x, tilePosition.y, flowerTilemap.HasTile(coordinate)));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
