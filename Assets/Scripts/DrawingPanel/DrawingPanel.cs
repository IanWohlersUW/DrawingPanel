using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DrawingPanel : MonoBehaviour
{
    private static DrawingPanel instance;

    [SerializeField]
    private Tilemap pixels;
    private Tile background;
    private int width = 200, height = 200; // by default our panel has a weird scale

    void Awake() => instance = this;
    public static DrawingPanel GetPanel(int width, int height)
    {
        instance.background = Color.WHITE;
        instance.SetSize(width, height);
        return instance;
    }

    private void SetSize(int width, int height)
    {
        (this.width, this.height) = (width, height);
        Clear();

        // Camera crop
        var anchorPos = pixels.CellToWorld(new Vector3Int(height / 2, width / 2, 0));
        Camera.main.transform.position = new Vector3(anchorPos.x, anchorPos.y, -10);
        Camera.main.orthographicSize = (height / 2) * 1.3f; // add some leeway
    }

    public void SetBackground(Tile backgroundColor)
    {
        this.background = backgroundColor;
        Clear();
    }

    public void Clear()
    { // sets each Tile to our background color
        pixels.ClearAllTiles();
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                pixels.SetTile(new Vector3Int(y, x, 0), background);
    }
    public Graphics GetGraphics() => new Graphics(pixels);
}
