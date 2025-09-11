#version 330 core
out vec4 FragColor;

in vec3 Normal;
in vec4 vertexColor;

uniform vec3 lightColor;
uniform vec3 objectColor;


void main()
{
    float ambientStrength = 0.1;
    vec3 ambient = lightColor * ambientStrength;


    vec3 result = (ambient) * objectColor;

    FragColor = vec4 (result, 1.0);
}   