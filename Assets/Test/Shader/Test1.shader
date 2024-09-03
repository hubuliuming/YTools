Shader "Custom/Test1"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _Ambient("Ambient",Color) = (0.3,0.3,0.3,1) //环境光
        _Specular("Specular",Color) = (1,1,1,1) //高光
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
       pass
        {
//          color(0.5,1,1,1)
            color[_Color]
            material
            {
                diffuse[_Color]
                ambient[_Ambient]
                specular[_Specular]
                shininess 80
            }
           lighting on
           separatespecular on
        }
    }
    FallBack "Diffuse"
}
