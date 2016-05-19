using System;
using Gtk;

namespace ShisenSho
{
	public class Core
	{
		// Defining enum direction
		enum Direction {none, up, down, left, right};

		public const int NO_TILE_TYPE = 0;
		private int height;
		private int width;
		private int brickNumber;	// Number of different type of bricks
		private int [,] board;
		private int brickCount;	// Number of bricks whit value != NO_TILE_TYPE
		private Random rnd;

		public Core (int height, int width, int brickNumber)
		{
			this.height = height;
			this.width = width;
			this.brickNumber = brickNumber;
			this.brickCount = height * width;
			board = new int[this.width + 2,this.height + 2];
			this.rnd = new Random ();
			generate_board ();
		}

		// Generating bricks for the board
		private void generate_board ()
		{
			int i, j, k;
			int[] numTile = new int[brickNumber];	// Each entry is for a different tile

			// Initializing the array
			for (i = 0; i < brickNumber; i++)
				numTile [i] = (this.width * this.height) / brickNumber;	// Number of instances of the same tile

			// Placing tiles
			for (i = 1; i <= this.height; i++)	// Rows
				for (j = 1; j <= this.width; j++) {	// Column
					do 
					{
						k = (rnd.Next () % (brickNumber - 1));
						if (k != (brickNumber - 1) && numTile [k] == 0 && numTile [k + 1] != 0)
							k++;
						else if (k != 0 && numTile [k] == 0 && numTile [k - 1] != 0)
							k--;
					}
					while (numTile [k] == 0);
					board [j, i] = k + 1;
					numTile [k]--;
				}
		}

		public int getBoardHeight ()
		{
			return this.height;
		}

		public int getBoardWidth ()
		{
			return this.width;
		}

		public int getBrickID (int x, int y)
		{
			return this.board [x,y];
		}

		public int getBrickCount ()
		{
			return brickCount;
		}

		public bool makeMove (int x1, int y1, int x2, int y2)
		{
			bool res = pathViability (x1,y1,x2,y2,Direction.none,0); // recursive call that checks if there is a path from a brick to another one
			if (res)
			{
				board [x1, y1] = board [x2, y2] = NO_TILE_TYPE;
				brickCount -= 2;
			}
			return res;
		}

		public void scramble_board ()
		{
			int temp_x = 0;
			int temp_y = 0;
			int temp_type = NO_TILE_TYPE;

			// Scrambling board's tiles
			for (int i = 1; i <= this.width; i++)
				for (int j = 1; j <= this.height; j++)
					if (this.board [i,j] > NO_TILE_TYPE)
					if (temp_type == NO_TILE_TYPE) {
						temp_x = i;
						temp_y = j;
						temp_type = this.board [i,j];
					} else {
						this.board [temp_x,temp_y] = this.board [i,j];
						this.board [i,j] = temp_type;
						temp_type = NO_TILE_TYPE;
					}
		}

		

		private bool pathViability (int x1, int y1, int x2, int y2, Direction d, int turn_count)
		{

			if (board [x1, y1] == board [x2, y2]) {

				return true;
			}
			return false;

			if (d == Direction.none)
			{
				Console.WriteLine ("Searching the next direction to explore");
				Console.WriteLine ("Recursion whit new direction");
			}
			else
			{
				// go DodoIta!
			}
				/*
			int direction; // The board is "divided" by the brick in 4 parts labeled whit (Nord-ovest, Nord-est, Sud-est, Sud-ovest).
			switch (d)
			{
			case Direction.none:
				if (this.board [x1][y1 - 1] == 0)
					return less_than_3_turn_path (x1, y1 - 1, x2, y2, Direction.up, turn_count + 1);
				else if (turn_count < 3)
					
				break;
			case Direction.up:
				if (y1 > y2 && this.board [x1,y1 - 1] == 0)
					return pathViability (x1, y1 - 1, x2, y2, Direction.up, turn_count);
				else
				{
					if (turn_count == 3)
						return false;
					if (x1 > x2 && this.board [x1 - 1,y1] == 0)
						return pathViability (x1 - 1, y1, x2, y2, Direction.left, turn_count + 1);
					else if (x1 < x2 && this.board [x1 + 1,y1] == 0)
						return pathViability (x1 + 1, y1, x2, y2, Direction.right, turn_count + 1);
				}
				break;
			case Direction.down:
				if (y1 < y2 && this.board [x1,y1 + 1] == 0)
					return pathViability (x1, y1 + 1, x2, y2, Direction.down, turn_count);
				break;
			case Direction.left:
				if (x1 > x2 && this.board [x1 - 1,y1] == 0)
					return pathViability (x1 - 1, y1, x2, y2, Direction.left, turn_count);
				break;
			case Direction.right:
				if (x1 < x2 && this.board [x1 + 1,y1] == 0)
					return pathViability (x1 + 1, y1, x2, y2, Direction.left, turn_count);
				break;
			default:
				// [Error Check] need to implement to prevent bad arguments
				break;
			}*/
		}
	}
}
