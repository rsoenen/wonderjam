Shader "Ocean/WhiteCapsPrecompute" 
{

	CGINCLUDE
	
	#include "UnityCG.cginc"

	uniform sampler2D _Map5, _Map6, _Map7;
	uniform float4 _Choppyness;

	struct v2f 
	{
		float4  pos : SV_POSITION;
		float2  uv : TEXCOORD0;
	};

	v2f vert(appdata_base v)
	{
		v2f OUT;
		OUT.pos = mul(UNITY_MATRIX_MVP, v.vertex);
		OUT.uv = v.texcoord;
		return OUT;
	}
	
	float4 frag0(v2f IN) : COLOR
	{ 
		float2 uv = IN.uv;
		
		// store Jacobian coeff value and variance
		float4 Jxx = _Choppyness*tex2D(_Map5, uv);
		float4 Jyy = _Choppyness*tex2D(_Map6, uv);
		float4 Jxy = _Choppyness*_Choppyness*tex2D(_Map7, uv);
	
		// Store partial jacobians
		float4 res = 0.25 + Jxx + Jyy + _Choppyness*Jxx*Jyy - Jxy*Jxy;
		float4 res2 = res*res;
	
		return float4(res.x, res2.x, res.y, res2.y);
	}
	
	float4 frag1(v2f IN) : COLOR
	{ 
		float2 uv = IN.uv;
		
		// store Jacobian coeff value and variance
		float4 Jxx = _Choppyness*tex2D(_Map5, uv);
		float4 Jyy = _Choppyness*tex2D(_Map6, uv);
		float4 Jxy = _Choppyness*_Choppyness*tex2D(_Map7, uv);
	
		// Store partial jacobians
		float4 res = 0.25 + Jxx + Jyy + _Choppyness*Jxx*Jyy - Jxy*Jxy;
		float4 res2 = res*res;
	
		return float4(res.z, res2.z, res.w, res2.w);
	}
			
	ENDCG
			
	SubShader 
	{
    	
    	Pass 
    	{
			ZTest Always Cull Off ZWrite Off
      		Fog { Mode off }
    		
			CGPROGRAM
			
			#pragma target 3.0
			#pragma vertex vert
			#pragma fragment frag0
			
			ENDCG

    	}
    	
    	Pass 
    	{
			ZTest Always Cull Off ZWrite Off
      		Fog { Mode off }
    		
			CGPROGRAM
			
			#pragma target 3.0
			#pragma vertex vert
			#pragma fragment frag1
			
			ENDCG

    	}
	}
}
