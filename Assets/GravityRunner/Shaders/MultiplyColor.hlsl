#ifndef FOLIAGE_LIGHTING_INCLUDED
#define FOLIAGE_LIGHTING_INCLUDED
// Define a custom function to multiply the input color by a user-defined color


float4 BoxBlur(float2 uv, sampler2D tex, float blurSize)
{
    float2 offset1 = float2(-1, 0);
    float2 offset2 = float2(0, 0);
    float2 offset3 = float2(1, 0);

    float4 color = tex2D(tex, uv) * 0.2;
    color += tex2D(tex, uv + offset1) * 0.2;
    color += tex2D(tex, uv + offset2) * 0.2;
    color += tex2D(tex, uv + offset3) * 0.2;

    /*
    float2 texelSize = 1.0 / _ScreenSize; // Get the screen size from Unity's built-in _ScreenSize variable
    float4 color = 0;

    // Sample surrounding pixels and average them
    for (int i = -2; i <= 2; i++)
    {
        for (int j = -2; j <= 2; j++)
        {
            color += tex2D(tex, uv + float2(i, j) * blurSize * texelSize);
        }
    }

    color /= 25.0; // Adjust the divisor based on the number of samples
    */
    return color;
}

void MultiplyColor_float(float2 uv, sampler2D tex, float blurSize, out float3 result)
{
    float4 color = BoxBlur(uv, tex, blurSize);
    result = color.rgb;
}

#endif

/*
void MultiplyColor(float4 inputColor, float4 userColor,out float result)
{
    result =  inputColor * userColor;
}

*/