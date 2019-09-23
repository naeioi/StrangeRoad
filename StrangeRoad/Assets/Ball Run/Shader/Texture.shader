// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Texture" {
	Properties {
	_MainTex ("Texture", 2D) = "" {}
	_GrayScaleBonus ("Gray Scale Bonus", float) = 1
}
SubShader {
	Tags { "RenderType"="Opaque"}
	
	LOD 200

	Pass {
		CGPROGRAM
//		#pragma exclude_renderers gles
		#pragma vertex vert 
		#pragma fragment frag

		struct VertexInput {
			half4 vertex : POSITION;
			half2 texcoord : TEXCOORD;
		} ; 

		struct VertexOutput {
			half4 pos : SV_POSITION;
			half2 uv : TEXCOORD;

		} ;

		sampler2D _MainTex;
		VertexOutput vert (VertexInput i) {
		    VertexOutput o;
		    o.pos = UnityObjectToClipPos(i.vertex);
		    o.uv = i.texcoord;
		    return o;
		}
        
        half _GrayScaleFactor;
        half _GrayScaleBonus;
		half4 frag (VertexOutput i) : COLOR {
		     half4 tex = tex2D(_MainTex, i.uv);
		     return tex;	
		}

		ENDCG
	}
}  
FallBack Off
}
