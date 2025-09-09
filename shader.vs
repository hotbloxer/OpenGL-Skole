#version 330 core
layout (location = 0) in vec3 aPos; // the position variable has attribute position 0
layout (location = 1) in vec4 color;
layout (location = 2) in vec3 normals;

  
out vec4 vertexColor; // specify a color output to the fragment shader
out vec3 Normal;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{

   gl_Position = vec4(aPos, 1.0) * model * view * projection;
   Normal = normals;

   vertexColor = vec4(color);
}