using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;

namespace PlasmaEffect
{
	// TODO: Review http://www.opentk.com/node/425 on VBOs.
	public class VertexBufferTessellator : IDisposable
	{
		#region Constants

		private const int POINTS_PER_PRIMITIVE = 4;

		#endregion

		#region Fields

		private List<VertexBufferElement> _request;

		#endregion

		#region Constructors

		public VertexBufferTessellator()
		{
			_request = new List<VertexBufferElement>();
		}

		#endregion

		#region Methods

		public void AddPoint(float x, float y)
		{
			_request.Add(new VertexBufferElement(new Vector3(x, y, 0)));
		}

		public void Dispose()
		{
			var vertArray = _request.ToArray();
			var elemArray = new short[vertArray.Length];
			var elemIndex = 0;
			for (var n = 0; n < vertArray.Length; n += POINTS_PER_PRIMITIVE)
			{
				for (var p = 0; p < POINTS_PER_PRIMITIVE; p++)
				{
					elemArray[elemIndex++] = (short)(n + p);
				}
			}

			var vbo = new VertexBufferObject(vertArray, elemArray);
			vbo.Render(PrimitiveType.Quads);
			vbo.Dispose();
		}

		#endregion
	}
}
