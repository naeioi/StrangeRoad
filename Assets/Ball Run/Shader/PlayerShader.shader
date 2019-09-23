// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/PlayerShader" {
Properties{
		_Color("Color ", color) = (1, 1, 1, 1)
		_ColorTop("Color Top ", color) = (1, 1, 1, 1)
		_MidleColorLeft ("Midle Color Left", Color) = (.5,.5,.5,1)
		_MidleColorFront ("Midle Color Front", Color) = (.5,.5,.5,1)
		_FogColor ("Fog Color", Color) = (.5,.5,.5,1)
        _Alpha("Alpha", range(0, 1)) = 1.0
        _AlphaPoint("_AlphaPoint", float) = 1.0
        _F("F", float) = 1.0
        _Range("_Range", float) = 1.0
        
	}
	SubShader{
	Tags{ "RenderType" = "Opaque"}
	Pass{
	CGPROGRAM
    #pragma vertex vert 
    #pragma fragment frag

	half4 _Color;
	half4 _ColorTop;
	half _Alpha;
	half _AlphaPoint;
	half _Range;
	half _F;
	half4 _FogColor;
	
	half4 _MidleColorLeft;
	half4 _MidleColorFront;

	struct VertexInput {
		half4 vertex : POSITION;
		half2 texcoord : TEXCOORD;
		half3 normal : NORMAL;
	};

	struct VertexOutput {
		half4 pos : SV_POSITION;
		 half3 normalDir : NORMAL;
        half lp : TEXCOORD0;
        half wpY : TEXCOORD1;
	};

	VertexOutput vert(VertexInput i) {
		VertexOutput o;
		o.pos = UnityObjectToClipPos(i.vertex);
		o.lp = i.vertex.y;
		o.wpY = mul(unity_ObjectToWorld, i.vertex).y;
		o.normalDir = normalize(mul(half4(i.normal, 0), unity_ObjectToWorld).xyz);
		return o;
	}

	half4 frag(VertexOutput i) : COLOR{
	   half4 color;
	    
	    if(i.wpY > _AlphaPoint)
		{
		     if(i.lp > _Alpha)
	         {
	            color = _ColorTop;
	         }
	         else
	         {
	            color = _Color;
	         }
		}
		else
		{
		     color = lerp(_MidleColorFront, _MidleColorLeft, clamp(abs(dot(i.normalDir, half3(1, 0, 0))), 0, 1)) ;  
			 color = lerp (_FogColor, color, clamp((i.wpY - _Range) * _F, 0, 1));
		}
		
	    return color;
		
	}
		ENDCG
	}


	}
		FallBack Off
}