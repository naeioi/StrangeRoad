// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/PikachuShader"
{
	Properties{
		_MainTex("Texture", 2D) = "" {}
	    _Value("Value", range(0, 1)) = 0.0
	}
		SubShader{
		Tags{ "RenderType" = "Transparent" "Queue" = "Transparent" }

		LOD 200

		Pass{
		Blend SrcAlpha OneMinusSrcAlpha
		CGPROGRAM

#pragma vertex vert 
#pragma fragment frag

	struct VertexInput {
		half4 vertex : POSITION;
		half2 texcoord : TEXCOORD;
	};

	struct VertexOutput {
		half4 pos : SV_POSITION;
		half2 uv : TEXCOORD;

	};

	sampler2D _MainTex;
	half _Value;
	VertexOutput vert(VertexInput i) {
		VertexOutput o;
		o.pos = UnityObjectToClipPos(i.vertex);
		o.uv = i.texcoord;
		return o;
	}

	half4 frag(VertexOutput i) : COLOR{
		half4 tex = tex2D(_MainTex, i.uv);
		tex -= half4(_Value, _Value, 0, 0);
		return tex;
	}

		ENDCG
	}
	}
		FallBack Off
}
