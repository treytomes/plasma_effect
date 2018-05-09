using OpenTK;
using OpenTK.Graphics;

namespace PlasmaEffect
{
	class PlasmaEffectGameWindow : GameWindow
	{
		#region Constants

		private const int SCREEN_WIDTH = 640;
		private const int SCREEN_HEIGHT = 480;
		private const int CENTER_X = SCREEN_WIDTH / 2;
		private const int CENTER_Y = SCREEN_HEIGHT / 2;

		#endregion

		#region Fields

		private readonly PlasmaShader _plasma;

		#endregion

		#region Constructors

		public PlasmaEffectGameWindow()
			: base(SCREEN_WIDTH, SCREEN_HEIGHT, new GraphicsMode(new ColorFormat(32), 1, 0, 4, new ColorFormat(32), 2), "Plasma Effect Demo", GameWindowFlags.FixedWindow)
		{
			_plasma = new PlasmaShader(new Vector2(SCREEN_WIDTH, SCREEN_HEIGHT));
		}

		#endregion

		#region Methods

		protected override void OnUpdateFrame(FrameEventArgs e)
		{
			_plasma.Time += (float)e.Time;
		}

		protected override void OnRenderFrame(FrameEventArgs e)
		{
			_plasma.Begin();
			using (var tessellator = new VertexBufferTessellator())
			{
				tessellator.AddPoint(-CENTER_X, -CENTER_Y);
				tessellator.AddPoint(-CENTER_X, CENTER_Y);
				tessellator.AddPoint(CENTER_X, CENTER_Y);
				tessellator.AddPoint(CENTER_X, -CENTER_Y);
			}
			_plasma.End();
			SwapBuffers();
		}

		#endregion
	}
}
