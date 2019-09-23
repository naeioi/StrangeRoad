// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Screw/Color Background" {
	Properties {
		_Color1 ("Color1", Color) = (1,1,1,1)
		_Color2 ("Color2", Color) = (1,1,1,1)
		_f("F", float) = 1.0
		
	}
	SubShader {
		Tags { "Queue" = "Background"}
		LOD 200
		
		Pass {
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			
			struct VertexInput {
				half4 vertex : POSITION;
				half4 color : COLOR;
				half2 texcoord : TEXCOORD;
			};
			
			struct VertexOutput {
				half4 pos : SV_POSITION;
				half4 color : COLOR;
				float4 wp : TEXCOORD;
			};

			half4 _Color1;
			half4 _Color2;
			half f;
			
			VertexOutput vert (VertexInput i)
			{
				VertexOutput o;
				o.pos = UnityObjectToClipPos(i.vertex);
				o.color = i.color;
			    o.wp = i.vertex;//i.color
				return o;
			}
			
			half4 frag (VertexOutput i) : COLOR
			{
				return lerp(_Color1, _Color2, clamp(f * i.wp.y  ,0, 1));
			}
			ENDCG
		}
		
		
	} 
	FallBack "Diffuse"
}
