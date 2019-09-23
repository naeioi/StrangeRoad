// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/ColorLight" {
	Properties {
	_Color("Color ", color) = (1, 1, 1, 1)
	_GrayScaleBonus ("Gray Scale Bonus", float) = 1
}
SubShader {
	Tags { "RenderType"="Opaque"}
	
	LOD 200

	Pass {
		CGPROGRAM

    #pragma vertex vert 
    #pragma fragment frag

	half4 _Color;

	struct VertexInput {
			half4 vertex : POSITION;
			half3 normal : NORMAL;
		} ; 

		struct VertexOutput {
			half4 pos : SV_POSITION;
			half4 color : COLOR;
		} ;

	VertexOutput vert (VertexInput i) {
		    VertexOutput o;
		    o.pos = UnityObjectToClipPos(i.vertex);
		    half3 normalDir = normalize(mul(half4(i.normal, 0), unity_ObjectToWorld).xyz);
		    o.color = lerp(_Color, half4(1, 1, 1, 1), clamp((dot(normalDir, half3(0, 1, 0))) * 0.7, 0, 1)) ;

		    return o;
		}

	
	half4 frag(VertexOutput i) : COLOR{
		 return i.color;
	}
		ENDCG
	}

	}
		FallBack Off
}