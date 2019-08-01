using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace PlasmaEffect
{
	class PlasmaEffectGameWindow : GameWindow
	{
		#region Constants

		private const int SCREEN_WIDTH = 640;
		private const int SCREEN_HEIGHT = 480;

		#endregion

		#region Fields

		private readonly PlasmaShader _plasma;

		#endregion

		#region Constructors

		public PlasmaEffectGameWindow()
			: base(SCREEN_WIDTH, SCREEN_HEIGHT, new GraphicsMode(new ColorFormat(32), 1, 0, 4, new ColorFormat(32), 2), "Plasma Effect Demo") //, GameWindowFlags.FixedWindow)
		{
			_plasma = new PlasmaShader(new Vector2(Width, Height));
		}

		#endregion

		#region Methods

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);

			_plasma.Resolution = new Vector2(Width, Height);

			GL.Viewport(ClientRectangle);
			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadIdentity();
			GL.Ortho(0, Width, Height, 0, -1, 0);
		}

		protected override void OnKeyUp(KeyboardKeyEventArgs e)
		{
			if ((e.Key == Key.F11) || ((e.Key == Key.Enter) && (e.Modifiers == KeyModifiers.Alt)))
			{
				if (WindowState != WindowState.Fullscreen)
				{
					WindowState = WindowState.Fullscreen;
				}
				else
				{
					WindowState = WindowState.Normal;
				}
			}

			base.OnKeyUp(e);
		}

		protected override void OnUpdateFrame(FrameEventArgs e)
		{
			_plasma.Time += (float)e.Time;
		}

		protected override void OnRenderFrame(FrameEventArgs e)
		{
			_plasma.Begin();
			using (var tessellator = new VertexBufferTessellator())
			{
				tessellator.AddPoint(0, 0);
				tessellator.AddPoint(0, Height);
				tessellator.AddPoint(Width, Height);
				tessellator.AddPoint(Width, 0);
			}
			_plasma.End();
			SwapBuffers();
		}

		#endregion
	}
}
