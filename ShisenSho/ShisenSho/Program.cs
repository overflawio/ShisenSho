using System;
using Gtk;

namespace ShisenSho
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Application.Init ();

			GameWindow win = new GameWindow ();
			win.Show ();

			Application.Run ();
		}
	}
}
