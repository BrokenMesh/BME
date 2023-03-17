#version 330 core
layout (location = 0) in vec3 a_Position;
layout (location = 1) in vec2 a_UV;
layout (location = 2) in float a_TexIndex;
layout (location = 3) in vec4 a_Color;

out vec2 v_UV;          
out float v_TexIndex;          
out vec4 v_Color;          

void main()
{
    gl_Position = vec4(a_Position.xyz, 1.0);

    v_UV = a_UV;
    v_TexIndex = a_TexIndex;
    v_Color = a_Color;
}
