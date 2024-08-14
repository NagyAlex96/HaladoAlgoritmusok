using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using HaladoAlgoritmusok.Assets;

namespace HaladoAlgoritmusok.HillClimbing
{
    public class MazeHillClimbing
    {
        public static bool MazeSearch(int[,] maze, int2 startPos, int2 goalPos, in int obstacle = 0)
        {
            int2 current = startPos;
            while (true)
            {
                if (current == goalPos)
                {
                    //cél megtalálva
                    return true;
                }

                //közvetlen környezetben megkeressük a legjobb szomszédot
                int2 next = BestNeighbour(maze, current, goalPos, obstacle);

                if (next == current) //nincs jobb szomszéd (nem tudunk sehova lépni), akkor nincs útvonal
                {
                    return false;
                }
                current = next;
            }

            static int2 BestNeighbour(int[,] maze, int2 startPos, int2 goalPos, in int obstacle)
            {
                int2[,] neighbours =
                {
                {new int2(startPos.x, startPos.y+1) }, //fel
                {new int2(startPos.x, startPos.y-1) }, //le
                {new int2(startPos.x-1, startPos.y) }, //balra
                {new int2(startPos.x+1, startPos.y) }  //jobbra
            };

                int2 bestNeighbour = startPos; //a legjobb szomszéd kezdetben a 
                int bestDistance = ManhattanDistance(startPos, goalPos); //manhattan távolság kiszámítása

                foreach (int2 item in neighbours)
                {
                    if (IsWithinTheMaze(maze, item) && maze[item.y, item.x] == obstacle) //labirintuson belülre lépünk és szabad az adott mezőre lépni
                    {
                        int distance = ManhattanDistance(item, goalPos);

                        if (distance < bestDistance)
                        {
                            bestDistance = distance;
                            bestNeighbour = item;
                        }
                    }
                }



                return bestNeighbour;
            }


            static bool IsWithinTheMaze(int[,] maze, int2 neighbourPos)
            {
                return neighbourPos.y >= 0 && //negatívba ne menjünk túl
                        neighbourPos.y < maze.GetLength(0) && //pozitívba nem menjünk túl
                        neighbourPos.x >= 0 &&
                        neighbourPos.x < maze.GetLength(1);
            }


            static int ManhattanDistance(int2 from, int2 to)
            {
                return Math.Abs(from.x - to.x) + Math.Abs(from.y - to.y);
            }

        }
    }
}
