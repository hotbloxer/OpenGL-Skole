#version 330 core
out vec4 FragColor;

in vec3 Normal;
in vec3 fragmentPosition;


uniform vec3 lightPosition;

void main()
{
    vec3 result;
    
    vec3 lightDir = normalize(lightPosition - fragmentPosition);

    vec3 normalizedNormals = normalize(Normal);
    float intensity = dot(lightDir, normalizedNormals);

    if (intensity > 0.95f) 
    {
        result = vec3(1.0,1.0,1.0);
    }
    else if (intensity > 0.50f) 
    {
        result = vec3(0.6,0.3,0.3);
    }

    else if (intensity > 0.25)
    {
        result = vec3(0.4,0.2,0.2);
    }

    else 
    {
        result = vec3(0.2,0.1,0.1);
    }


    FragColor = vec4 (result, 1.0);
}   