#version 330 core
layout (location = 0) in vec3 aPos; // the position variable has attribute position 0
layout (location = 1) in vec4 color;



uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

out vec3 fragmentPosition;


void main()
{
	mat4 view2 = model;
	mat4 view3 = view;
	mat4 view4 = projection;

	//gl_Position =  projection * view  * model* vec4(aPos, 1.0);
	gl_Position =  vec4(aPos, 1.0) * model* view * projection ;

    //fragmentPosition = vec3(vec4(aPos, 1.0) * model);
}