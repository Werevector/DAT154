#pragma once
#include "Structs.h"


class Car
{
public:
	Car(int x, int y);
	~Car();

	void draw(HDC drawcanvas);
	void update();

	bool loadBitMap(HINSTANCE hInst, HDC originDC, int IDB, int w, int h);


private:
	Point position;
	Vector2d velocity;
	float acceleration;
	BitMap bitMap;
	HDC carDC;

};

