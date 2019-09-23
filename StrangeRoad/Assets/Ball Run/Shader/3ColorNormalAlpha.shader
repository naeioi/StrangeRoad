// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/3ColorNormalAlpha" {
	Properties {
	    _TopColor ("Top Color", Color) = (.5,.5,.5,1)
		_FrontColor ("Front Color", Color) = (.5,.5,.5,1)
		_LeftColor ("Left Color", Color) = (.5,.5,.5,1)
		_FogColor ("Fog Color", Color) = (.5,.5,.5,1)
		_MidleColorLeft ("Midle Color Left", Color) = (.5,.5,.5,1)
		_MidleColorFront ("Midle Color Front", Color) = (.5,.5,.5,1)
		_AlphaPoint("_AlphaPoint", float) = 1.0
		_Range("_Range", float) = 1.0
		_F("F", float) = 1.0
		_Alpha("_Alpha", range(0,1)) = 1.0
	}
	SubShader {
		Tags{ "RenderType" = "Transparent" "Queue" = "Transparent" }
		Pass{
		Blend SrcAlpha OneMinusSrcAlpha
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
			half4 _FogColor;
			half4 _MidleColorLeft;
			half4 _MidleColorFront;
			half _AlphaPoint;
			half _Range;
			half _F;
			half _Alpha;

	        VertexOutput vert(VertexInput i) {
		       VertexOutput o;
		       o.pos = UnityObjectToClipPos(i.vertex);
		       o.wp = mul(unity_ObjectToWorld, i.vertex);

		       half3 normalDir = normalize(mul(half4(i.normal, 0), unity_ObjectToWorld).xyz);
		       if (dot(normalDir, half3(0, 1, 0)) > 0.99) 
			   	{
				   o.color = _TopColor;
		    	}
			    else
			    {
			        o.color = lerp(_FrontColor, _LeftColor, clamp(abs(dot(normalDir, half3(1, 0, 0))), 0, 1)) ;
			    }

		       return o;
	        }
			

			
			half4 frag (VertexOutput i) : COLOR
			{
				half4 color;

				if(i.wp.y > _AlphaPoint)
				{
					color = i.color;
				}
				else
				{
				    color = lerp (_FogColor, _MidleColorFront, clamp((-i.wp.y - _Range) * _F, 0, 1));
				}
				color.a = _Alpha;
				return color;			
			}
			
			ENDCG
	}


	}
		FallBack Off
}