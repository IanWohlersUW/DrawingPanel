using UnityEngine;
using UnityEngine.Tilemaps;

public class Color : MonoBehaviour
{
    [SerializeField]
    private Tile black, white, blue, grey, red;
    public static Tile BLACK, WHITE, BLUE, GREY, RED;

    // Set static colors to those setup in inspector
    void Awake() => (BLACK, WHITE, BLUE, GREY, RED) = (black, white, blue, grey, red);
}
