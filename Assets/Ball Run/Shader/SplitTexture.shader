﻿// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/SplitTexture" {
	Properties {
	_MainTex ("Texture", 2D) = "" {}
	_Alpha("Alpha", range(0, 1)) = 1.0
}
SubShader {
	Tags { "RenderType"="Transparent" "Queue"="Transparent" }

	Pass {
		Blend SrcAlpha OneMinusSrcAlpha
		
		CGPROGRAM

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
		
		half _Alpha;

		half4 frag (VertexOutput i) : COLOR {
		     half4 tex = tex2D(_MainTex, i.uv);
		     return half4(tex.rgb, tex.a * _Alpha);	
		}

		ENDCG
	}
}  
FallBack Off
}
