#version 330 core

out vec4 FragColor;


uniform vec3 lightPosition;
uniform vec4 lightColor;


void main()
{

	vec3 lig = lightPosition;
	vec4 light = lightColor;



    FragColor = vec4 (FragColor);
}   