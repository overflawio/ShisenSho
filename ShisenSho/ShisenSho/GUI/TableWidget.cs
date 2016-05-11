using System;
using Gtk;

namespace ShisenSho
{
	public class TableWidget : Table
	{
		private int s;
		private Core c;
		private bool crossed;
		private BrickWidget selected;

		public TableWidget (Core core, int scale) : base ((uint)core.getBoardHeight () + 2, (uint)core.getBoardWidth () + 2, true) // +2 is added at both values because we need empty boxes in the outline
		{
			this.c = core;
			this.s = scale;
			crossed = false;
		}

		private void attachBrick (Widget w, int x, int y)
		{
			this.Attach (w, (uint)x, (uint)x + 1, (uint)y, (uint)y + 1);
		}
			
		public void update ()
		{
			//Destroy all childrens
			foreach (Widget child in this.Children) {
				child.Destroy ();
			}
			//add all children
			populateBoard ();
		}

		private void populateBoard ()
		{
			// Starting from one because of the outline
			for (int x = 1; x < c.getBoardWidth () + 2; x++) {
				for (int y = 1; y < c.getBoardHeight () + 2; y++) {
					int brickID;
					if (x == 0 || y == 0 || x == c.getBoardWidth () + 1 || y == c.getBoardHeight () + 1)
						brickID = 0;
					else
						brickID = this.c.getBrickID (x, y);
					if (brickID != 0)
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
			}
		}

		/*
		private void processTile (BrickWidget b)
		{
		}*/
	}
}

