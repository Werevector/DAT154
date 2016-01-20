#pragma once
#include "Structs.h"


class Car
{
public:
	Car(int x, int y, Direction dir);
	~Car();

	void draw(HDC drawcanvas);
	void update();

	bool loadBitMap(HINSTANCE hInst, int IDB, int w, int h);


private:
	Point position;
	Vector2d velocity;
	float acceleration;
	BitMap bitMap;
	Direction direction;

};

