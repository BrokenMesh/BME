#version 330 core
layout(location = 0) out vec4 f_color; 

in vec2 v_UV;
in float v_TexIndex;          
in vec4 v_Color;   

uniform sampler2D u_Textures[32];

void main()
{
    int i = int(v_TexIndex);
    f_color = texture(u_Textures[i], v_UV) * v_Color;
    //f_color = v_Color;
}
