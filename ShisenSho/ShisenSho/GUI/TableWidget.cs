using System;
using Gtk;

namespace ShisenSho
{
	public class TableWidget : Table
	{
		private int s;
		private Core c;
		private bool brickChecked;	// True if a brick is checked
		private BrickWidget selected;
		private GameWindow gameWindow;

		public TableWidget (Core core, GameWindow gameWindow, int scale) : base ((uint)core.getBoardHeight () + 2, (uint)core.getBoardWidth () + 2, true) // +2 is added at both values because we need empty boxes in the outline
		{
			this.c = core;
			this.s = scale;
			this.gameWindow = gameWindow;
			brickChecked = false;
		}

		private void attachBrick (Widget w, int x, int y)
		{
			this.Attach (w, (uint)x, (uint)x + 1, (uint)y, (uint)y + 1);
		}
			
		public void update ()
		{
			brickChecked = false;
			selected = null;

			//Destroy all childrens
			foreach (Widget child in this.Children) {
				child.Destroy ();
			}
			//add all children
			populateBoard ();
			if (c.getBrickCount () == 0) {
				// Simulation a click on NewGame
				gameWindow.NewGameActivated (null, null);
				gameWindow.NewGamePopup ();
			}
			else
			{
				if (c.getBrickCount () == 4) {
					/*
						In this implementation of ShisenSho (6 x 12 tiles with 12 tile types), if the tiles amout to four, they are positioned one
						next to the other forming a square and with alternated types, we are in GameOver use case.

						Here is an example of "no more move possible" tile disposition 12
																					   21
					*/
					Console.WriteLine ("Checking if game is over");

					for (int x = 1; x < c.getBoardWidth () + 2; x++) {
						for (int y = 1; y < c.getBoardHeight () + 2; y++) {
							if (this.c.getBrickID (x, y) != Core.NO_BRICK_TYPE &&
								this.c.getBrickID (x, y) == this.c.getBrickID (x + 1, y + 1) &&
								this.c.getBrickID (x + 1, y) != Core.NO_BRICK_TYPE &&
								this.c.getBrickID (x + 1, y) == this.c.getBrickID (x, y + 1))
							{
								x = c.getBoardWidth () + 2;
								y = c.getBoardHeight () + 2;
								gameWindow.NewGameActivated (null, null);
								gameWindow.GameOverPopup ();
							}
						}
					}
				}
				else
					Console.WriteLine (c.getBrickCount ());
			}
		}

		private void populateBoard ()
		{
			// Starting from one because of the outline
			for (int x = 1; x < c.getBoardWidth () + 2; x++) {
				for (int y = 1; y < c.getBoardHeight () + 2; y++) {
					if (this.c.getBrickID (x, y) != Core.NO_BRICK_TYPE)
					{
						BrickWidget b = new BrickWidget (x, y, s, this.c.getBrickID (x, y));

						b.ButtonPressEvent += onClick;
						this.attachBrick (b, x, y);
					}
				}
			}
			this.ShowAll ();
		}

		private void onClick (object obj, ButtonPressEventArgs args)
		{
			BrickWidget brick = (BrickWidget)obj;

			if( ((Gdk.EventButton)args.Event).Type == Gdk.EventType.ButtonPress)
			{
				if (!brickChecked) {
					brickChecked = true;
					selected = brick;
					Fixed f = (Fixed)(brick.Child);
					Image check = brick.checkBrick ();
					f.Add (check);
					f.ShowAll ();
				} else {
					Fixed f = (Fixed)(selected.Child);
					if (f.Children.Length > 1)
						f.Remove (f.Children [1]);
					
					if (selected == brick) {
						brickChecked = false;
					} else {
						if (!c.makeMove (selected.getX (), selected.getY (),
							    brick.getX (), brick.getY ())) {
							selected = brick;
							f = (Fixed)(brick.Child);
							Image check = brick.checkBrick ();
							f.Add (check);
							f.ShowAll ();
						} else {
							selected = null;
							brickChecked = false;
							update ();
						}
					}
				}
			}
		}
	}
}