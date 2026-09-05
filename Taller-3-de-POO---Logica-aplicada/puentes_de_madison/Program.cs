using System;
using System.Linq;
using shared;

var answer = string.Empty;
var options = new List<string> { "s", "n" };

do
{
    Console.Write("Ingrese el puente: ");
    string bridge = Console.ReadLine().Trim();

    Console.WriteLine(IsValid(bridge) ? "VALIDO" : "INVALIDO");

    answer = ConsoleExtension.GetValidOptions("¿Deseas continuar [S]i, [N]o?: ", options);

} while (answer.Equals("s", StringComparison.CurrentCultureIgnoreCase));

Console.WriteLine("Game Over.");

static bool IsValid(string bridge)
{
    if (string.IsNullOrEmpty(bridge) || bridge.Length < 2)
        return false;

    if (bridge[0] != '*' || bridge[bridge.Length - 1] != '*')
        return false;

    string inner = bridge.Substring(1, bridge.Length - 2);
    if (inner.Contains('*'))
        return false;

    foreach (char c in inner)
        if (c != '=' && c != '+')
            return false;

    if (inner.StartsWith("+") || inner.EndsWith("+"))
        return false;

    char[] reversed = bridge.ToCharArray();
    Array.Reverse(reversed);
    if (new string(reversed) != bridge)
        return false;

    string[] groups = inner.Length == 0
        ? Array.Empty<string>()
        : inner.Split('+');

    int groupCount = groups.Length;
    int centerIndex = (groupCount % 2 == 1) ? groupCount / 2 : -1;

    for (int i = 0; i < groupCount; i++)
    {
        if (groups[i].Length == 0 || groups[i].Any(c => c != '='))
            return false;

        int maxAllowed = (i == centerIndex) ? 3 : 2;
        if (groups[i].Length > maxAllowed)
            return false;
    }

    return true;
}