#include "stdafx.h"
#include "Car.h"


Car::Car(int x, int y)
{
	position.x = x;
	position.y = y;

	velocity.x = 0;
	velocity.y = 0;

	acceleration = 2;

}


Car::~Car()
{
	DeleteObject(carDC);
	DeleteObject(bitMap.image);
}

void Car::draw(HDC drawcanvas)
{
	int nx = position.x + (bitMap.width / 2);
	int ny = position.y + (bitMap.height / 2);

	SelectObject(carDC, bitMap.image);
	BitBlt(drawcanvas, nx, ny, bitMap.width, bitMap.height, carDC, 0, 0, SRCCOPY);

}

bool Car::loadBitMap(HINSTANCE hInst, HDC originDC, int IDB, int w, int h)
{
	bool result = true;
	bitMap.image = LoadBitmap(hInst, MAKEINTRESOURCE(IDB));
	bitMap.width = w;
	bitMap.height = h;

	carDC = CreateCompatibleDC(originDC);



	return result;
}
