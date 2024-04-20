using System;
using System.Numerics;

public partial class MainGameCode
{
	//		Modifiable Variables

	//		!!	CAUTION	!!
	//		Do not set the screen size values less than 5
	//			Default is 21
	
	//	Default 2001 !Keep Heigh and Width the same to avoid bugs! Also remember to use odd numbers.
	public static int worldWidth = 2001;
	//	Default 2001 !Keep Heigh and Width the same to avoid bugs! Also remember to use odd numbers.
	public static int worldHeigh = 2001;
	public static int obstaclePercent = 33;	//	Default 33
	public static int gemsPercent = 1;	//	Default 1
	public static int minePercent = 4;	//	Default 4
	public static int screenWidth = 21;	//	Default 21 - Only odd numbers.
	public static int screenHeigh = 21;	//	Default 21 - Only odd numbers.

	public static int playerSpawnRatio = 4;	//	Defaul 4
	public static int obstaclesInSpawn = 2; //	1 = 10%

	//		Composite Variables
	public static BigInteger[,] world = new BigInteger[worldWidth, worldHeigh];
	public static BigInteger expectedObstacles = ((BigInteger)obstaclePercent * world.Length) / 100;
	public static BigInteger expectedGems = ((BigInteger)gemsPercent * world.Length) / 100;
	public static BigInteger walkablePath = world.Length - expectedObstacles;
	public static BigInteger expectedMines = ((BigInteger)minePercent * walkablePath) / 100;

	//		Required Random Method
	public static Random roll = new Random();

	//		Initial Player Values
	public static int score;
	public static int pickaxe;
	public static int pickCharge;
	public static bool activePickaxe;
	public static char control;

	//		Screen Variables
	public static int worldCenter = worldWidth / 2;
	public static int localX = worldCenter - (screenWidth / 2);
	public static int localY = worldCenter - (screenHeigh / 2);
	public static int playerX = localX + (screenWidth / 2);
	public static int playerY = localY + (screenHeigh / 2);

	public static ConsoleColor systemColor = ConsoleColor.White;

	public static bool gameIsOn = true;
}