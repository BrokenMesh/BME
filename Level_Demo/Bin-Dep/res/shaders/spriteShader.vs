#version 330 core
layout (location = 0) in vec3 a_Position;
layout (location = 1) in vec2 a_UV;

out vec2 v_UV;          
uniform mat4 u_projection;
uniform mat4 u_model;

void main()
{
    gl_Position = u_projection * u_model * vec4(a_Position.xyz, 1.0);

    v_UV = a_UV;
}