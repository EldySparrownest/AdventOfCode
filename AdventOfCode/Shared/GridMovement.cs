namespace AdventOfCode;

public struct GridMovement
{
    public bool? Horizontal;
    public bool? Vertical;

    public GridMovement(bool? horizontal, bool? vertical)
    {
        Horizontal = horizontal;
        Vertical = vertical;
    }

    public static readonly GridMovement[] Movements =
    {
        new GridMovement(null, false),  // up   
        new GridMovement(true, null),   // right 
        new GridMovement(null, true),   // down
        new GridMovement(false, null)   // left
    };

    public static readonly GridMovement Up = new GridMovement(null, false);
    public static readonly GridMovement Right = new GridMovement(true, null);
    public static readonly GridMovement Down = new GridMovement(null, true);
    public static readonly GridMovement Left = new GridMovement(false, null);
}
