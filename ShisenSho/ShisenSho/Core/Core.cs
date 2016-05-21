using System;
using Gtk;

namespace ShisenSho
{
	public class Core
	{
		// Defining enum direction
		enum Direction {none, up, down, left, right};
		public const int NO_BRICK_TYPE = 0;
		private int height;
		private int width;
		private int brickNumber;	// Number of different type of bricks
		private int [,] board;
		private int brickCount;	// Number of bricks whit value != NO_BRICK_TYPE
		private Random rnd;

		public Core (int height, int width, int brickNumber)
		{
			this.height = height;
			this.width = width;
			this.brickNumber = brickNumber;
			newGame ();
		}

		public void newGame ()
		{
			this.brickCount = height * width;
			board = new int[this.width + 2,this.height + 2];
			this.rnd = new Random ();
			generate_board ();
		}

		// Generating bricks for the board
		private void generate_board ()
		{
			int i, j, k;
			int[] numBrick = new int[brickNumber];	// Each entry is for a different brick

			// Initializing the array
			for (i = 0; i < brickNumber; i++)
				numBrick [i] = (this.width * this.height) / brickNumber;	// Number of instances of the same brick

			// Placing bricks
			for (i = 1; i <= this.height; i++)	// Rows
				for (j = 1; j <= this.width; j++) {	// Column
					do 
					{
						k = (rnd.Next () % (brickNumber - 1));
						if (k != (brickNumber - 1) && numBrick [k] == 0 && numBrick [k + 1] != 0)
							k++;
						else if (k != 0 && numBrick [k] == 0 && numBrick [k - 1] != 0)
							k--;
					}
					while (numBrick [k] == 0);
					board [j, i] = k + 1;
					numBrick [k]--;
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
				board [x1, y1] = board [x2, y2] = NO_BRICK_TYPE;
				brickCount -= 2;
			}
			return res;
		}

		public void scramble_board ()
		{
			int temp_x = 0;
			int temp_y = 0;
			int temp_type = NO_BRICK_TYPE;

			// Scrambling board's bricks
			for (int i = 1; i <= this.width; i++)
				for (int j = 1; j <= this.height; j++)
					if (this.board [i,j] > NO_BRICK_TYPE)
					if (temp_type == NO_BRICK_TYPE) {
						temp_x = i;
						temp_y = j;
						temp_type = this.board [i,j];
					} else {
						this.board [temp_x,temp_y] = this.board [i,j];
						this.board [i,j] = temp_type;
						temp_type = NO_BRICK_TYPE;
					}
		}	

		private bool pathViability (int x1, int y1, int x2, int y2, Direction d, int turn_count)
		{
			int i;
			bool viable = false;

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
			/*
			switch (d)
			{
			case Direction.none:
				// WIP					
				break;
			case Direction.up:
				// First check if the second tile is in the same column of the first one
				if (y1 == y2) {
					// Check if the second tile is next to the first one
					if (x2 == x1 - 1)
						return true;
					else
						for (i = x1 - 1; (board [i, y1] == 0 && i > 0); i--)
							if (board [i, y1] == board [x1, y1])	// To be handled within the GUI
								return true;
				} 
				else 
				{	// Otherwise try every path recursively
					i = x1 - 1;
					if (turn_count < 2) {
						turn_count++;
						viable = pathViability (i, y1, x2, y2, Direction.left, turn_count);	// Try left
						if (viable == true)
							return viable;
						viable = pathViability (i, y1, x2, y2, Direction.left, turn_count);	// Try left
						if (viable == true)
							return viable;
					} 
					else {
						viable = pathViability (i, y1, x2, y2, Direction.up, turn_count);	// Keep going up
						if (viable == true)
							return viable;
					}
				}
				if (turn_count >= 2 && viable == false)
					return viable;
				break;
			case Direction.down:
				// First check if the second tile is in the same column of the first one
				if (y1 == y2) {
					// Check if the second tile is next to the first one
					if (x2 == x1 + 1)
						return true;
					else
						for (i = x1 + 1; (board [i, y1] == 0 || i < this.height + 1); i++)
							if (board [i, y1] == board[x1, y1])	// To be handled within the GUI
								return true;
				} 
				else {	// Otherwise try every path recursively
					i = x1 + 1;
					do {
						if (i <= this.height + 1) {	// Try down until the edge of the board
							viable = pathViability (i, y1, x2, y2, Direction.down, turn_count);
							if (viable == true)
								return viable;
						}
						else if (y1 <= this.width + 1) {	// Try right until the edge of the board
							viable = pathViability (i, y1, x2, y2, Direction.right, turn_count + 1);
							if (viable == true)
								return viable;
						}
						else if (y1 >= 0) {	// Try left until the edge of the board
							viable = pathViability (i, y1, x2, y2, Direction.left, turn_count + 1);
							if (viable == true)
								return viable;
						}
					}
					while (viable == false && turn_count < 2);
				}
				break;
			case Direction.left:
				// First check if the second tile is in the same row of the first one
				if (x1 == x2) {
					// Check if the second tile is next to the first one
					if (y2 == y1 - 1)
						return true;
					else
						for (i = y1 - 1; (board [x1, i] == 0 || i > 0); i--)
							if (board [x1, i] == board[x1, y1])	// To be handled within the GUI
								return true;
				} 
				else {	// Otherwise try every path recursively
					i = y1 - 1;
					do {
						if (i >= 0) {	// Try left until the edge of the board
							viable = pathViability (x1, i, x2, y2, Direction.left, turn_count);
							if (viable == true)
								return viable;
						}
						else if (x1 >= 0) {	// Try up until the edge of the board
							viable = pathViability (x1, i, x2, y2, Direction.up, turn_count + 1);
							if (viable == true)
								return viable;
						}
						else if (x1 <= this.height + 1) {	// Try down until the edge of the board
							viable = pathViability (i, y1, x2, y2, Direction.down, turn_count + 1);
							if (viable == true)
								return viable;
						}
					}
					while (viable == false && turn_count < 2);
				}
				break;
			case Direction.right:
				// First check if the second tile is in the same row of the first one
				if (x1 == x2) {
					// Check if the second tile is next to the first one
					if (y2 == y1 + 1)
						return true;
					else
						for (i = y1 + 1; (board [x1, i] == 0 || i < this.width + 1); i++)
							if (board [x1, i] == board[x1, y1])	// To be handled within the GUI
								return true;
				} 
				else {	// Otherwise try every path recursively
					i = y1 + 1;
					do {
						if (i <= this.width + 1) {	// Try right until the edge of the board
							viable = pathViability (x1, i, x2, y2, Direction.right, turn_count);
							if (viable == true)
								return viable;
						}
						else if (x1 >= 0) {	// Try up until the edge of the board
							viable = pathViability (x1, i, x2, y2, Direction.up, turn_count + 1);
							if (viable == true)
								return viable;
						}
						else if (x1 <= this.height + 1) {	// Try down until the edge of the board
							viable = pathViability (i, y1, x2, y2, Direction.down, turn_count + 1);
							if (viable == true)
								return viable;
						}
					}
					while (viable == false && turn_count < 2);
				}
				break;
				*/
			}
		}
	}
}