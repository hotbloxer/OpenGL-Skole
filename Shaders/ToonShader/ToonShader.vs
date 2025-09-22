#version 330 core
layout (location = 0) in vec3 aPos; 
layout (location = 1) in vec4 color;
layout (location = 2) in vec3 aNormal;


out vec3 fragmentPosition;
out vec3 normal;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;




void main()
{
   gl_Position = vec4(aPos, 1.0) * model * view * projection;
   
   
   mat3 normalMatrix = mat3 (transpose(inverse(projection + model)));
   normal = normalMatrix * aNormal;

   fragmentPosition = vec3 (model * vec4 (aPos, 1.0));

}