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
				Console.WriteLine ("You won, need to implement a popup");
				gameWindow.NewGameActivated (null, null);	// WIP this is bad. just testing
				gameWindow.NewGamePopup ();
			}
			else
			{
				if (c.getBrickCount () == 4)
					Console.WriteLine ("Need to check if there are other perfermable moves");
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