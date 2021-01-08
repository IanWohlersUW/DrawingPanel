using System;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

/*
 * Graphics is essentially a "brush" used in conjuction with a given DrawingPanel
 */
public class Graphics
{
    private Tile currColor; // current color our pen is set to
    private Tilemap pixels; // this is dumb... but done to reflect the CSE 142 format
    public Graphics(Tilemap pixels) => this.pixels = pixels;

    // need to flip x, y b/c tilemap is DUMB!
    private void DrawTile(int x, int y) => pixels.SetTile(
        new Vector3Int(x, y, 0), currColor
    );
    public void SetColor(Tile currColor) => this.currColor = currColor;
    public void FillRect(int anchorX, int anchorY, int width, int height)
    {
        for (int xOffset = 0; xOffset < width; xOffset++)
            for (int yOffset = 0; yOffset < height; yOffset++)
                DrawTile(anchorX + xOffset, anchorY + yOffset);
    }

    public void DrawLine(int sourceX, int sourceY, int destX, int destY)
    {
        double deltaX = destX - sourceX, deltaY = destY - sourceY;
        double m = deltaX == 0 ?
            5000 // basically vertical slope
            : deltaY / deltaX;
        double b = sourceY - m * sourceX;

        // Use our linear equation to draw all our points
        // Choose either each X or each Y given which is heigher
        var points = new List<Vector2Int>();
        Func<double, int> round = (double num) => (int)Math.Round(num);
        if (Math.Abs(deltaX) > Math.Abs(deltaY)) 
        { // draw a point for each X
            Func<double, double> linEq = (double x) => m * x + b;
            for (int x = Math.Min(sourceX, destX); x <= Math.Max(sourceX, destX); x++)
                points.Add(new Vector2Int(x, round(linEq(x))));
        } 
        else
        { // draw a point for each Y
            Func<double, double> invLinEq = (double y) => (y - b) / m;
            for (int y = Math.Min(sourceY, destY); y <= Math.Max(sourceY, destY); y++)
                points.Add(new Vector2Int(round(invLinEq(y)), y));
        }
        foreach (Vector2Int point in points)
            DrawTile(point.x, point.y);
    }
}
