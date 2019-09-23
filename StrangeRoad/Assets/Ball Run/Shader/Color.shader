// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/ColorOnly"
{
	Properties{
		_Color("Color ", color) = (1, 1, 1, 1)
	}
		SubShader{
		Tags{ "RenderType" = "Opaque" }
		Pass{
		CGPROGRAM

#pragma vertex vert 
#pragma fragment frag

		half4 _Color;
	    half _Alpha;

	struct VertexInput {
		half4 vertex : POSITION;
		half2 texcoord : TEXCOORD;
	};

	struct VertexOutput {
		half4 pos : SV_POSITION;
		half2 uv : TEXCOORD;

	};

	VertexOutput vert(VertexInput i) {
		VertexOutput o;
		o.pos = UnityObjectToClipPos(i.vertex);
		o.uv = i.texcoord;
		return o;
	}

	half4 frag(VertexOutput i) : COLOR{

		return _Color;


	}



		ENDCG
	}


	}
		FallBack Off
}
