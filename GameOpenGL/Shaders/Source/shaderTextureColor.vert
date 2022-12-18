#version 330 core
layout (location = 0) in vec3 aVertexCoord;   // the position variable has attribute position 0
layout (location = 1) in vec2 aTextureCoord;
layout (location = 2) in vec4 aColor;

out vec2 textureCoord;
out vec4 color;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
    // note that we read the multiplication from right to left
    gl_Position =  vec4(aVertexCoord, 1.0) * model * view * projection;
    textureCoord = aTextureCoord;
    color = aColor;
}