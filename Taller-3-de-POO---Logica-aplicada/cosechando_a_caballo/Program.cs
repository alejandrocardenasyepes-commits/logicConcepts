using shared;
using System;
using System.Collections.Generic;

var answer = string.Empty;
var options = new List<string> { "s", "n" };

do
{
    char[,] field = new char[8, 8];
    for (int i = 0; i < 8; i++)
        for (int j = 0; j < 8; j++)
            field[i, j] = ' ';

    Console.Write("Ingrese ubicación de los frutos: ");
    string fruitsInput = Console.ReadLine().Trim();

    Console.Write("Ingrese posición inicial del caballo: ");
    string startPositionInput = Console.ReadLine().Trim();

    Console.Write("Ingrese los movimientos del caballo: ");
    string movesInput = Console.ReadLine().Trim();

    // sembrar cultivo en el campo
    string[] fruits = fruitsInput.Split(',');
    foreach (string fruit in fruits)
    {
        string positionText = fruit.Substring(0, fruit.Length - 1);
        char symbol = fruit[fruit.Length - 1];
        (int col, int row) position = ParsePosition(positionText);
        field[position.col, position.row] = symbol;
    }

    (int col, int row) knight = ParsePosition(startPositionInput);

    // deltas del movimiento del caballo
    Dictionary<string, (int dCol, int dRow)> moveDeltas = new Dictionary<string, (int, int)>
    {
        { "UL", (-1, 2) },
        { "UR", (1, 2) },
        { "LU", (-2, 1) },
        { "LD", (-2, -1) },
        { "RU", (2, 1) },
        { "RD", (2, -1) },
        { "DL", (-1, -2) },
        { "DR", (1, -2) }
    };

    List<char> harvestedFruits = new List<char>();
    string[] moveCodes = movesInput.Split(',');

    foreach (string moveCode in moveCodes)
    {
        string code = moveCode.Trim();
        var (dCol, dRow) = moveDeltas[code];
        int newCol = knight.col + dCol;
        int newRow = knight.row + dRow;

        if (newCol < 0 || newCol > 7 || newRow < 0 || newRow > 7)
            continue; // di el movimiento dale del tablero saltalo

        knight = (newCol, newRow);

        char fruit = field[knight.col, knight.row];
        if (fruit != ' ')
        {
            harvestedFruits.Add(fruit);
            field[knight.col, knight.row] = ' '; // ya cosechado
        }
    }

    Console.WriteLine("Los frutos recogidos son: " + string.Join(" ", harvestedFruits));

    answer = ConsoleExtension.GetValidOptions("¿Deseas continuar [S]i, [N]o?: ", options);

} while (answer.Equals("s", StringComparison.CurrentCultureIgnoreCase));

Console.WriteLine("Game Over.");

static (int col, int row) ParsePosition(string text)
{
    char columnLetter = char.ToUpper(text[0]);
    int rowNumber = int.Parse(text.Substring(1));
    return (columnLetter - 'A', rowNumber - 1);
}