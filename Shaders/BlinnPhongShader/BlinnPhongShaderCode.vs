#version 330 core
layout (location = 0) in vec3 aPos; // the position variable has attribute position 0
layout (location = 1) in vec4 aColor;
layout (location = 2) in vec3 aNormal;




uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

out vec3 fragmentPosition;
out vec3 normal;
out vec3 objectColor;


void main()
{
	normal = aNormal;
	objectColor = vec3 (aColor.xyz);

	// virker ikke men er korrekt beregnet
	//gl_Position =  projection * view  * model* vec4(aPos, 1.0);
	gl_Position =  vec4(aPos, 1.0) * model* view * projection;

    fragmentPosition = vec3(model * vec4(aPos, 1.0));
}