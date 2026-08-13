do
{
    Console.Write("ingrese un numero o ingrese control + c para terminar:");
    var numberString = Console.ReadLine();
    try
    {
        var numberInt = int.Parse(numberString!);
        if (numberInt % 2 == 0)
        {
            Console.WriteLine($"el numero: {numberInt}, es par");
        }
        else
        {
            Console.WriteLine($"el numero: {numberInt}, es impar");
        }
    }
    catch
    {
        Console.WriteLine($"el numero ingresado: {numberString}, no es valido. solo utilice caracteres numericos.");
    }
} while (true);