#version 330 core

in vec3 fragmentPosition;
in vec3 normal;
in vec3 objectColor;
in vec2 uv;

out vec4 FragColor;


uniform vec3 lightPosition;
uniform vec3 lightColor;
uniform vec3 viewPosition;
uniform sampler2D texture0;
uniform sampler2D texture1;




void main()
{

	float ambientStrength = 0.1;
    vec3 ambient = lightColor * ambientStrength;

	vec3 normalizedNormals = normalize(normal);
    vec3 lightDir = normalize(lightPosition - fragmentPosition);

    float diff = max(dot(normalizedNormals, lightDir), 0.0);
    vec3 diffuse = diff * lightColor;

    float specularStrength = 0.5;
    vec4 pixelsFromSpecularMap = texture2D (texture1, uv);
    vec3 viewDirection = normalize(viewPosition - fragmentPosition);
    vec3 reflectDirection = reflect(-lightDir, normalizedNormals);
    float spec = pow(max(dot(viewDirection, reflectDirection), 0.0), 254); //The 32 is the shininess of the material.
    vec3 specular = specularStrength * spec * lightColor;

    vec3 result = (ambient + diffuse + specular * vec3 (pixelsFromSpecularMap.xyz)) * objectColor;

    vec4 pixel = texture2D(texture0, uv);
    

    FragColor = pixel;
    FragColor = vec4 (result, 1.0) * pixel;

    //FragColor = vec4 (result, 1);

}