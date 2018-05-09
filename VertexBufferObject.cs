using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace PlasmaEffect
{
	/// <remarks>
	/// To create a VBO:
	/// 1) Generate the buffer handles for the vertex and element buffers.
	/// 2) Bind the vertex buffer handle and upload your vertex data. Check that the buffer was uploaded correctly.
	/// 3) Bind the element buffer handle and upload your element data. Check that the buffer was uploaded correctly.
	/// 
	/// To draw a VBO:
	/// 1) Ensure that the VertexArray client state is enabled.
	/// 2) Bind the vertex and element buffer handles.
	/// 3) Set up the data pointers (vertex, normal, color) according to your vertex format.
	/// 4) Call DrawElements. (Note: the last parameter is an offset into the element buffer and will usually be IntPtr.Zero).
	/// </remarks>
	public class VertexBufferObject : IDisposable
	{
		#region Fields

		private int _vboID;
		private int _eboID;
		private int _numElements;

		private bool _disposed;

		private readonly VertexBufferElement[] _vertices;
		private readonly short[] _elements;

		private readonly int _stride;

		#endregion

		#region Constructors

		public VertexBufferObject(VertexBufferElement[] vertices, short[] elements)
		{
			_disposed = false;

			EnableStates();

			_vertices = vertices;
			_elements = elements;
			_stride = BlittableValueType.StrideOf(vertices);

			LoadData();
		}

		~VertexBufferObject()
		{
			Dispose(false);
		}

		#endregion

		#region Methods

		public static void EnableStates()
		{
			GL.EnableClientState(ArrayCap.VertexArray);
		}

		public void Render(PrimitiveType type)
		{
			GL.BindBuffer(BufferTarget.ArrayBuffer, _vboID);
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, _eboID);

			GL.VertexPointer(3, VertexPointerType.Float, _stride, IntPtr.Zero);

			GL.DrawElements(type, _numElements, DrawElementsType.UnsignedShort, IntPtr.Zero);
		}

		public void Dispose()
		{
			Dispose(true);
		}

		protected void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					GL.DeleteBuffer(_vboID);
					GL.DeleteBuffer(_eboID);
				}
				_disposed = true;
			}
		}

		private void LoadData()
		{
			int size;

			_vboID = GL.GenBuffer();
			GL.BindBuffer(BufferTarget.ArrayBuffer, _vboID);
			GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(_vertices.Length * _stride), _vertices, BufferUsageHint.StaticDraw);
			GL.GetBufferParameter(BufferTarget.ArrayBuffer, BufferParameterName.BufferSize, out size);
			if (_vertices.Length * _stride != size)
			{
				throw new ApplicationException("Vertex data not uploaded correctly");
			}

			_eboID = GL.GenBuffer();
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, _eboID);
			GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(_elements.Length * sizeof(short)), _elements, BufferUsageHint.StaticDraw);
			GL.GetBufferParameter(BufferTarget.ElementArrayBuffer, BufferParameterName.BufferSize, out size);
			if (_elements.Length * sizeof(short) != size)
			{
				throw new ApplicationException("Element data not uploaded correctly");
			}

			_numElements = _elements.Length;
		}

		#endregion
	}
}
