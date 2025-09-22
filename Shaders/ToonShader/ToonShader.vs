#version 330 core
layout (location = 0) in vec3 aPos; // the position variable has attribute position 0
layout (location = 1) in vec4 color;
layout (location = 2) in vec3 normals;


out vec3 Normal;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

out vec3 fragmentPosition;


void main()
{
   gl_Position = vec4(aPos, 1.0) * model * view * projection;
   
   
   mat3 normalMatrix = mat3 (transpose(inverse(projection + model)));
   Normal = normalMatrix * normals;

   fragmentPosition = vec3 (model * vec4 (aPos, 1.0));

}