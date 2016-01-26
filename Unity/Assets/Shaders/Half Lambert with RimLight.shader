Shader "Otixa/Half Lambert with RimLight" {
	Properties{
		_MainTex("Texture", 2D) = "white" {}
	_Color("Color", Color) = (1.0 , 1.0 , 1.0 , 1.0)
		_SColor("Shadow Color", Color) = (1.0 , 1.0 , 1.0 , 1.0)
		_WrapAmount("Wrap Amount", Range(0.0, 1.0)) = 0.5
		_RimColor("Rim Color", Color) = (1.0 , 1.0 , 1.0 , 1.0)
		_RimPower("Rim Power", Range(0.1, 10.0)) = 3.0
	}
		SubShader{
		Tags{
		"RenderType" = "Opaque"
	}

		CGPROGRAM
#pragma surface surf WrapLambert

		float _WrapAmount;
	uniform float4 _Color;
	uniform float4 _SColor;

	half4 LightingWrapLambert(SurfaceOutput s, half3 lightDir, half atten) {
		half NdotL = dot(s.Normal, lightDir);
		half diff = NdotL * _WrapAmount + (1 - _WrapAmount);
		half4 c;
		c.rgb = lerp(s.Albedo * _LightColor0.rgb * (diff * atten * 2),(diff * _LightColor0.rgb * atten * diff) * _SColor,1 - diff);
		c.a = s.Alpha;
		return c;
	}

	struct Input {
		float2 uv_MainTex;
		float3 viewDir;
	};
	sampler2D _MainTex;
	float4 _RimColor;
	float _RimPower;

	void surf(Input IN, inout SurfaceOutput o) {
		o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb * _Color;
		o.Alpha = tex2D(_MainTex, IN.uv_MainTex).a * _Color.a;
		half rim = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal));
		o.Emission = _RimColor.rgb * pow(rim, _RimPower);
	}
	ENDCG
	}
		Fallback "Diffuse"
}