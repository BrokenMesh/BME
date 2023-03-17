#version 330 core
layout (location = 0) in vec3 a_Position;
layout (location = 1) in vec2 a_UV;
layout (location = 2) in vec4 a_Color;

out vec2 v_UV;                
out vec4 v_Color;          

uniform mat4 u_Projection;
uniform mat4 u_Model;

void main()
{
    gl_Position = u_Projection * u_Model * vec4(a_Position.xyz, 1.0);

    v_UV = a_UV;
    v_Color = a_Color;
}
