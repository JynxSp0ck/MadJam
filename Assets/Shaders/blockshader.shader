Shader "BlockShader"
{
	Properties
	{
		_Texure("Texture", 2D) = "white" {}
	}
		SubShader
	{
		Tags{
			"Queue" = "Geometry-100"
			"RenderType" = "Opaque"
		}

			Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha
			CGPROGRAM
			#pragma target 4.0
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float3 nml : NORMAL;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
				float4 pos : TEXCOORD1;
				float3 nml : TEXCOORD2;
			};

			sampler2D _Texture;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				o.pos = float4(v.uv.x, v.vertex.y, v.uv.y, length(WorldSpaceViewDir(v.vertex)));
				o.nml = UnityObjectToWorldNormal(v.nml);
				return o;
			}

			fixed3 shading(fixed3 colour, fixed4 map, float3 pos, fixed3 normal) {
				colour.rgb = colour.rgb;
				float time = _Time.y / 5;
				//time = 3.14 / 2;
				float sunstr = saturate(cos(time) * 2 + 1.3);
				float moonstr = 0.7 * (1 - sunstr);
				float hazestr = saturate(1.5 * -cos(time + 0.2) + 0.5) * saturate(cos(time - 0.1) * 2 + 1.8);
				float3 suncol = float3(1, 1, 1);
				float3 mooncol = float3(0.7, 0.7, 1);
				float3 hazecol = float3(1, 0.9, 0.7);
				float3 sun = normalize(float3(sin(time), cos(time) * 0.5 + 0.2, cos(time)));
				float3 moon = normalize(float3(-sin(time), -cos(time) * 0.5 + 0.2, -cos(time)));
				float3 haze = float3(1, 1, 1) * (1 - hazestr) + hazestr * hazecol;

				float3 ambient = 0.2*((sunstr * suncol + moonstr * mooncol) * haze);

				normal = float3(normal.x / 4, normal.y / 4, normal.z / 4) + float3(map.r - 0.5, 0, map.g - 0.5);
				normal = normalize(normal);
				float sdiffuse = sunstr * (0.5 + 0.5 * dot(sun, normal));
				float mdiffuse = moonstr * (0.5 + 0.5 * dot(moon, normal));
				float3 diffuse = (sdiffuse * suncol + mdiffuse * mooncol) * haze;
				/*
				map.b = map.b * 2 - 1;
				float emm = saturate(map.b * -1) * 5;
				map.b = saturate(map.b);
				float3 sreflect = normalize(sun - 2 * dot(-sun, normal) * normal);
				float3 mreflect = normalize(moon - 2 * dot(-moon, normal) * normal);
				float shine = pow(map.b, 2);
				float sspectral = 10 * shine*shine*shine * pow(saturate(dot(sreflect, normalize(_WorldSpaceCameraPos - pos))), shine * 10);
				float mspectral = 5 * shine*shine*shine * pow(saturate(dot(mreflect, normalize(_WorldSpaceCameraPos - pos))), shine * 10);
				if (shine == 0) {
					sspectral = 0;
					mspectral = 0;
				}
				return fixed4(saturate(colour * (emm + ambient + diffuse) + sspectral * suncol + mspectral * mooncol), 1);
				*/
				return saturate(colour * (ambient + diffuse));
			}

			fixed4 frag(v2f i) : SV_Target
			{
				//get border coordinates
				//float3 normal = tex2D(_Normal, i.pos.xz / _Size).rgb + float3(-0.5, 0.25, -0.5);
				float4 tex = tex2D(_Texture, i.uv);
				if (tex.a < 1)
					discard;
				fixed4 col = fixed4(0, 0, 0, tex.a);
				col.rgb += shading(tex.rgb, fixed4(0.5, 0.5, 0.5, 0), i.pos.xyz, i.nml);
				return col;
			}
			ENDCG
		}
	}
}
