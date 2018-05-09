#version 330

#define PI 3.1415926535897932384626433832795

uniform float time;
uniform vec2 resolution;

float plasma_big(float x, float y, float t)
{
	float v1 = sin(x * 10 + t);
	
	float v2 = sin(10 * (x * sin(t / 2) + y * cos(t / 3)) + t);

	float cx = x + 0.5 * sin(t / 5);
	float cy = y + 0.5 * cos(t / 3);
	float v3 = sin(sqrt(100 * (cx * cx + cy * cy) + 1) + t);

	float v_average = ((v1 + v2 + v3) / 3 + 1) / 2;

	return sin(v_average * 5 * PI);
}

float plasma_small(float x, float y, float t)
{
	float v1 = sin(x * 10 + t);
	
	float v2 = sin(10 * (x * sin(t / 2) + y * cos(t / 3)) + t);

	float cx = x + 0.5 * sin(t / 5);
	float cy = y + 0.5 * cos(t / 3);
	float v3 = sin(sqrt(100 * (cx * cx + cy * cy) + 1) + t);

	float v_average = ((v1 + v2 + v3) / 3 + 1) / 2;

	return sin(v_average * 15 * PI);
}

vec4 toRGB(float hue, float saturation, float brightness, float alpha)
{
	if (alpha < 0) alpha = 0;
	if (hue < 0) hue = 0;
	if (saturation < 0) saturation = 0;
	if (brightness < 0) brightness = 0;
	if (alpha > 1) alpha = 1;
	if (hue > 1) hue = 1;
	if (saturation > 1) saturation = 1;
	if (brightness > 1) brightness = 1;

	if (0 == saturation)
	{
		brightness = floor(brightness * 255);
		return vec4(brightness, brightness, brightness, alpha);
	}

	float fMax, fMid, fMin;
	int iSextant;

	if (0.5 < brightness)
	{
		fMax = brightness - (brightness * saturation) + saturation;
		fMin = brightness + (brightness * saturation) - saturation;
	}
	else
	{
		fMax = brightness + (brightness * saturation);
		fMin = brightness - (brightness * saturation);
	}

	iSextant = int(hue / 60);
	if (300 <= hue)
	{
		hue -= 360;
	}
	hue /= 60;
	hue -= 2 * floor(mod(iSextant + 1, 6) / 2);
	if (0 == mod(iSextant, 2))
	{
		fMid = hue * (fMax - fMin) + fMin;
	}
	else
	{
		fMid = fMin - hue * (fMax - fMin);
	}

	switch (iSextant)
	{
		case 1:
			return vec4(fMid, fMax, fMin, alpha);
		case 2:
			return vec4(fMin, fMax, fMid, alpha);
		case 3:
			return vec4(fMin, fMid, fMax, alpha);
		case 4:
			return vec4(fMid, fMin, fMax, alpha);
		case 5:
			return vec4(fMax, fMin, fMid, alpha);
		default:
			return vec4(fMax, fMid, fMin, alpha);
	}
}

void main(void)
{
	vec2 p = -1.0 + 2.0 * (gl_FragCoord.xy / resolution.xy);

	float v_big = plasma_big(p.x, p.y, time);
	float v_small = plasma_small(p.x, p.y, time / 4);

	float h = 0.25 * (sin(v_big / 2 + v_small) + 1);
	float s = 0.5 * (sin(v_small) + 1);
	float v = ((sin(v_big) + 1) / 2) * 0.125; //0.25 * (sin(v_big / 4 + v_small / 4) + 1);

	//gl_FragColor = toRGB(h, s, v, 1.0);
	gl_FragColor = vec4(h, s, v, 1.0);
}