#version 330 core
in vec3 Normal;
in vec3 FragPos;

uniform vec4 objectColor;
uniform vec3 lightColor;
uniform vec3 lightPos;
uniform vec3 viewPos;

void main()
{
    float ambientStrength = 0.1;
    float specularStrength = 0.5;
    
    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(lightPos - FragPos);
    
    vec3 viewDir = normalize(viewPos - FragPos);
    vec3 reflectDir = reflect(-lightDir, norm);
    float spec = specularStrength * pow(max(dot(viewDir, reflectDir), 0.0), 32);
    
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 result = (spec + ambientStrength + diff) * lightColor * objectColor.xyz;
    gl_FragColor = vec4(result, objectColor.w);
}