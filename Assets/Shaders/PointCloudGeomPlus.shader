//fork from RealSenseSDK2.0/Shaders/PointCloudGeom

Shader "Custom/PointCloudGeomPlus"
{
    Properties
    {
		[NoScaleOffset]_MainTex ("Texture", 2D) = "white" {}
		[NoScaleOffset]_UVMap("UV", 2D) = "white" {}
		_PointSize("Point Size", Float) = 4.0
		_Color("PointCloud Color", Color) = (1, 1, 1, 1)
		[Toggle(USE_DISTANCE)]_UseDistance("Scale by distance?", float) = 0
		_WaveVelocity("Wave Velocity",Float) = 1.0
		_SPB("Second Per Beat",float) = 0.0
		_TimeFromPreviousBeat("Time From Previous Beat",Float)=0.0
	}
    SubShader
    {
		Cull Off
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
			#pragma geometry geom
            #pragma fragment frag
			#pragma shader_feature USE_DISTANCE
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
            };
			float _PointSize;
			fixed4 _Color;

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;

			sampler2D _UVMap;
			float4 _UVMap_TexelSize;

			float _WaveVelocity;
			float _SPB;
			float _TimeFromPreviousBeat;

			struct g2f
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			[maxvertexcount(4)]
			void geom(point v2f i[1], inout TriangleStream<g2f> triStream)
			{
				g2f o;
				float4 v = i[0].vertex;
				v.y = -v.y;

				// TODO: interpolate uvs on quad
				float2 uv = i[0].uv;
				float2 p = _PointSize * 0.001;
				p.y *= _ScreenParams.x / _ScreenParams.y;

				o.vertex = UnityObjectToClipPos(v);
				#ifdef USE_DISTANCE
				o.vertex += float4(-p.x, p.y, 0, 0);
				#else
				o.vertex += float4(-p.x, p.y, 0, 0) * o.vertex.w;
				#endif
				o.uv = uv;
				triStream.Append(o);

				o.vertex = UnityObjectToClipPos(v);
				#ifdef USE_DISTANCE
				o.vertex += float4(-p.x, -p.y, 0, 0);
				#else
				o.vertex += float4(-p.x, -p.y, 0, 0) * o.vertex.w;
				#endif
				o.uv = uv;
				triStream.Append(o);

				o.vertex = UnityObjectToClipPos(v);
				#ifdef USE_DISTANCE
				o.vertex += float4(p.x, p.y, 0, 0);
				#else
				o.vertex += float4(p.x, p.y, 0, 0) * o.vertex.w;
				#endif
				o.uv = uv;
				triStream.Append(o);

				o.vertex = UnityObjectToClipPos(v);
				#ifdef USE_DISTANCE
				o.vertex += float4(p.x, -p.y, 0, 0);
				#else
				o.vertex += float4(p.x, -p.y, 0, 0) * o.vertex.w;
				#endif
				o.uv = uv;
				triStream.Append(o);

			}
			// https://thebookofshaders.com/edit.php?log=160909064320
			float easeInQuad(float t) {
				return t * t;
			}

			float easeOutQuad(float t) {
				return -1.0 * t * (t - 2.0);
			}
			float easeInOutQuad(float t) {
				if ((t *= 2.0) < 1.0) {
					return 0.5 * t * t;
				}
				else {
					return -0.5 * ((t - 1.0) * (t - 3.0) - 1.0);
				}
			}
            v2f vert (appdata v)
            {
                v2f o;
				o.vertex = v.vertex;
				float4 wVertex= mul(unity_ObjectToWorld, v.vertex);


				float beatLength=_WaveVelocity * _SPB;
				float waveX = _WaveVelocity* _TimeFromPreviousBeat;
				float waveProgress = frac((abs(wVertex.x) - waveX) / beatLength);
				if (waveProgress < 0.25) {
					wVertex.y -= easeInOutQuad(waveProgress/0.25)*0.1;
				}
				else if (waveProgress < 0.5) {
					wVertex.y -= (1.0 - easeInOutQuad((waveProgress-0.25) / 0.25))*0.1;
				}
				o.vertex = mul(unity_WorldToObject, wVertex);

				o.uv = v.uv;
				return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				float2 uv = tex2D(_UVMap, i.uv);
				if (any(uv <= 0 || uv >= 1))
					discard;
				// offset to pixel center
				uv += 0.5 * _MainTex_TexelSize.xy;
				fixed4 color = tex2D(_MainTex, uv) * _Color;
				// color.r = _TimeFromPreviousBeat /_SPB;
				return color;
            }
            ENDCG
        }
    }
}
