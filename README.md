# Advent of Code

![.NET](https://img.shields.io/badge/.NET-8.0-purple)

My solutions to [Advent of Code](https://adventofcode.com/) in C# / .NET 8.

## Project structure

```
AdventOfCode.sln
AdventOfCode/
├── Days/
│   ├── 2023
│   │   ├── Day01.cs
│   │   └── Day02.cs
│   └── 2024
│       └── Day01.cs
├── Inputs/
│   ├── 2023
│   │   ├── Day01.txt
│   │   ├── Day01Example.txt
│   │   ├── Day02.txt
│   │   └── Day02Example.txt
│   └── 2024
│       ├── Day01.txt
│       └── Day01Example.txt
├── Puzzles/
│   ├── 2023
│   │   ├── Day01Part1.txt
│   │   ├── Day01Part2.txt
│   │   ├── Day02Part1.txt
│   │   └── Day02Part2.txt
│   └── 2024
│       ├── Day01Part1.txt
│       └── Day01Part2.txt
└── Shared/
```

## Running

Open the project in Visual Studio / Rider, set the desired year and day
in `Program.cs`, then run the console app.

```csharp
// Run Year 2025, day 2 and uses the real input
`Universal.Solve<AoC.Y2025.Day02>(useExample: false);`
```

> Puzzle inputs are not included - add your own `Day01.txt`, `Day01Example.txt` etc. files to the year folders (e.g. `Inputs/2023/`).

Optionally, you can create a local `Puzzles/` folder (excluded from version control) and add puzzle description `.txt` files - the app will display them when running.
The expected structure is `Puzzles/{year}/Day01Part1.txt`, etc.

> CLI day selection is not yet implemented.
