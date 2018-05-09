using OpenTK;
using System.IO;

namespace PlasmaEffect
{
	class PlasmaShader : ShaderProgram
	{
		#region Constants

		private const string BASE_PATH = "./gfx/shaders/plasma";
		private const string VERTEX_NAME = "vs.glsl";
		private const string FRAGMENT_NAME = "fs.glsl";
		private const string TIME_NAME = "time";
		private const string RESOLUTION_NAME = "resolution";

		#endregion

		#region Fields

		private readonly ShaderVariable _timeVar;
		private readonly ShaderVariable _resolutionVar;

		#endregion

		#region Constructors

		public PlasmaShader(Vector2 resolution)
			: base(Path.Combine(BASE_PATH, VERTEX_NAME), Path.Combine(BASE_PATH, FRAGMENT_NAME))
		{
			Time = 0f;
			Resolution = resolution;
			_timeVar = GetVariable(TIME_NAME);
			_resolutionVar = GetVariable(RESOLUTION_NAME);
		}

		#endregion

		#region Properties

		public float Time { get; set; }

		public Vector2 Resolution { get; set; }

		#endregion

		public override void Begin()
		{
			base.Begin();
			_timeVar.BindValue(Time);
			_resolutionVar.BindValue(Resolution.X, Resolution.Y);
		}
	}
}
