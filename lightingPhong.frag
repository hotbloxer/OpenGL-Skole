#version 330 core
out vec4 FragColor;

in vec3 Normal;
in vec4 vertexColor;
in vec3 fragmentPosition;

uniform vec3 lightColor;
uniform vec3 lightPosition;

uniform vec3 objectColor;

uniform vec3 viewPos;


void main()
{
    float ambientStrength = 0.1;
    vec3 ambient = lightColor * ambientStrength;

    vec3 normalizedNormals = normalize(Normal);
    vec3 lightDir = normalize(lightPosition - fragmentPosition);

    float diff = max(dot(normalizedNormals, lightDir), 0.0);
    vec3 diffuse = diff * lightColor;


    vec3 result = (ambient + diffuse) * objectColor;

    FragColor = vec4 (result, 1.0);
}   