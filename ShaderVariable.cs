using OpenTK.Graphics.OpenGL;

namespace PlasmaEffect
{
	public class ShaderVariable
	{
		#region Fields

		private int _id;

		#endregion

		#region Constructors

		public ShaderVariable(int id)
		{
			_id = id;
		}

		#endregion

		#region Methods

		public void BindTexture(int textureId)
		{
			GL.ActiveTexture(TextureUnit.Texture0);
			GL.BindTexture(TextureTarget.Texture2D, textureId);
			GL.Uniform1(_id, 0); // bind the shader to "Texture0"
		}

		public void BindValue(float value)
		{
			GL.Uniform1(_id, value);
		}

		public void BindValue(float x, float y)
		{
			GL.Uniform2(_id, x, y);
		}

		#endregion
	}
}
