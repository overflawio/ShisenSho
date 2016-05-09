using System;
using Gtk;

namespace ShisenSho
{
	public class TableWidget : Table
	{
		private int s;
		private Core c;

		public TableWidget (Core core, int scale) : base ((uint)core.getBoardHeight () + 2, (uint)core.getBoardWidth () + 2, true) // +2 is added at both values because we need empty boxes in the outline
		{
			this.c = core;
			this.s = scale;
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
			for (int x = 1; x <= c.getBoardWidth (); x++) {
				for (int y = 1; y <= c.getBoardHeight (); y++) {
					BrickWidget brick = new BrickWidget (s, this.c.getBrickID (x, y));
					this.attachBrick (brick, x, y);
				}
			}
			this.ShowAll ();
		}

		private void onClick (object obj, ButtonPressEventArgs args)
		{
			BrickWidget brick = (BrickWidget)obj;
		}
	}
}

