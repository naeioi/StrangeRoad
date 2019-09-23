// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/TimeBarShader" {
	Properties{
		_Color1("Color 1", color) = (1, 1, 1, 1)
		_Color2("Color 2", color) = (1, 1, 1, 1)
		_Color3("Color 3", color) = (1, 1, 1, 1)
	    _Alpha("Alpha", range(0, 1)) = 1.0

	}
		SubShader{
		Tags{ "RenderType" = "Transparent" "Queue" = "Transparent" }
		Pass{
		Blend SrcAlpha OneMinusSrcAlpha
		CGPROGRAM

        #pragma vertex vert 
        #pragma fragment frag

		half4 _Color1;
		half4 _Color2;
		half4 _Color3;
	    half _Alpha;

	struct VertexInput {
		half4 vertex : POSITION;
		half2 texcoord : TEXCOORD;
	};

	struct VertexOutput {
		half4 pos : SV_POSITION;
		float4 wp : TEXCOORD;
	};

	VertexOutput vert(VertexInput i) {
		VertexOutput o;
		o.pos = UnityObjectToClipPos(i.vertex);
		o.wp = mul(unity_ObjectToWorld, i.vertex);
		return o;
	}

	half4 frag(VertexOutput i) : COLOR
	{
	    half4 Color;
	    if(i.wp.z > 0)
	    {
	        Color = lerp(_Color1, _Color2, clamp(1.8 - i.wp.z ,0, 1));
	    }
	    else if(i.wp.z > -3)
	    {
	         Color = lerp(_Color2, _Color3, clamp(-1.3 - i.wp.z ,0, 1));
	    }
	    else
	    {
	        Color = _Color3;
	    }
		return half4(Color.rgb, _Alpha);
	}



		ENDCG
	}


	}
		FallBack Off
}
