using System;
using Gtk;

namespace ShisenSho
{
	public class GameWindow : Gtk.Window
	{
		public GameWindow () : base (Gtk.WindowType.Toplevel)
		{
			Build ();
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

