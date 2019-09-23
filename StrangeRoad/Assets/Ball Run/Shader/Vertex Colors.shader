// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

 Shader "Vertex Colors/Vertex Colors"
{
    Properties
    {
        _Color0 ("_Color0", Color) = (.5,.5,.5,1)
        _Color1 ("_Color1", Color) = (.5,.5,.5,1)
        _Color23 ("_Color23", Color) = (.5,.5,.5,1)
        _Color67 ("_Color67", Color) = (.5,.5,.5,1)
    }
    SubShader {
		Pass{
			
			CGPROGRAM

			#pragma vertex vert 
            #pragma fragment frag
			
			struct VertexInput {
		       half4 vertex : POSITION;
		       half4 color : COLOR;
		      
	        };

	        struct VertexOutput {
		       half4 pos : SV_POSITION;
		       half4 vertCol : COLOR;

	        };
	        
	        half _GrayScaleFactor;
	       VertexOutput vert(VertexInput v){
             VertexOutput o;
             o.pos = UnityObjectToClipPos(v.vertex);
             o.vertCol = v.color;
             return o;
         }
			
		half4 frag(VertexOutput i) : COLOR
         {
             return i.vertCol;
         }
         ENDCG
	}


	}
		FallBack Off
}