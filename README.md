# plasma_effect

Sinusoidal plasma shader demo written in C# with OpenTK, using an OpenGL shader.

I've made this demo as simple as possible, with the exception of using a vertex buffer object to push the elements out to the graphics card.  I don't trust OpenGL immediate mode to still be available when I come back to look at this project in a year.

There are no keyboard hooks, but the window should be closeable.

The window is resizeable, and you can use F11 or Ctrl+Enter to toggle fullscreen.
