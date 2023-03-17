#version 330 core
layout(location = 0) out vec4 f_color; 

in vec2 v_UV;

uniform vec4 u_tint;
uniform sampler2D u_texture;

void main()
{
    f_color = texture(u_texture, v_UV) * u_tint;
}