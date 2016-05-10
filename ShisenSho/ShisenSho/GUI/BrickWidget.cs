using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Gtk;

namespace ShisenSho
{
	public class BrickWidget : Gtk.EventBox
	{
		private string id;
		private int scale;

		public BrickWidget (int s, int brickIdentifier)
		{
			this.scale = s * 20;
			this.id = brickIdentifier.ToString ();

			Image img = renderSvg (id, scale);
			Fixed f = new Fixed ();
			f.Add (img);
			//f.ShowAll ();
			this.Add (f);
			//this.Show ();
		}

		// This function will produce an image of the desired scale from the svg file
		public Gtk.Image renderSvg (string id, int scale)
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

		public Gtk.Image checkBrick (int scale)
		{
			string basePath = System.IO.Path.GetDirectoryName (System.Reflection.Assembly.GetExecutingAssembly ().GetName ().CodeBase).Substring (5);
			Gdk.Pixbuf display = new Gdk.Pixbuf (basePath + @"/svg/cross.svg", scale, scale);
			return (new Gtk.Image (display));
		}
	}
}

