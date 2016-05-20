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

		MenuBar menuBar;

		int scale;


		public GameWindow (Core c) : base (Gtk.WindowType.Toplevel)
		{
			Build ();

			this.c = c;

			// Workaround needed to not exceed window dimension on HD screen or lesser
			scale = (Screen.Height < 1000) ? 4 : 5;

			table = new TableWidget (c, scale);
			vContainer = new VBox ();
			hContainer = new HBox ();

			menuBar = createMenu ();

			this.vContainer.PackStart(menuBar, false, false, 0);
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
			this.Name = "GameWindow";
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

		private void OnScrambleActivated (object sender, EventArgs args)
		{
			c.scramble_board ();
			table.update ();
		}

		private void NewGameActivated (object sender, EventArgs args)
		{
			c.newGame ();
			table.update ();
		}

		private MenuBar createMenu ()
		{
			MenuBar m = new MenuBar ();
			MenuItem entry = new MenuItem("Table");
			Menu menu = new Menu();

			MenuItem item = new MenuItem ("New Game");
			item.Activated += NewGameActivated;
			menu.Append (item);

			entry.Submenu = menu;
			m.Append(entry);

			entry = new MenuItem ("Board");
			menu = new Menu ();

			item = new MenuItem("Scramble");
			item.Activated += OnScrambleActivated;
			menu.Append(item);

			entry.Submenu = menu;
			m.Append (entry);

			return m;
		}

		protected void OnDeleteEvent (object sender, DeleteEventArgs a)
		{
			Application.Quit ();
			a.RetVal = true;
		}
	}
}