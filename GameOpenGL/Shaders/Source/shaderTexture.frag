﻿#version 330 core
//In this tutorial it might seem like a lot is going on, but really we just combine the last tutorials, 3 pieces of source code into one
//and added 3 extra point lights.
struct Material {
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;

    float shininess;
};
uniform Material material;
//This is the directional light struct, where we only need the directions
struct DirLight {
    vec3 direction;

    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
};
uniform DirLight dirLight;
//This is our pointlight where we need the position aswell as the constants defining the attenuation of the light.
struct PointLight {
    vec3 position;

    float constant;
    float linear;
    float quadratic;

    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
};
//We have a total of 4 point lights now, so we define a preprossecor directive to tell the gpu the size of our point light array
#define NR_POINT_LIGHTS 16
uniform PointLight pointLights[NR_POINT_LIGHTS];

uniform vec3 viewPos;
uniform sampler2D texture0;

uniform int pointLightsCount;

out vec4 FragColor;

in vec3 Normal;
in vec3 FragModelPos;
in vec2 TexCoords;

//Here we have some function prototypes, these are the signatures the gpu will use to know how the
//parameters of each light calculation is layed out.
//We have one function per light, since this makes it so we dont have to take up to much space in the main function.
vec3 CalcDirLight(DirLight light, vec3 normal, vec3 viewDir);
vec3 CalcPointLight(PointLight light, vec3 normal, vec3 fragPos, vec3 viewDir);

void main()
{
    //properties
    vec3 norm = normalize(Normal);
    vec3 viewDir = normalize(viewPos - FragModelPos);

    //phase 1: Directional lighting
    vec3 result = CalcDirLight(dirLight, norm, viewDir);
    //phase 2: Point lights
    for(int i = 0; i < pointLightsCount; i++)
    result += CalcPointLight(pointLights[i], norm, FragModelPos, viewDir);

    FragColor = vec4(result, 1.0);
}

vec3 CalcDirLight(DirLight light, vec3 normal, vec3 viewDir)
{
    vec3 lightDir = normalize(-light.direction);
    //diffuse shading
    float diff = max(dot(normal, lightDir), 0.0);
    //specular shading
    vec3 reflectDir = reflect(-lightDir, normal);
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
    //combine results
    vec3 textureColor = vec3(texture(texture0, TexCoords));
    vec3 ambient  = light.ambient  * textureColor * material.ambient;
    vec3 diffuse  = light.diffuse  * textureColor * material.diffuse * diff;
    vec3 specular = light.specular * textureColor * material.specular * spec;
    return (ambient + diffuse + specular);
}

vec3 CalcPointLight(PointLight light, vec3 normal, vec3 fragPos, vec3 viewDir)
{
    vec3 lightDir = normalize(light.position - fragPos);
    //diffuse shading
    float diff = max(dot(normal, lightDir), 0.0);
    //specular shading
    vec3 reflectDir = reflect(-lightDir, normal);
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
    //attenuation
    float distance    = length(light.position - fragPos);
    float attenuation = 1.0 / (light.constant + light.linear * distance +
    light.quadratic * (distance * distance));
    //combine results
    vec3 textureColor = vec3(texture(texture0, TexCoords));

    vec3 ambient  = light.ambient  * textureColor * material.ambient;
    vec3 diffuse  = light.diffuse  * textureColor * material.diffuse * diff;
    vec3 specular = light.specular * textureColor * material.specular * spec;
    
    ambient  *= attenuation;
    diffuse  *= attenuation;
    specular *= attenuation;
    return (ambient + diffuse + specular);
}