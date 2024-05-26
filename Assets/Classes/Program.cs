// See https://aka.ms/new-console-template for more information
using ConsoleApp1;

Console.WriteLine("Hello, World!");
Graphe g = new Graphe();
Console.WriteLine(g.ExisteChemin(g.Sommets[36], g.Sommets[102]));
List<Sommet> list = g.Dijkstra(g.Sommets[36], g.Sommets[102]);
foreach(Sommet s in list)
{
    Console.WriteLine(s.X + " ; " + s.Y);
}


