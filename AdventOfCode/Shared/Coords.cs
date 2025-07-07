namespace AdventOfCode;

public struct Coords
{
    public int R;
    public int C;

    public Coords(int r, int c)
    {
        R = r;
        C = c;
    }

    public string ForPrint() => $"r: {R}, c: {C}";

    public Coords Add(Coords adding) => new Coords(R + adding.R, C + adding.C);

    ///<summary>
    ///Returns Coords of a hypothetical point B that is an equal distance from this as param A but in the opposite direction.
    ///</summary>
    public Coords MakeThisCenterOfLineWith(Coords A) => new Coords((2 * R) - A.R, (2 * C) - A.C);
    public Coords GetDistanceBetweenAsCoords(Coords otherCoords) => new Coords(R - otherCoords.R, C - otherCoords.C);

    public Coords DoMovement(GridMovement movement)
    {
        var newX = movement.Vertical switch
        {
            true => R + 1,
            false => R - 1,
            _ => R
        };

        var newY = movement.Horizontal switch
        {
            true => C + 1,
            false => C - 1,
            _ => C
        };

        return new Coords(newX, newY);
    }

    public bool IsInside(List<string> territory)
    {
        if (R < 0 || R >= territory.Count)
        {
            return false;
        }
        if (C < 0 || C >= territory[R].Length)
        {
            return false;
        }
        return true;
    }

    public bool IsOrthogonallyAdjacentTo(Coords potentialNeighbour)
    {
        return potentialNeighbour.R == R && Math.Abs(potentialNeighbour.C - C) == 1
            || potentialNeighbour.C == C && Math.Abs(potentialNeighbour.R - R) == 1;
    }
}
