using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject stagePrehub;
    [SerializeField]
    private Transform playerTrans;

    private Vector2Int currentCenter;
    private int range = 30;
    private float tileSize = 1f;
    private Dictionary<Vector2Int, GameObject> tiles = new Dictionary<Vector2Int, GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        UpdateTiles(true);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2Int playerPos = new Vector2Int(
            Mathf.FloorToInt(playerTrans.position.x / tileSize),
            Mathf.FloorToInt(playerTrans.position.y / tileSize)
        );

        if(playerPos != currentCenter)
        {
            UpdateTiles(false);
        }
    }
    
    private void UpdateTiles(bool firstTime)
    {
        Vector2Int playerTilePos = new Vector2Int(
            Mathf.FloorToInt(playerTrans.position.x / tileSize),
            Mathf.FloorToInt(playerTrans.position.y / tileSize)
        );

        currentCenter = playerTilePos;

        var existing = new HashSet<Vector2Int>(tiles.Keys);

        for (int i = -range; i <= range; i++)
        {
            for (int j = -range; j <= range; j++)
            {
                Vector2Int pos = new Vector2Int(playerTilePos.x + i, playerTilePos.y + j);
                if (!tiles.ContainsKey(pos))
                {
                    Vector3 worldPos = new Vector3(pos.x * tileSize, pos.y * tileSize, 0);
                    var tile = Instantiate(stagePrehub, worldPos, Quaternion.identity);
                    tiles[pos] = tile;
                }
                existing.Remove(pos);
            }
        }
        foreach(Vector2Int pos in existing)
        {
            Destroy(tiles[pos]);
            tiles.Remove(pos);
        }
    }
}
