// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/3Color2" {
	Properties {
		_FrontColor ("Front Color", Color) = (.5,.5,.5,1)
		_TopColor ("Top Color", Color) = (.5,.5,.5,1)
		_LeftColor ("Left Color", Color) = (.5,.5,.5,1)
		_Alpha("Alpha", range(0, 1)) = 1.0
		_FogColor ("Fog Color", Color) = (.5,.5,.5,1)
		_Range("_Range", float) = 1.0
		_F("F", float) = 1.0
	}
	SubShader {
		Tags{ "RenderType" = "Opaque"}
		Pass{
		LOD 200
			CGPROGRAM
			#pragma vertex vert 
            #pragma fragment frag
			
			struct VertexInput {
		       half4 vertex : POSITION;
		       half3 normal : NORMAL;
	        };

	        struct VertexOutput {
		      half4 pos : SV_POSITION;
		      half3 wp : TEXCOORD0;
		      fixed4 color : COLOR;
	        };


	        half4 _FrontColor;
			half4 _TopColor;
			half4 _LeftColor;
	        half _Alpha;
	        half4 _FogColor;
	        half _Range;
	        half _F;

	        VertexOutput vert(VertexInput i) {
		       VertexOutput o;
		       o.pos = UnityObjectToClipPos(i.vertex);
		       o.wp = mul(unity_ObjectToWorld, i.vertex);

		       half3 normalDir = normalize(mul(half4(i.normal, 0), unity_ObjectToWorld).xyz);

		       o.color = lerp(_FrontColor, _LeftColor, clamp(abs(dot(normalDir, half3(1, 0, 0))), 0, 1)) ;

		       o.color = lerp(o.color, _TopColor, clamp(abs(dot(normalDir, half3(0, 1, 0))), 0, 1)) ;

		       return o;
	        }
			

			
	half4 frag (VertexOutput i) : COLOR
	{
		if(i.wp.y > -1.6)
	    {
	        return i.color;
	    }
	    else
	    {
	       return lerp (_FogColor, i.color, clamp((i.wp.y - _Range) * _F, 0, 1));
	    }
							
	}
			
			ENDCG
	}


	}
		FallBack Off
}