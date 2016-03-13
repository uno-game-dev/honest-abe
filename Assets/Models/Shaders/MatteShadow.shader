Shader "Custom/Matte Shadow" {

	Properties{
		_Color("Main Color", Color) = (1,1,1,1)
		_MainTex("Base (RGB) Trans (A)", 2D) = "white" {}
	_Cutoff("Alpha cutoff", Range(0,1)) = 0.5
	}

		SubShader{
		Tags{ "Queue" = "AlphaTest" "RenderType" = "TransparentCutout" }
		LOD 200
		Blend Zero SrcColor
		Offset 2, 2

		CGPROGRAM

#pragma surface surf ShadowOnly alphatest:_Cutoff fullforwardshadows

		fixed4 _Color;

	struct Input {
		float2 uv_MainTex;
	};

	inline fixed4 LightingShadowOnly(SurfaceOutput s, fixed3 lightDir, fixed atten) {
		fixed4 c;

		c.rgb = s.Albedo*atten;
		c.a = s.Alpha;
		return c;
	}

	void surf(Input IN, inout SurfaceOutput o) {
		fixed4 c = _Color;
		//fixed4 c = _LightColor0 + _LightColor0 * _Color; // Fix for Colored Lights
		o.Albedo = c.rgb;
		o.Alpha = 1.0f;
	}
	ENDCG
	}
		Fallback "Transparent/Cutout/VertexLit"
}