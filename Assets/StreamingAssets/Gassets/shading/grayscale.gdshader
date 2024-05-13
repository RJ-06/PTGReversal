shader_type canvas_item;

void vertex() {
	// Called for every vertex the material is visible on.
}

void fragment() {
	vec4 source = texture(TEXTURE, UV);
	float average = (source.r + source.g + source.b) / 3.f;
	COLOR = vec4(average, average, average, source.a);
}

//void light() {
	// Called for every pixel for every light affecting the material.
	// Uncomment to replace the default light processing function with this one.
//}
