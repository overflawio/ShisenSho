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
		private int brickCount;	// Number of bricks with value != NO_BRICK_TYPE
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
			// Initializing the edges of the board
			for (i = 0; i <= this.height + 1; i++)
				board [0, i] = 0;
			for (j = 0; j <= this.width + 1; j++)
				board [j, 0] = 0;
			// Placing bricks
			for (i = 1; i <= this.height; i++)	// Rows
				for (j = 1; j <= this.width; j++) {	// Column
					do 
					{
						k = (rnd.Next () % brickNumber);
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
			bool res = pathViability (x1, y1, x2, y2); // Checks if there is a path

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

		private bool pathViability (int x1, int y1, int x2, int y2)
		{
			int i, j, example;

			if (board [x1, y1] != board [x2, y2])
				return false;
			
			/*** Checking all the cases ***/
			// Selected tiles on the same column
			if (x1 == x2) {
				if (y1 > y2) {	// Need to go up
					if (checkPath (x1, y1, x2, y2, 1))
						return true;
					else if (checkPath (x1, y1, x2, y2, 2))
						return true;
					else
						return false;
				}	// End if where you need to go up
				else if (y2 > y1) {	// Need to go down
					// Same as above, just swap the tiles' coordinates
					if (checkPath (x2, y2, x1, y1, 1))
						return true;
					else if (checkPath (x2, y2, x1, y1, 2))
						return true;
					else
						return false;
				}	// End if where you need to go down
			}

		}

		private bool checkPath (int x1, int y1, int x2, int y2, int example)
		{
			int i, j;

			switch (example) {
			case 1:	// Tiles on the same row/column, 0 turns
				for (j = y1 - 1; (j > y2 || board [x1, j] != 0); j--)
					;
				if (j == y2)	// Case one
					return true;
				else
					return false;
				break;
			case 2:	// Tiles on the same row/column, 2 turns
				if (board [x1 - 1, y1] != 0) {	// Try left
					i = x1 - 1;
					do {	// Then go up
						for (j = y1 - 1; (j > y2 || board [i, j] != 0); j--)
							;
						i--;
					} while (j != y2 || i >= 0 || board [i, y1] != 0);
					if (j == y2) {	// Go right (last turn)
						for (i = i + 1; (i < x2 || board [i, y1] != 0); i++)
							;
						if (i == x2)
							return true;	// End of case three (left-up-right)
					}								
				} else if (board [x1 + 1, y1] != 0) {	// Otherwise try right
					i = x1 + 1;
					do {	// Then go up
						for (j = y1 - 1; (j > y2 || board [i, j] != 0); j--)
							;
						i++;
					} while (j != y2 || i <= this.width + 1 || board [i, y1] != 0);
					if (j == y2) {	// Go left (last turn)
						for (i = i - 1; (i > x2 || board [i, j] != 0); i--)
							;
						if (i == x2)
							return true;	// End of case three (right-up-left)
					}
				} else
					return false;	// It's impossible to go left or right
				break;
				// WIP
			default:
				// To be implemented
				break;
			}
		}
	}
}