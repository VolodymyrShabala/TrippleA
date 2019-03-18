using UnityEngine;

public class Grid : MonoBehaviour {
    public static int width = 10;
    public static int height = 20;
    public static Transform[,] grid = new Transform[width, height];

    public static Vector2 RoundVec2(Vector2 pos) {
        return new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y));
    }

    public static bool IsInsideBorder(Vector2 pos) {
        return pos.x >= 0 && pos.x < width && pos.y >= 0;
    }

    private static void DeleteRow(int y) {
        for(int x = 0; x < width; ++x) {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    private static void DecreaseRow(int y)  {
        for(int x = 0; x < width; ++x) {
            if(grid[x, y] != null) {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;
                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    private static void DecreaseRowsAbove(int y) {
        for(int i = y; i < height; ++i) {
            DecreaseRow(i);
        }
    }

    private static bool IsRowFull(int y) {
        for(int x = 0; x < width; ++x) {
            if(grid[x, y] == null) {
                return false;
            }
        }
        return true;
    }

    public static void DeleteFullRows() {
        for(int y = 0; y < height; ++y) {
            if(IsRowFull(y)) {
                DeleteRow(y);
                DecreaseRowsAbove(y + 1);
                --y;
            }
        }
    }
}