using OpenTK;
using System.Runtime.InteropServices;

namespace PlasmaEffect
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct VertexBufferElement
	{
		public readonly Vector3 Position;

		public VertexBufferElement(Vector3 position)
		{
			Position = position;
		}
	}
}
