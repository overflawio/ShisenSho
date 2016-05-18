using System;
using Gtk;

namespace ShisenSho
{
	public class Core
	{
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
			bool res = false;
			if (board [x1, y1] == board [x2, y2]) {
				board [x1, y1] = board [x2, y2] = NO_BRICK_TYPE;
				res = true;
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
	}
}