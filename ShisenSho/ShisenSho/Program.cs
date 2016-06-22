using System;
using Gtk;

namespace ShisenSho
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Application.Init ();

			Core core = new Core (6,12,12);

			GameWindow win = new GameWindow (core);
			win.Show ();

			Application.Run ();
		}
	}
}
