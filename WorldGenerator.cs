//	Just as a minor reminder, these are the values that the cells can have and it's meaning:

//	0 = Sand: Risky cells which may contain mines, if the cell's value is 0, then it's safe.
//	1 = Obstacle: A solid unpassable wall, it is drawed as a solid square "██".
//	2 = Mine: Any cell with this value means it has a mine, it should look exactly as a sand cell ("▒▒").
//	3 = Diamon: it is represented as the emoji "💎", when taken they add +1 to score.
//	4 = Rock Floor: Similar to a translucet gray tone, it's a safe cell with no mines at all.
//	5 & 7 = Flag: Represented by the emoji "🚩", 5 means a flaged cell with no mine, and 7 has a mine.
//	6 & 8 = Inquiry: Represented by a "?", 6 means no mine, and 8 has a mine.

using System;
using System.Numerics;

public partial class MainGameCode
{
	static void WorldGenerator()
	{
		//Converts all the values in the grid to 0.

		for(int y = 0; y < world.GetLength(0) - 1; y ++)
		{
			for(int x = 0; x < world.GetLength(1) - 1; x ++)
			{
				world[x,y] = 0;
			}
		}

		//	==========	Obstaccle Generator	==========

		PorcentualGenerator(expectedObstacles, roll, 2, 1);

		//	==========	Rock Floor Generator	==========

		//	It calls the "EightSensor" Method, every empty cell after the obstacles generator, can potentially
		//	be a stone floor if it has 3 or more adyacent stone wall.

		for(int y = 1; y < world.GetLength(0) - 2; y ++)
		{
			for(int x = 1; x < world.GetLength(1) - 2; x++)
			{
				if(world[x,y] == 1)
				{
					continue;
				}
				if(EightSensor(x, y, 1) >= 3)
				{
					world[x,y] = 4;
				}
			}
		}

		//	==========	Mine Generator	==========

		PorcentualGenerator(expectedMines, roll, 4, 2);

		//	==========	Treasure Generator	==========

		PorcentualGenerator(expectedGems, roll, 2, 3);

		//	==========	Generates Spawn Area	==========

		PlayerSpawnArea();
		
	}

	//	This Method takes in the spected porcentual value of X element in the world, and iterates
	//	until it finishes, it uses random values to fill the spaces randomly and not in straigh orger, which
	//	may lead to unexpected or undesired results.

	//	It avoids to change the value of any cell with the value of 1 or the varialbe "secondCheck", which means
	//	"another value to avoid besides 1".

	//	The "painter" variable indicates what value will change into the cell once the requirements are met.

	static void PorcentualGenerator(BigInteger expected, Random roll, int secondCheck, int painter)
	{
		int x, y;
		for(BigInteger i = 0; i < expected; i ++)
		{
			do
			{
				x = roll.Next(0, world.GetLength(1));
				y = roll.Next(0, world.GetLength(0));
				if(world[x,y] != 1 && world[x,y] != secondCheck)
				{
					world[x,y] = painter;
					break;
				}
			}while(true);
		}
	}

	//	It takes the position of a cell (x and y) and checks every 8 adyacent cells to look if it has
	//	the same value marked as "criteria", it then returns in a number indicating how many adyacent cells have
	//	that value. 

	static int EightSensor(int x, int y, int criteria)
	{
		int count = 0;
		count += world[x - 1, y - 1] == criteria? 1 : 0;
		count += world[x - 0, y - 1] == criteria? 1 : 0;
		count += world[x + 1, y - 1] == criteria? 1 : 0;
		count += world[x - 1, y - 0] == criteria? 1 : 0;
		count += world[x + 1, y - 0] == criteria? 1 : 0;
		count += world[x - 1, y + 1] == criteria? 1 : 0;
		count += world[x - 0, y + 1] == criteria? 1 : 0;
		count += world[x + 1, y + 1] == criteria? 1 : 0;
		return count;
	}

	//	Defines a circle around the player based in the Pythagoras Theorem, which is safe and has some obstacles.	

	static void PlayerSpawnArea()
	{
		int limits = (playerSpawnRatio * 2) + 1;
		int initialMeasure = worldCenter - playerSpawnRatio;

		for(int y = 0; y < limits	; y ++)
		{
			for(int x = 0; x < limits; x ++)
			{
				if(IsIn(x, y, playerSpawnRatio))
				{
					if(roll.Next(1, 11) <= obstaclesInSpawn && !(x == playerSpawnRatio && y == playerSpawnRatio))
					{
						world[initialMeasure + x, initialMeasure + y] = 1;
					}else
					{
						world[initialMeasure + x , initialMeasure + y] = 4;
					}
				}		
			}
		}
	}

	//	Pythagoras Theorem logic to define if specific coordinates are inside the given circle.

	static bool IsIn(int x, int y, int ratio)
	{
		int absoluteX = Math.Abs(ratio - x);
		absoluteX *= absoluteX;
		int absoluteY = Math.Abs(ratio - y);
		absoluteY *= absoluteY;
		ratio *= ratio;

		return absoluteX + absoluteY <= ratio + (ratio * 0.07);
	}
}