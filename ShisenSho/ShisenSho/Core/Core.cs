using System;
using Gtk;

namespace ShisenSho
{
	public class Core
	{
		public Core ()
		{
		}

		public int getBoardHeight ()
		{
			return 6;
		}

		public int getBoardWidth ()
		{
			return 12;
		}

		public int getBrickID (int x, int y)
		{
			return 1;
		}
	}
}

