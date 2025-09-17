#version 330 core
out vec4 FragColor;

in vec3 Normal;
in vec4 vertexColor;
in vec3 fragmentPosition;
in vec2 uv;

uniform vec3 lightColor;
uniform vec3 lightPosition;

uniform vec3 objectColor;

uniform vec3 viewPos;

uniform sampler2D texture0;




void main()
{
    float ambientStrength = 0.1;
    vec3 ambient = lightColor * ambientStrength;

    vec3 normalizedNormals = normalize(Normal);
    vec3 lightDir = normalize(lightPosition - fragmentPosition);

    float diff = max(dot(normalizedNormals, lightDir), 0.0);
    vec3 diffuse = diff * lightColor;

    vec3 diffuseStart = lightPosition * fragmentPosition;

    float specularStrength = 0.5;
    vec3 viewDir = normalize(viewPos - fragmentPosition);
    vec3 reflectDir = reflect(-lightDir, normalizedNormals);
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), 254); //The 32 is the shininess of the material.
    vec3 specular = specularStrength * spec * lightColor;

    vec3 result = (ambient + diffuse + specular) * objectColor;

    

    vec4 pixel = texture2D(texture0, uv);

    //FragColor = pixel;
    FragColor = vec4 (result, 1.0) * pixel;
    //FragColor = vec4(uv, 0.0, 1.0);

}   