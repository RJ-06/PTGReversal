shader_type canvas_item;

uniform sampler2D screen_texture: hint_screen_texture, repeat_disable, filter_nearest;
uniform float offset_amount;
uniform bool enabled;

void vertex() {
	// Called for every vertex the material is visible on.
}

void fragment() {
	float offset_actual = enabled? offset_amount : 0.0f;
	vec2 size = SCREEN_PIXEL_SIZE;
	vec2 offsetA = SCREEN_UV + vec2(size.x * offset_actual, 0);
	vec2 offsetB = SCREEN_UV + vec2(0, size.y * offset_actual);
	vec4 color = textureLod(screen_texture, SCREEN_UV, 0.0);
	vec4 colorA = textureLod(screen_texture, offsetA, 0.0);
	vec4 colorB = textureLod(screen_texture, offsetB, 0.0);
	color.r = colorA.r;
	color.b = colorB.b;
	COLOR = color;
}

//void light() {
	// Called for every pixel for every light affecting the material.
	// Uncomment to replace the default light processing function with this one.
//}
