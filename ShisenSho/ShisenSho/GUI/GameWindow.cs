using System;
using Gtk;

namespace ShisenSho
{
	public class GameWindow : Gtk.Window
	{
		Core c;
		private TableWidget table;
		VBox vContainer;			// Vertical container box
		HBox hContainer;			// Horizontal container box

		public GameWindow () : base (Gtk.WindowType.Toplevel)
		{
			Build ();

			c = new Core (6,12,12);

			// Workaround needed to not exceed window dimension on HD screen or lesser
			int scale = (Screen.Height < 1000) ? 4 : 8;

			this.table = new TableWidget (c, scale);
			this.vContainer = new VBox ();
			this.hContainer = new HBox ();

			this.vContainer.PackStart (this.table, false, false, 0);

			hContainer.PackStart (new HBox ());
			hContainer.PackStart (vContainer, false, false, 0);
			hContainer.PackStart (new HBox ());
			hContainer.ShowAll ();

			this.Add (hContainer);

			table.update ();

			this.Show ();
		}

		// Imported from the designer generated file (necessary if you do not want to use the default monodevelop designer)
		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget MainWindow
			this.Name = "MainWindow";
			this.Title = global::Mono.Unix.Catalog.GetString ("ShisenSho");

			// 3 is the value needed to show the window at the center of the screen
			this.WindowPosition = ((global::Gtk.WindowPosition)(3));
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.DefaultWidth = 400;
			this.DefaultHeight = 300;
			this.Show ();
			this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
		}

		protected void OnDeleteEvent (object sender, DeleteEventArgs a)
		{
			Application.Quit ();
			a.RetVal = true;
		}
	}
}

