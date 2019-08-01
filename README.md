# plasma_effect

Plasma shader demo written in C# with OpenTK, using an OpenGL shader.

I've made this demo as simple as possible, with the exception of using a vertex buffer object to push the elements out to the graphics card.  I don't trust OpenGL immediate mode to still be available when I come back to look at this project in a year.

There are no keyboard hooks, but the window should be closeable.
