#pragma once
struct Point {
	float x;
	float y;
};

struct Vector2d {
	float x;
	float y;
};

struct BitMap {
	HBITMAP image;
	int width;
	int height;
};

enum Direction {
	NORTH,
	WEST
};

struct rectangle {
	int x;
	int y;
	int w;
	int h;
};

struct lightState {
	bool red;
	bool yellow;
	bool green;
};
