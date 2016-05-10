using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Gtk;

namespace ShisenSho
{
	public class BrickWidget : Gtk.EventBox
	{
		private int x;
		private int y;
		private string id;
		private int scale;

		public BrickWidget (int x, int y, int s, int brickIdentifier)
		{
			this.scale = s * 20;
			this.id = brickIdentifier.ToString ();
			this.x = x;
			this.y = y;

			Image img = renderSvg (id);
			Fixed f = new Fixed ();
			f.Add (img);
			//f.ShowAll ();
			this.Add (f);
			//this.Show ();
		}

		// This function will produce an image of the desired scale from the svg file
		public Gtk.Image renderSvg (string id)
		{
			Gdk.Pixbuf display;
			string basePath = System.IO.Path.GetDirectoryName (System.Reflection.Assembly.GetExecutingAssembly ().GetName ().CodeBase).Substring (5);
			try {
				display = new Gdk.Pixbuf (basePath + @"/svg/" + id + ".svg", this.scale, this.scale);
			} catch (GLib.GException e) {
				Console.WriteLine (e);
				display = null;
			}
			return (new Gtk.Image (display));
		}

		public Gtk.Image checkBrick ()
		{
			Gdk.Pixbuf display;
			string basePath = System.IO.Path.GetDirectoryName (System.Reflection.Assembly.GetExecutingAssembly ().GetName ().CodeBase).Substring (5);
			try {
				display = new Gdk.Pixbuf (basePath + @"/svg/cross.svg", this.scale, this.scale);
			} catch (GLib.GException e) {
				Console.WriteLine (e);
				display = null;
			}
			return (new Gtk.Image (display));
		}

		public int getX ()
		{
			return this.x;
		}

		public int getY ()
		{
			return this.y;
		}
	}
}

