#version 330 core
layout(location = 0) out vec4 f_color; 

in vec2 v_UV;      
in vec4 v_Color;   

uniform sampler2D u_Atlas;

uniform float u_Width;
uniform float u_Edge;

uniform float u_BorderWidth;
uniform float u_BorderEdge;
uniform vec3 u_OutlineColor;
uniform vec2 u_Offset;

void main()
{
    float distance = 1.0 - texture(u_Atlas, v_UV).a;
    float alpha = 1.0 - smoothstep(u_Width, u_Width + u_Edge, distance);

    float distance2 = 1.0 - texture(u_Atlas, v_UV + u_Offset).a;
    float outlineAlpha = 1.0 - smoothstep(u_BorderWidth, u_BorderWidth + u_BorderEdge, distance2);

    float overallAlpha = alpha + (1.0 - alpha) * outlineAlpha;
    vec3 overallColor = mix(u_OutlineColor, v_Color.xyz, alpha / overallAlpha);

    f_color = vec4(overallColor, overallAlpha);
    //f_color = v_Color;
}
