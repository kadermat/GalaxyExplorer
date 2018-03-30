// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Planet/CubeMap" {
	Properties{
		_CubeTex("Cubemap", CUBE) = "" {}
	}
		SubShader{
		Tags{ "RenderType" = "Opaque" }

		Pass{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma fragmentoption ARB_precision_hint_fastest
#include "UnityCG.cginc"
		samplerCUBE _CubeTex;
	struct appdata_t {
		float4 vertex : POSITION;
		float3 normal : NORMAL;
	};
	struct v2f {
		float4 vertex : POSITION;
		float3 texcoord : TEXCOORD0;
	};
	v2f vert(appdata_t v) {
		v2f OUT;
		OUT.vertex = UnityObjectToClipPos(v.vertex);
		OUT.texcoord = v.normal;
		return OUT;
	}
	half4 frag(v2f IN) : COLOR{
		return texCUBE(_CubeTex, IN.texcoord);
	}
		ENDCG
	}
	}
		FallBack "Diffuse"
}
