
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class Piece : MonoBehaviour
{
    public Board board { get; private set; }
    public TetrominoData data { get; private set; }
    public Vector3Int position { get; private set; }
    public Vector3Int[] cells { get; private set; }
    public int Rotateindex { get; private set; }
    public float stepDelay = 1f;
    public float lockDelay = 0.3f;
    private float steptime;
    private float locktime;

    public void Initialize(Board board, Vector3Int position,TetrominoData data)
    {
        this.position = position;
        this.data = data;
        this.board = board;
        this.steptime = Time.time + stepDelay;
        this.locktime = 0f;
        this.Rotateindex = 0;
        if(this.cells == null)
            this.cells = new Vector3Int[data.cells.Length];
        for(int i = 0; i < this.cells.Length; i++)
        {
            this.cells[i] = (Vector3Int)data.cells[i];
        }
    }
    public void Update()
    {
        this.board.Clear(this);
        this.locktime += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Q))
        {
            rotate(-1);
        }
        else if (Input.GetKeyDown(KeyCode.E)){
            rotate(1);
        }
        
        if (Input.GetKeyDown(KeyCode.A)){
            Move(Vector2Int.left);
        }else if (Input.GetKeyDown(KeyCode.D))
        {
            Move(Vector2Int.right);
        }
        if(Input.GetKeyDown(KeyCode.S)) {  Move(Vector2Int.down);}
        if (Input.GetKeyDown(KeyCode.Space)) { hardrop(); }
        if (Time.time >= this.steptime) {
            Step();
        }
        this.board.Set(this);
    }
    private void Step()
    {
        this.steptime = Time.time + this.stepDelay;
        Move(Vector2Int.down);
        if(this.locktime > this.lockDelay) {
            Lock();
        }
    }
    private void Lock()
    {
        this.board.Set(this);
        this.board.clearline();
        this.board.Spawnpiece();

    }
    private bool Move(Vector2Int translation)
    {
        Vector3Int newPosition = this.position;
        newPosition.x += translation.x;
        newPosition.y += translation.y;

        bool valid = this.board.isvalidPosition(this,newPosition);
        if (valid)
        {
            this.position =newPosition;
            this.locktime = 0f;
        }
        return valid;
    }
    private void rotate(int direction)
    {
        int originalRotation = this.Rotateindex;
        this.Rotateindex = Wrap(this.Rotateindex + direction, 0, 4);
        ApplyrotationMatrix(direction);
        if (!TestWllKicks(this.Rotateindex, direction))
        {
            this.Rotateindex = originalRotation;
            ApplyrotationMatrix(-direction);
        }


    }
    private void ApplyrotationMatrix(int direction)
    {
        for (int i = 0; i < this.data.cells.Length; i++)
        {
            Vector3 cell = this.cells[i];
            int x, y;
            switch (this.data.tetromino)
            {
                case Tetromino.I:
                case Tetromino.O:
                    cell.x -= 0.5f;
                    cell.y -= 0.5f;
                    x = Mathf.CeilToInt((cell.x * Data.RotationMatrix[0] * direction) + (cell.y * Data.RotationMatrix[1] * direction));
                    y = Mathf.CeilToInt((cell.x * Data.RotationMatrix[2] * direction) + (cell.y * Data.RotationMatrix[3] * direction));
                    break;
                default:
                    x = Mathf.RoundToInt((cell.x * Data.RotationMatrix[0] * direction) + (cell.y * Data.RotationMatrix[1] * direction));
                    y = Mathf.RoundToInt((cell.x * Data.RotationMatrix[2] * direction) + (cell.y * Data.RotationMatrix[3] * direction));
                    break;
            }
            this.cells[i] = new Vector3Int(x, y, 0);

        }
    }
    private int Wrap(int input,int min, int max)
    {
        if(input < min)
        {
            return max -(min - input) % (max-min);
        }
        else
        {
            return min + (input - min) % (max - min);
        }
    }
    private bool TestWllKicks(int rotationIndex,int rotationdirection)
    {
        int WallKickIndex = GetWallKicks(rotationIndex,rotationdirection);
        for(int i =0; i< this.data.wallkicks.GetLength(1);i++)
        {
            Vector2Int translation = this.data.wallkicks[WallKickIndex,i];
            if (Move(translation))
            {
                return true;
            }
        }
        return false;
    }
    private int GetWallKicks(int rotationIndex,int rotationdirection) { 
        int WallsKickIndex = rotationIndex * 2;
        if(rotationIndex <0)
        {
            WallsKickIndex--;
        }
        return Wrap(WallsKickIndex,0, this.data.wallkicks.GetLength(0));
    }
    public void hardrop()
    {
        while (Move(Vector2Int.down))
        {

            continue;
        }
    }

}
