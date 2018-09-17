Shader "Custom/DepthMask" {
	SubShader{
		Tags { "Queue" = "Geometry+90" }
		
		ColorMask 0
		ZWrite On

		Pass {}

	}
}
