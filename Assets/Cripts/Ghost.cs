using UnityEngine.Tilemaps;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public Tile tile;
    public Board board;
    public Piece Trackingpiece;
    public Tilemap tilemap {  get; private set; }
    public Vector3Int[] cells { get; private set; }
    public Vector3Int position { get; private set; }
    private void Awake()
    {
        this.tilemap = GetComponentInChildren<Tilemap>();
        this.cells = new Vector3Int[4];

    }
    private void LateUpdate()
    {
        Clear();
        Copy();
        
        Drop();
        Set();
    }
    private void Clear()
    {
        for (int i = 0; i < this.cells.Length; i++)
        {
            Vector3Int tilePosition = this.cells[i] + this.position;
            this.tilemap.SetTile(tilePosition, null); 
        }
    }
    private void Copy()
    {
        for (int i = 0; i < this.cells.Length; i++)
        {
            this.cells[i] = this.Trackingpiece.cells[i];
        }
    }
    private void Set()
    {
        for (int i = 0; i < this.cells.Length; i++)
        {
            Vector3Int tilePosition = this.cells[i] + this.position;
            this.tilemap.SetTile(tilePosition, this.tile);
        }
    }
    private void Drop()
    {
        Vector3Int position = this.Trackingpiece.position;

        int current = position.y;
        int bottom = -this.board.boardsize.y / 2 - 1;
        this.board.Clear(this.Trackingpiece);
        for(int row = current; row >= bottom; row--) 
        {
            position.y = row;
            if (this.board.isvalidPosition(this.Trackingpiece, position))
            {
                this.position = position;
            } else
            {
                break;
            }
        }
        this.board.Set(this.Trackingpiece);
    }
}
