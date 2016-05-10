using System;
using Gtk;

namespace ShisenSho
{
	public class Core
	{
		private int height;
		private int width;
		private int brickNumber;	// Number of different type of bricks
		private int [,] board;
		private Random rnd;

		public Core (int height, int width, int brickNumber)
		{
			this.height = height;
			this.width = width;
			this.brickNumber = brickNumber;
			board = new int[this.width + 2,this.height + 2];
			this.rnd = new Random ();
			generate_board ();
		}

		// Testing function, the final implementation will be different
		private void generate_board ()
		{
			// Generating bricks for the board
			for (int i = 1; i <= this.width; i++)
				for (int j = 1; j <= this.height; j++)
					this.board [i,j] = (this.rnd.Next() % (brickNumber - 1)) + 1;
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
	}
}

